=begin
require 'xmlrpc/base64'
#~ require "../"+@@currentDir+"/features/util/SQLConnector.rb"
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","sql_server_connector.rb")
########################
class SQLQuery < SqlServerConnector
  puts "In SQL Query Class for executing SQL Query"
  def initialize(pDBname)
    # Initialize all objects that this class requires.
    puts "In SQL Query Class - Instantiate objects"
    # create instance of sql server connector. This connection object is only initialized once.
    @password="U3VtbWVyMTgK"
    @decoded_password = XMLRPC::Base64.decode(@password)
    @@dbconnector = SqlServerConnector.new('va1-dgmrsql053.markelcorp.markelna.com', 'maxbi', 'd@t@h0g', pDBname)
    #@@dbconnector = SqlServerConnector.new('va1-dgmrsql053.markelcorp.markelna.com', 'sjanardanannair', @decoded_password, pDBname)
  end
#####################
  def query(querystring, type, param, recordset)
    @querystring = querystring
    @type = type
    @recordset = recordset
    case
      when @type == "select"
        puts "executing select statement"
        @recordset = @@dbconnector.select_query(@querystring, @recordset)
        return @recordset
      when @type == "update"
        puts "execute update query"
        @@dbconnector.update_insert_query(@querystring)
      when @type == "insert"
        puts "execute insert query"
        @@dbconnector.update_insert_query(@querystring)
      when @type == "storedprocedure"
        puts "execute STP query"
        @@dbconnector.execute_stp(@querystring, param, @recordset)
        return @@recordset
      else
        @@dbconnector.close()
        raise "Invalid query type"
    end
  end
########################
  def close()
    puts "Initiating DB close"
    @@dbconnector.close()
  end
########################
  puts "Exiting Query Class"
end
=end
