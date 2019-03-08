=begin
class DatabaseSetup
  # this method will clean up all the tables in the schema where we are running the scripts DO NOT RUN IN IUAT/ARNR
  def DatabaseSetup.teardowndata(dbh,inputpath)
#delete script is located in test helper which gets executed when we run this method
    inputpath=inputpath+"sqlscripts/"
    sql_script_delete = File.join(inputpath,"delete_script.sql")
    exe_sql_delete = File.read(sql_script_delete)
    deletes = OracleConnector.execute(dbh, exe_sql_delete)
  end

  def DatabaseSetup.insertData(dbh,inputpath)
    @output_path = File.expand_path inputpath
    files = Dir[@output_path+"/*.sql"]
    files.each do |file_name|
      next if File.directory? file_name
      exe_sql_insert = File.read(file_name)
      inserts = OracleConnector.execute(dbh, exe_sql_insert)
    end
  end


  def DatabaseSetup.runBlobInsert(dbh, inputpath,pform)
    #dbh is database connector class
    #inputpath is the path to xml file where you have insert scripts
    #pform is the name of the form eg 760, 800, 500 etc..
    Dir.glob(inputpath+'/*.xml') do |fpath|
      pDoc = File.new(fpath)
      xmldoc = Document.new(pDoc)
      OracleConnector.rp_xml_blob_insert(dbh, xmldoc.elements['Submission'].attributes["Source"],pform, xmldoc)
      xmldoc = nil
    end
  end

  def DatabaseSetup.setup(dbh,inputpath,sqlscript)
    #dbh is database connector class
    #inputpath is the path to sql file where you have insert scripts
    #sqlscript is the name of the sql file that you want to execute

    sql_credit_inserts = File.join(inputpath,sqlscript)
    exe_sql_file = File.read(sql_credit_inserts)
    OracleConnector.execute(dbh,exe_sql_file)
  end
end
=end
