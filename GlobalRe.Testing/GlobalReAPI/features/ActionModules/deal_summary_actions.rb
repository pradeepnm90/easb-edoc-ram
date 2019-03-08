require 'rest-client'
require 'json'
require 'hashie'
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","config.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","db_queries.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","base.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","sql_query.rb")
class DealSummaryActions < BaseContainer
  def initialize(reporter)
    @@reporter = reporter
  end
  def modifyResponseBody(respbody)
    @Responsebody = respbody
    #@parsedresponse = JSON.parse(@Responsebody)
    @hashieresponse = Hashie::Mash.new
    @Responsebody.extend Hashie::Extensions::DeepFind
    @hashieresponse = @Responsebody.deep_find_all('statusSummary')
    @hashieresponse.delete(nil)
    @combinedresponse = @Responsebody#|@hashieresponse
    #@combinedresponse.delete_if{|key,value|key="statusSummary"}
    #@combinedresponse.extend Hashie::Extensions::DeepFind
    #@neededResponse = [@combinedresponse.deep_find_all('statusName'),@combinedresponse.deep_find_all('count')]
    #@combinedresponse.each do |k,v|
    #@keyset = [:statusName,:count]
    #  print
    #end

    @combinedresponse.extend Hashie::Extensions::DeepFind
    @respstatusname = @combinedresponse.deep_find_all('statusName')#['statusName']#,'count']#.to_s
    @respcount = @combinedresponse.deep_find_all('count')
    #print "\n"
    #print @respstatusname#[0] #@combinedresponse[0]#.to_s.split(',')
    #print "\n"
    #print @respcount#[0] #@combinedresponse[0]#.to_s.split(',')
    #print "\n"
    @modifiedresponse = Array.new()
    @modifiedresponse.push(@respstatusname)
    @modifiedresponse.push(@respcount)
    #print @modifiedresponse
    return @modifiedresponse
  end
  def compareSummaryAPIResponseandDEBResults(modifiedresponsebody,user)
    @dbquery = DBQueries.new
    @responsebodycombinedArr = Array.new
    @responsebodycombinedArr = modifiedresponsebody
    @StatusNameresponseArr = @responsebodycombinedArr[0]
    @CountresponseArr = @responsebodycombinedArr[1]
    @StatusNameresponseArr.delete(nil)
    @CountresponseArr.delete(nil)
    # print "\n"
    # print @StatusNameresponseArr
    # print "\n"
    puts "Response Data :" + @StatusNameresponseArr.to_s
    for verifycount in 0..@StatusNameresponseArr.length-1

      case @StatusNameresponseArr[verifycount]
        when "Bound - Pending Actions"
          print "Verifying the the API response and DB count for the " + @StatusNameresponseArr[verifycount].to_s + " status.\n"
          @sqlquery = @dbquery.getDealSummaryCountByStatusCodeforUser(@StatusNameresponseArr[verifycount].to_s,user)
          # print "\n"
          # print @sqlquery
          # print "\n"
          @dbresultvalue=BaseContainer.ExecuteQuery(@sqlquery.to_s)
          # print "\n"
          # print @dbresultvalue
          # print "\n"
          @dbresultcount = BaseContainer.fetchDBcount(@dbresultvalue)
          # print "\n"
          # print @dbresultcount
          # print "\n"
          BaseContainer.CompareCountResults(@CountresponseArr[verifycount].to_s,@dbresultcount.to_s)

        when "On Hold"
          print "Verifying the the API response and DB count for the " + @StatusNameresponseArr[verifycount].to_s + " status.\n"
          @sqlquery = @dbquery.getDealSummaryCountByStatusCodeforUser(@StatusNameresponseArr[verifycount].to_s,user)
          # print "\n"
          # print @sqlquery
          # print "\n"
          @dbresultvalue=BaseContainer.ExecuteQuery(@sqlquery.to_s)
          @dbresultcount = BaseContainer.fetchDBcount(@dbresultvalue)
          BaseContainer.CompareCountResults(@CountresponseArr[verifycount].to_s,@dbresultcount.to_s)

        when "In Progress"
          print "Verifying the the API response and DB count for the " + @StatusNameresponseArr[verifycount].to_s + " status.\n"
          @sqlquery = @dbquery.getDealSummaryCountByStatusCodeforUser(@StatusNameresponseArr[verifycount].to_s,user)
          puts @sqlquery # boya added
          # print "\n"
          # print @sqlquery
          # print "\n"
          @dbresultvalue=BaseContainer.ExecuteQuery(@sqlquery.to_s)
          puts "DB result"+@dbresultvalue.to_s # boya added
          @dbresultcount = BaseContainer.fetchDBcount(@dbresultvalue)
          BaseContainer.CompareCountResults(@CountresponseArr[verifycount].to_s,@dbresultcount.to_s)

        when "Authorize"
          print "Verifying the the API response and DB count for the " + @StatusNameresponseArr[verifycount].to_s + " status.\n"
          @sqlquery = @dbquery.getDealSummaryCountByStatusCodeforUser(@StatusNameresponseArr[verifycount].to_s,user)
          # print "\n"
          # print @sqlquery
          # print "\n"
          @dbresultvalue=BaseContainer.ExecuteQuery(@sqlquery.to_s)
          @dbresultcount = BaseContainer.fetchDBcount(@dbresultvalue)
          BaseContainer.CompareCountResults(@CountresponseArr[verifycount].to_s,@dbresultcount.to_s)

        when "Outstanding Quote"
          print "Verifying the the API response and DB count for the " + @StatusNameresponseArr[verifycount].to_s + " status.\n"
          @sqlquery = @dbquery.getDealSummaryCountByStatusCodeforUser(@StatusNameresponseArr[verifycount].to_s,user)
          # print "\n"
          # print @sqlquery
          # print "\n"
          @dbresultvalue=BaseContainer.ExecuteQuery(@sqlquery.to_s)
          @dbresultcount = BaseContainer.fetchDBcount(@dbresultvalue)
          BaseContainer.CompareCountResults(@CountresponseArr[verifycount].to_s,@dbresultcount.to_s)

        when "To Be Declined"
          print "Verifying the the API response and DB count for the " + @StatusNameresponseArr[verifycount].to_s + " status.\n"
          @sqlquery = @dbquery.getDealSummaryCountByStatusCodeforUser(@StatusNameresponseArr[verifycount].to_s,user)
          # print "\n"
          # print @sqlquery
          # print "\n"
          @dbresultvalue=BaseContainer.ExecuteQuery(@sqlquery.to_s)
          @dbresultcount = BaseContainer.fetchDBcount(@dbresultvalue)
          BaseContainer.CompareCountResults(@CountresponseArr[verifycount].to_s,@dbresultcount.to_s)

        when "Bound Pending Data Entry"
          print "Verifying the the API response and DB count for the " + @StatusNameresponseArr[verifycount].to_s + " status.\n"
          @sqlquery = @dbquery.getDealSummaryCountByStatusCodeforUser(@StatusNameresponseArr[verifycount].to_s,user)
          # print "\n"
          # print @sqlquery
          # print "\n"
          @dbresultvalue=BaseContainer.ExecuteQuery(@sqlquery.to_s)
          # print "\n"
          # print @dbresultvalue
          # print "\n"
          @dbresultcount = BaseContainer.fetchDBcount(@dbresultvalue)
          # print "\n"
          # print @dbresultcount
          # print "\n"
          BaseContainer.CompareCountResults(@CountresponseArr[verifycount].to_s,@dbresultcount.to_s)

        when "Renewable - 6 Months"
          print "Verifying the the API response and DB count for the " + @StatusNameresponseArr[verifycount].to_s + " status.\n"
          @sqlquery = @dbquery.getDealSummaryCountByStatusCodeforUser(@StatusNameresponseArr[verifycount].to_s,user)
          # print "\n"
          # print @sqlquery
          # print "\n"
          @dbresultvalue=BaseContainer.ExecuteQuery(@sqlquery.to_s)
          # print "\n"
          # print @dbresultvalue
          # print "\n"
          @dbresultcount = BaseContainer.fetchDBcount(@dbresultvalue)
          # print "\n"
          # print @dbresultcount
          # print "\n"
          BaseContainer.CompareCountResults(@CountresponseArr[verifycount].to_s,@dbresultcount.to_s)

        when "Under Review"
          print "Verifying the the API response and DB count for the " + @StatusNameresponseArr[verifycount].to_s + " status.\n"
          @sqlquery = @dbquery.getDealSummaryCountByStatusCodeforUser(@StatusNameresponseArr[verifycount].to_s,user)
          # print "\n"
          # print @sqlquery
          # print "\n"
          @dbresultvalue=BaseContainer.ExecuteQuery(@sqlquery.to_s)
          # print "\n"
          # print @dbresultvalue
          # print "\n"
          @dbresultcount = BaseContainer.fetchDBcount(@dbresultvalue)
          # print "\n"
          # print @dbresultcount
          # print "\n"
          BaseContainer.CompareCountResults(@CountresponseArr[verifycount].to_s,@dbresultcount.to_s)

      end
    end
    #print "\n"
    #print @StatusNameresponseArr[0]
    #print "\n"
    #print @CountresponseArr[0]
  end
  def compareSubDivisionsSummaryAPIResponseandDBResults(modifiedresponsebody,subdivisions)
    @dbquery = DBQueries.new
    @responsebodycombinedArr = modifiedresponsebody
    @StatusNameresponseArr = @responsebodycombinedArr[0]
    @CountresponseArr = @responsebodycombinedArr[1]
    @StatusNameresponseArr.delete(nil)
    @CountresponseArr.delete(nil)
    for verifycount in 0..@StatusNameresponseArr.length-1

      case @StatusNameresponseArr[verifycount]
        when "Bound - Pending Actions"
          print "Verifying the the API response and DB count for the " + @StatusNameresponseArr[verifycount].to_s + " status.\n"
          @sqlquery = @dbquery.getDealCountByStatusAndSubdivision(@StatusNameresponseArr[verifycount].to_s,subdivisions)
          # print "\n"
          # print @sqlquery
          # print "\n"
          @dbresultvalue=BaseContainer.ExecuteQuery(@sqlquery.to_s)
          # print "\n"
          # print @dbresultvalue
          # print "\n"
          @dbresultcount = BaseContainer.fetchDBcount(@dbresultvalue)
          # print "\n"
          # print @dbresultcount
          # print "\n"
          BaseContainer.CompareCountResults(@CountresponseArr[verifycount].to_s,@dbresultcount.to_s)

        when "On Hold"
          print "Verifying the the API response and DB count for the " + @StatusNameresponseArr[verifycount].to_s + " status.\n"
          @sqlquery = @dbquery.getDealCountByStatusAndSubdivision(@StatusNameresponseArr[verifycount].to_s,subdivisions)
          # print "\n"
          # print @sqlquery
          # print "\n"
          @dbresultvalue=BaseContainer.ExecuteQuery(@sqlquery.to_s)
          @dbresultcount = BaseContainer.fetchDBcount(@dbresultvalue)
          BaseContainer.CompareCountResults(@CountresponseArr[verifycount].to_s,@dbresultcount.to_s)

        when "In Progress"
          print "Verifying the the API response and DB count for the " + @StatusNameresponseArr[verifycount].to_s + " status.\n"
          @sqlquery = @dbquery.getDealCountByStatusAndSubdivision(@StatusNameresponseArr[verifycount].to_s,subdivisions)
          @dbresultvalue=BaseContainer.ExecuteQuery(@sqlquery.to_s)
          @dbresultcount = BaseContainer.fetchDBcount(@dbresultvalue)
          BaseContainer.CompareCountResults(@CountresponseArr[verifycount].to_s,@dbresultcount.to_s)

        when "Authorize"
          print "Verifying the the API response and DB count for the " + @StatusNameresponseArr[verifycount].to_s + " status.\n"
          @sqlquery = @dbquery.getDealCountByStatusAndSubdivision(@StatusNameresponseArr[verifycount].to_s,subdivisions)
          @dbresultvalue=BaseContainer.ExecuteQuery(@sqlquery.to_s)
          @dbresultcount = BaseContainer.fetchDBcount(@dbresultvalue)
          BaseContainer.CompareCountResults(@CountresponseArr[verifycount].to_s,@dbresultcount.to_s)

        when "Outstanding Quote"
          print "Verifying the the API response and DB count for the " + @StatusNameresponseArr[verifycount].to_s + " status.\n"
          @sqlquery = @dbquery.getDealCountByStatusAndSubdivision(@StatusNameresponseArr[verifycount].to_s,subdivisions)
          @dbresultvalue=BaseContainer.ExecuteQuery(@sqlquery.to_s)
          @dbresultcount = BaseContainer.fetchDBcount(@dbresultvalue)
          BaseContainer.CompareCountResults(@CountresponseArr[verifycount].to_s,@dbresultcount.to_s)

        when "To Be Declined"
          print "Verifying the the API response and DB count for the " + @StatusNameresponseArr[verifycount].to_s + " status.\n"
          @sqlquery = @dbquery.getDealCountByStatusAndSubdivision(@StatusNameresponseArr[verifycount].to_s,subdivisions)
          @dbresultvalue=BaseContainer.ExecuteQuery(@sqlquery.to_s)
          @dbresultcount = BaseContainer.fetchDBcount(@dbresultvalue)
          BaseContainer.CompareCountResults(@CountresponseArr[verifycount].to_s,@dbresultcount.to_s)

        when "Bound Pending Data Entry"
          print "Verifying the the API response and DB count for the " + @StatusNameresponseArr[verifycount].to_s + " status.\n"
          @sqlquery = @dbquery.getDealCountByStatusAndSubdivision(@StatusNameresponseArr[verifycount].to_s,subdivisions)
          # print "\n"
          # print @sqlquery
          # print "\n"
          @dbresultvalue=BaseContainer.ExecuteQuery(@sqlquery.to_s)
          # print "\n"
          # print @dbresultvalue
          # print "\n"
          @dbresultcount = BaseContainer.fetchDBcount(@dbresultvalue)
          # print "\n"
          # print @dbresultcount
          # print "\n"
          BaseContainer.CompareCountResults(@CountresponseArr[verifycount].to_s,@dbresultcount.to_s)

        when "Renewable - 6 Months"
          print "Verifying the the API response and DB count for the " + @StatusNameresponseArr[verifycount].to_s + " status.\n"
          @sqlquery = @dbquery.getDealCountByStatusAndSubdivision(@StatusNameresponseArr[verifycount].to_s,subdivisions)
          # print "\n"
          # print @sqlquery
          # print "\n"
          @dbresultvalue=BaseContainer.ExecuteQuery(@sqlquery.to_s)
          # print "\n"
          # print @dbresultvalue
          # print "\n"
          @dbresultcount = BaseContainer.fetchDBcount(@dbresultvalue)
          # print "\n"
          # print @dbresultcount
          # print "\n"
          BaseContainer.CompareCountResults(@CountresponseArr[verifycount].to_s,@dbresultcount.to_s)

        when "Under Review"
          print "Verifying the the API response and DB count for the " + @StatusNameresponseArr[verifycount].to_s + " status.\n"
          @sqlquery = @dbquery.getDealCountByStatusAndSubdivision(@StatusNameresponseArr[verifycount].to_s,subdivisions)
          # print "\n"
          # print @sqlquery
          # print "\n"
          @dbresultvalue=BaseContainer.ExecuteQuery(@sqlquery.to_s)
          # print "\n"
          # print @dbresultvalue
          # print "\n"
          @dbresultcount = BaseContainer.fetchDBcount(@dbresultvalue)
          # print "\n"
          # print @dbresultcount
          # print "\n"
          BaseContainer.CompareCountResults(@CountresponseArr[verifycount].to_s,@dbresultcount.to_s)

      end
    end
    #print "\n"
    #print @StatusNameresponseArr[0]
    #print "\n"
    #print @CountresponseArr[0]
  end
  def modifyDBresults(dbresult)
    @DBQueryResult = dbresult


  end

end