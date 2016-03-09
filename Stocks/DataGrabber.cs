using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Stocks
{
    static class DataGrabber
    {
        private static SqlConnection _conn;
        private static object _lockObj = new Object();

        public static void Connect()
        {
            _conn = new SqlConnection("Server=192.168.1.16;Database=Stocks;User Id=Alex;Password=1234");
            _conn.Open();
        }

        public static void Close()
        {
            _conn.Close();
        }

        public static DataTable GetStockCloseInfo(string symbol, string startDate, string endDate)
        {
            string query = "SELECT Date, c_AdjClose FROM t_StockValue WHERE Stock='" + symbol + "' AND Date between '" + startDate + "' and '" + endDate + "'";

            return GetStockInfo(symbol, startDate, endDate, query);
        }

        public static DataTable GetStockMA(string symbol, string startDate, string endDate, int maValue)
        {
            string query = "SELECT X.Date, (SELECT AVG(A.c_AdjClose) " +
                                                     "FROM (SELECT TOP " + maValue + " Stock, Date, c_AdjClose " +
                                                            "FROM t_StockValue " +
                                                            "WHERE Stock = X.Stock " +
                                                            "and Date <= X.Date " +
                                                            "order by Date desc) A " +
                                                     "WHERE A.Stock = X.Stock) as c_AdjClose " +
                           "FROM t_StockValue X " +
                           "WHERE X.Stock = '" + symbol + "' " +
                           "and X.Date between '" + startDate + "' and '" + endDate + "'"; // slow?

            return GetStockInfo(symbol, startDate, endDate, query);
        }

        public static DataTable GetStockConversionLine(string symbol, string startDate, string endDate, int top)
        {
            string query = "SELECT X.Date, (SELECT (MIN(A.c_Low) + MAX(A.c_High)) / 2.0 " +
                                                    "FROM (SELECT TOP " + top + " Stock, c_Low, c_High " +
                                                        "FROM t_StockValue " +
                                                        "WHERE Stock = X.Stock and Date <= X.Date " +
                                                        "ORDER BY Date desc) A " +
                                                    "WHERE A.Stock = X.Stock) as Conversion " +
                           "FROM t_StockValue X " +
                           "WHERE X.Stock = '" + symbol + "' and X.Date between '" + startDate + "' and '" + endDate + "'"; // slow?

            return GetStockInfo(symbol, startDate, endDate, query);
        }

        public static DataTable GetStockLeadingSpanALine(string symbol, string startDate, string endDate, int top1, int top2)
        {
            string query = "SELECT X.Date, (SELECT (((MIN(A.c_Low) + MAX(A.c_High)) / 2.0) + ((MIN(B.c_Low) + MAX(B.c_High)) / 2.0)) / 2.0 " +
                                                    "FROM (SELECT TOP " + top1 + " Stock, c_Low, c_High " +
                                                        "FROM t_StockValue " +
                                                        "WHERE Stock = X.Stock and Date <= X.Date " +
                                                        "ORDER BY Date desc) A, " +
                                                        "(SELECT TOP " + top2 + " Stock, c_Low, c_High " +
                                                        "FROM t_StockValue " +
                                                        "WHERE Stock = X.Stock and Date <= X.Date " +
                                                        "ORDER BY Date desc) B " +
                                                    "WHERE A.Stock = X.Stock and B.Stock = X.Stock) as LSpanA " +
                           "FROM t_StockValue X " +
                           "WHERE X.Stock = '" + symbol + "' and X.Date between '" + startDate + "' and '" + endDate + "'"; // slow?

            return GetStockInfo(symbol, startDate, endDate, query);
        }



        private static DataTable GetStockInfo(string symbol, string startDate, string endDate, string query)
        {
            DataTable dt = new DataTable();

            SqlCommand cmd = new SqlCommand(query, _conn);

            lock (_lockObj)
            {
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    dt.Load(rdr);
                }
            }

            return dt;
        }
    }
}
