using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingProject7LearningDATABASE
{/// <summary>
/// ExexcuteReader command returns the SqlDataReader object to read data.... ExecuteReader is method of SqlCommand class
/// which takes command and connection object as parameters(its only one of its overloads)
/// ExexcuteScalar is method of SqlCommand which is useful when our command is returning only one value from exucating 
/// command its waste use whole reader object for that purpose while we can use single value calculator for the same 
/// sqlcommnd itself calculates the all thing and returns only one thing that we required
/// 
/// 
/// ****************************************************************************************************************
/// disconnected databases and working with
/// S
/// DataSets are in memory data stored for disconnected behaviour as SqlDataAdapter objects works as connection between data source and 
/// connect it to data source. It(DataSet) doesent operate data source.
/// 
/// It works Like that (The job of sqlDataAdapter)
/// Open conn ---> Write data to dataset ---> Closes Conn
/// Open Conn ---> write changes from dataset to datasource ---> closes connection
/// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            DataSet dataSet = new DataSet();
            SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TestingLocalDb;Integrated Security=True");
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from StudentInfo", con);
            /*
             * we havent hopened any connection here as its responsibility of sqldataadapter
             * to open and close connection as needed
             * 
             * here we have only given select command 
             * but sqldataadapter will manage all other commands like insert update delete itself only
             * for that it use sqlcommandbuilder class which takes an sqldataadapter object as constructor initialization
             * parameter
             * this is to know on which adapter this command should run
             * 
             
             */

            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

            /*
             * 
             * Now once we have created tese datasets and adapter and commandbuilder to run and create dataset
             * its time to fill this dataset
             * for that we need to use the method of adapter to fill the dataset
             * this fill method has two over load 
             * one is which takes only dataset object as its parameter
             * second is one which takes 2 parameter one is dataset object and second one is tabel name for that particular
             * table this is the name which we will later use to accessing data 
             * if we use first overload it will create tabel with name Tabel, tbel1 .....
             * these things are happing in dataset not in datasource ; which is disconnected dataset;
             * 
             
             */

            dataAdapter.Fill(dataSet, "StudentInfoDataset");
            /*
             * 
             * now we can use dataset too do our work if we want to update dataset to source
             * we will use again dataAdapter to update with parameters dataset object and dataset tabel name
             
             */

            DataTableReader reader = dataSet.CreateDataReader();
            while(reader.HasRows)
            {
                while (reader.Read())
                { Console.WriteLine(" " + reader[1]);

                }
            }

            dataAdapter.Update(dataSet, "StudentInfoDataset");
        }string id = "25 and 35;m inner jpi";
    }

    ///<summary>
    ///
    /// As i am using above to run sql command i am directly putting string command in the SqlCommand like
    /// "insert into table where id="+ id 
    /// actually above method ofwriting query is dangerous 
    /// it may give control of our database to hacker 
    /// to prevent the same we need to create parameter object and use it uin our sql command
    /// so that we can protect our database
    /// as above case we are directly passing value we got from the textbox or the weblink
    /// and it might be dangerous as we are allowing to use any sort of string that is passed from the textbox or the 
    /// 
    /// 
    /// so first create sqlCommandObject so 
    /// see below created class
    /// 
    /// </summary>
    ///


    class ThisIsTotest
    {
        public void TestAgain()
        {

           SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TestingLocalDb;Integrated Security=True");

            String commandString = "Select * from Table where id = @Id";//created string with parameters
            SqlCommand command = new SqlCommand(commandString, con);//passing the same string to command object
            //Now we have to define the parameters otherwise how sqlcommand will know which parameters to use

            SqlParameter param = new SqlParameter();
            param.ParameterName = "@Id";
            param.Value = 1; //here you can pass whatever the value we have been taking from the webpages or
            //textboxes or even with console app
            //once we created these two things now we have to give this param pbject to our command object

            command.Parameters.Add(param);
            //no we are done to use our query safely as the parameters assigned will be only treated as params
            //only so we can relx little bit

    }
    }
}