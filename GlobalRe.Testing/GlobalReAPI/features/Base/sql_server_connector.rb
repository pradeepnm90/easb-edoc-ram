=begin
require 'win32ole'

class SqlServerConnector
  puts "In SqlserverConnector class"
  # This class manages database connection and queries
  attr_accessor :connection, :data, :fields
  attr_writer :username, :password
  def initialize(host, username, password, dbname)
    #@connection = nil
    #@data = nil
    @host = host
    @username = username
    @password = password
    @dbname = dbname
    # Create an instance of an ADO Connection
    @@connection = WIN32OLE.new('ADODB.Connection')
    # calling to open a conncetion. Doing this in initialized since it should only be called once when intialized.
    puts "Open connection"
    open(@dbname)
  end
  def open(database)
    # Open ADO connection to the SQL Server database
    connection_string =  "Provider=SQLOLEDB.1;"
    #connection_string << "Persist Security Info=False;"
    connection_string << "User ID=#{@username};"
    connection_string << "password=#{@password};"
    connection_string << "Initial Catalog=#{database};"
    connection_string << "Data Source=#{@host};"
    #~ connection_string << "Network Library=dbmssocn"
    #~ puts connection_string
    puts "Opening Connection using connection string parameters"
    @@connection.Open(connection_string)
  end
  #########################################
  def select_query(sql, recordset)
    # Open the recordset, using an SQL statement and the existing ADO connection
    recordset.Open(sql)
    return recordset
  end
  def update_insert_query(sql)
    # Execute queries that do not return a dataset.
    @@connection.Execute(sql)
  end
  def execute_stp(sql, param, recordset)
    # Open the recordset, using an SQL statement and the existing ADO connection
    if param == nil
      @execStr ='exec ' + sql
    elsif
    @execStr ='exec '+ "#{sql} " + param
    end
    puts @execStr
    recordset.Open(@execStr, @@connection)
    return recordset
  end
  #########################################
  def close()
    puts "Closing DB connection"
    #@@connection.
  end
  ################################
  puts "Exiting SqlserverConnector class"
end
=end