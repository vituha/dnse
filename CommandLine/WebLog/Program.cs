using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSUtil;

namespace WebLog
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
logparser file:%FILE%.sql -i:IISW3C -dirTime:ON -o:DATAGRID
             *
             * 
             * SELECT 	
	TO_TIMESTAMP(date, time) AS dt, 
	STRCAT(	cs-uri-stem, 
		REPLACE_IF_NOT_NULL(cs-uri-query, STRCAT('?',cs-uri-query))
		) AS Request, 
	STRCAT(	TO_STRING(sc-status), 		
		STRCAT(	'.',
			COALESCE(TO_STRING(sc-substatus), '?' )
			)
		) AS Status
FROM <Default Web Site>
WHERE (sc-status >= 400) 
ORDER BY dt DESC

             * 
             * SELECT 	
	TO_TIMESTAMP(date, time) AS dt, 
	STRCAT(	cs-uri-stem, 
		REPLACE_IF_NOT_NULL(cs-uri-query, STRCAT('?',cs-uri-query))
		) AS Request, 
	STRCAT(	TO_STRING(sc-status), 		
		STRCAT(	'.',
			COALESCE(TO_STRING(sc-substatus), '?' )
			)
		) AS Status
FROM <Default Web Site>
ORDER BY dt DESC

             */
            ICOMW3CInputContext inputContext = new COMW3CInputContextClassClass();

            ILogQuery query = new LogQueryClassClass();
            //query.Execute(

            string ConnectionString = @"Server=KORIDOOR\SQLEXPRESS;Database=WebLog;Trusted_COnnection=True";
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            conn.Close();
        }
    }
}
