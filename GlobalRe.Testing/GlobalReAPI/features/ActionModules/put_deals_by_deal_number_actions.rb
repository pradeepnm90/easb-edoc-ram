require 'rest-client'
require 'json'
require 'hashie'
require 'facets/hash/except'
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","config.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","db_queries.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","base.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","sql_query.rb")
class PutDealsByDealNumberActions < BaseContainer
  def initialize()
  end
  def verifyfieldvalueIsUpdated(field,dealnumber,value,status,actualvalue)
    if status.to_s == "Bound"
      print "Skipping DB value to input value comparison as the deal is of status #{status}.\n"
    else
      @actualvaluesarray = BaseContainer.generatedealvaluesArray(actualvalue)
      @goingtobeModifiedvaluesarray = BaseContainer.generateGoingTomodifyvaluesArray(value)
      if @goingtobeModifiedvaluesarray.length == 2
        @value1 = @goingtobeModifiedvaluesarray[0]
        @value2 = @goingtobeModifiedvaluesarray[1]
      elsif @goingtobeModifiedvaluesarray.length == 1
        @value1 = @goingtobeModifiedvaluesarray[0]
      end
      @dealnumber = dealnumber
      @status = status
      @dbquery = DBQueries.new
      @sqlquery=@dbquery.getDealFieldByDealNumber(field,dealnumber).to_s
      @dbresultvalue=BaseContainer.ExecuteQuery(@sqlquery)
      # print "\n"
      # print @dbresultvalue[0]
      # print "\n"
      case field
        when "Deal Name"
          #@actualvalue = @actualvaluesarray[1].to_s
          # @expectedvalue = "{\"dealName\"=>\"#{@value1}\"}"
          @modifiedvaluefromDB = @dbresultvalue[0]
          @parsedDNDBvalue = JSON.parse(@modifiedvaluefromDB.to_json)
          # print "\n"
          # print @parsedDNDBvalue
          # print "\n"

          @extractedDNDBValue1 = @parsedDNDBvalue["dealName"]
          #assert(@extractedDNDBValue1.to_s == @value1.to_s,"FAILED - The DB Value for the dealName field \"#{@extractedDNDBValue1}\" and the input parameter value \"#{@value1}\" failed to match.\n")
          if @extractedDNDBValue1.to_s == @value1.to_s
            print "PASSED - The DB Value for the dealName field \"#{@extractedDNDBValue1}\" and the input parameter value \"#{@value1}\" are matched successfully.\n"
            $putmessage.puts "PASSED - The DB Value for the dealName field \"#{@extractedDNDBValue1}\" and the input parameter value \"#{@value1}\" are matched successfully.\n"
            #$putmessage.pass
          else
            print "FAILED - The DB Value for the dealName field \"#{@extractedDNDBValue1}\" and the input parameter value \"#{@value1}\" failed to match.\n"
            fail "FAILED - The DB Value for the dealName field \"#{@extractedDNDBValue1}\" and the input parameter value \"#{@value1}\" failed to match.\n"
            $putmessage.puts "FAILED - The DB Value for the dealName field \"#{@extractedDNDBValue1}\" and the input parameter value \"#{@value1}\" failed to match.\n"
            #fail($scenario)
          end
        when "Target Date"
          # @expectedvalue = "{\"targetDate\"=>\"#{@value1}\"}"
          # @actualvalue = @actualvaluesarray[6].to_s
          @modifiedvaluefromDB = @dbresultvalue[0]
          @parsedTDDBvalue = JSON.parse(@modifiedvaluefromDB.to_json)
          # print "\n"
          # print @parsedTDDBvalue
          # print "\n"
          @extractedTDDBValue1 = @parsedTDDBvalue["targetDate"]

          if @extractedTDDBValue1.to_s == @value1.to_s
            print "PASSED - The DB Value for the targetDate field \"#{@extractedTDDBValue1}\" and the input parameter value \"#{@value1}\" are matched successfully.\n"
            $putmessage.puts "PASSED - The DB Value for the targetDate field \"#{@extractedTDDBValue1}\" and the input parameter value \"#{@value1}\" are matched successfully.\n"
          else
            print "FAILED - The DB Value for the targetDate field \"#{@extractedTDDBValue1}\" and the input parameter value \"#{@value1}\" failed to match.\n"
            fail "FAILED - The DB Value for the targetDate field \"#{@extractedTDDBValue1}\" and the input parameter value \"#{@value1}\" failed to match.\n"
            $putmessage.puts "FAILED - The DB Value for the targetDate field \"#{@extractedTDDBValue1}\" and the input parameter value \"#{@value1}\" failed to match.\n"
          end
        when "Priority"
          # @expectedvalue = "{\"targetDate\"=>\"#{@value1}\"}"
          # @actualvalue = @actualvaluesarray[6].to_s
          @modifiedvaluefromDB = @dbresultvalue[0]
          @parsedPDBvalue = JSON.parse(@modifiedvaluefromDB.to_json)
          # print "\n"
          # print @parsedPDBvalue
          # print "\n"
          @extractedPDBValue1 = @parsedPDBvalue["priority"]
          if @extractedPDBValue1.to_s == @value1.to_s
            print "PASSED - The DB Value for the Priority field \"#{@extractedPDBValue1}\" and the input parameter value \"#{@value1}\" are matched successfully.\n"
          else
            print "FAILED - The DB Value for the Priority field \"#{@extractedPDBValue1}\" and the input parameter value \"#{@value1}\" failed to match.\n"
            fail "FAILED - The DB Value for the Priority field \"#{@extractedPDBValue1}\" and the input parameter value \"#{@value1}\" failed to match.\n"
          end
        when "Status"
          # @expectedvalue = "{\"status\"=>\"#{@value1}\",\"statusCode\"=>\"#{@value2}\"}"
          # @actualnamevalue = @actualvaluesarray[3].to_s
          # @actualcodevalue = @actualvaluesarray[2].to_s
          @modifiedvaluefromDB = @dbresultvalue[0]
          @parsedSDBvalue = JSON.parse(@modifiedvaluefromDB.to_json)
          # print "\n"
          # print @parsedSDBvalue
          # print "\n"
          @extractedSDBValue1 = @parsedSDBvalue["status"]
          @extractedSDBValue2 = @parsedSDBvalue["statusCode"]
          if @extractedSDBValue1.to_s == @value1.to_s && @extractedSDBValue2.to_s == @value2.to_s
            print "PASSED - The DB Value for the status and statusCode field having the values \"#{@extractedSDBValue1}\", \"#{@extractedSDBValue2}\" and the input parameter values \"#{@value1}\", \"#{@value2}\" are matched successfully.\n"
          else
            print "FAILED - The DB Value for the status and statusCode field having the values \"#{@extractedSDBValue1}\", \"#{@extractedSDBValue2}\" and the input parameter values \"#{@value1}\", \"#{@value2}\" failed to match.\n"
            fail "FAILED - The DB Value for the status and statusCode field having the values \"#{@extractedSDBValue1}\", \"#{@extractedSDBValue2}\" and the input parameter values \"#{@value1}\", \"#{@value2}\" failed to match.\n"
          end
        when "Underwriter"
          # @expectedvalue = "{\"primaryUnderwriterName\"=>\"#{@value1}\",\"primaryUnderwriterCode\"=>\"#{@value2}\"}"
          # @actualnamevalue = @actualvaluesarray[10].to_s
          # @actualcodevalue = @actualvaluesarray[9].to_s
          @modifiedvaluefromDB = @dbresultvalue[0]
          @parsedUDBvalue = JSON.parse(@modifiedvaluefromDB.to_json)
          # print "\n"
          # print @parsedUDBvalue
          # print "\n"
          @extractedUDBValue1 = @parsedUDBvalue["primaryUnderwriterName"]
          @extractedUDBValue2 = @parsedUDBvalue["primaryUnderwriterCode"]
          if @extractedUDBValue1.to_s == @value1.to_s && @extractedUDBValue2.to_s == @value2.to_s
            print "PASSED - The DB Value for the primaryUnderwriterName and primaryUnderwriterCode field having the values \"#{@extractedUDBValue1}\", \"#{@extractedUDBValue2}\" and the input parameter values \"#{@value1}\", \"#{@value2}\" are matched successfully.\n"
          else
            print "FAILED - The DB Value for the primaryUnderwriterName and primaryUnderwriterCode field having the values \"#{@extractedUDBValue1}\", \"#{@extractedUDBValue2}\" and the input parameter values \"#{@value1}\", \"#{@value2}\" failed to match.\n"
            fail "FAILED - The DB Value for the primaryUnderwriterName and primaryUnderwriterCode field having the values \"#{@extractedUDBValue1}\", \"#{@extractedUDBValue2}\" and the input parameter values \"#{@value1}\", \"#{@value2}\" failed to match.\n"
          end
        when "Underwriter 2"
          # @expectedvalue = "{\"secondaryUnderwriterName\"=>\"#{@value1}\",\"secondaryUnderwriterCode\"=>\"#{@value2}\"}"
          # @actualnamevalue = @actualvaluesarray[12].to_s
          # @actualcodevalue = @actualvaluesarray[11].to_s
          @modifiedvaluefromDB = @dbresultvalue[0]
          @parsedU2DBvalue = JSON.parse(@modifiedvaluefromDB.to_json)
          # print "\n"
          # print @parsedU2DBvalue
          # print "\n"
          @extractedU2DBValue1 = @parsedU2DBvalue["secondaryUnderwriterName"]
          @extractedU2DBValue2 = @parsedU2DBvalue["secondaryUnderwriterCode"]
          if @extractedU2DBValue1.to_s == @value1.to_s && @extractedU2DBValue2.to_s == @value2.to_s
            print "PASSED - The DB Value for the secondaryUnderwriterName and secondaryUnderwriterCode field having the values \"#{@extractedU2DBValue1}\", \"#{@extractedU2DBValue2}\" and the input parameter values \"#{@value1}\", \"#{@value2}\" are matched successfully.\n"
          else
            print "FAILED - The DB Value for the secondaryUnderwriterName and secondaryUnderwriterCode field having the values \"#{@extractedU2DBValue1}\", \"#{@extractedU2DBValue2}\" and the input parameter values \"#{@value1}\", \"#{@value2}\" failed to match.\n"
            fail "FAILED - The DB Value for the secondaryUnderwriterName and secondaryUnderwriterCode field having the values \"#{@extractedU2DBValue1}\", \"#{@extractedU2DBValue2}\" and the input parameter values \"#{@value1}\", \"#{@value2}\" failed to match.\n"
          end
        when "TA"
          # @expectedvalue = "{\"technicalAssistantName\"=>\"#{@value1}\",\"technicalAssistantCode\"=>\"#{@value2}\"}"
          # @actualnamevalue = @actualvaluesarray[14].to_s
          # @actualcodevalue = @actualvaluesarray[13].to_s
          @modifiedvaluefromDB = @dbresultvalue[0]
          @parsedTADBvalue = JSON.parse(@modifiedvaluefromDB.to_json)
          # print "\n"
          # print @parsedTADBvalue
          # print "\n"
          @extractedTADBValue1 = @parsedTADBvalue["technicalAssistantName"]
          @extractedTADBValue2 = @parsedTADBvalue["technicalAssistantCode"]
          if @extractedTADBValue1.to_s == @value1.to_s && @extractedTADBValue2.to_s == @value2.to_s
            print "PASSED - The DB Value for the technicalAssistantName and technicalAssistantCode field having the values \"#{@extractedTADBValue1}\", \"#{@extractedTADBValue2}\" and the input parameter values \"#{@value1}\", \"#{@value2}\" are matched successfully.\n"
          else
            print "FAILED - The DB Value for the technicalAssistantName and technicalAssistantCode field having the values \"#{@extractedTADBValue1}\", \"#{@extractedTADBValue2}\" and the input parameter values \"#{@value1}\", \"#{@value2}\" failed to match.\n"
            fail "FAILED - The DB Value for the technicalAssistantName and technicalAssistantCode field having the values \"#{@extractedTADBValue1}\", \"#{@extractedTADBValue2}\" and the input parameter values \"#{@value1}\", \"#{@value2}\" failed to match.\n"
          end
        when "Modeler"
          # @expectedvalue = "{\"modellerName\"=>\"#{@value1}\",\"modellerCode\"=>\"#{@value2}\"}"
          # @actualnamevalue = @actualvaluesarray[16].to_s
          # @actualcodevalue = @actualvaluesarray[15].to_s
          @modifiedvaluefromDB = @dbresultvalue[0]
          @parsedMDBvalue = JSON.parse(@modifiedvaluefromDB.to_json)
          # print "\n"
          # print @parsedMDBvalue
          # print "\n"
          @extractedMDBValue1 = @parsedMDBvalue["modellerName"]
          @extractedMDBValue2 = @parsedMDBvalue["modellerCode"]
          if @extractedMDBValue1.to_s == @value1.to_s && @extractedMDBValue2.to_s == @value2.to_s
            print "PASSED - The DB Value for the modellerName and modellerCode field having the values #{@extractedMDBValue1}, #{@extractedMDBValue2} and the input parameter values #{@value1}, #{@value2} are matched successfully.\n"
          else
            print "FAILED - The DB Value for the modellerName and modellerCode field having the values #{@extractedMDBValue1}, #{@extractedMDBValue2} and the input parameter values #{@value1}, #{@value2} failed to match.\n"
            fail "FAILED - The DB Value for the modellerName and modellerCode field having the values #{@extractedMDBValue1}, #{@extractedMDBValue2} and the input parameter values #{@value1}, #{@value2} failed to match.\n"
          end
        when "Actuary"
          # @expectedvalue = "{\"actuaryName\"=>\"#{@value1}\",\"actuaryCode\"=>\"#{@value2}\"}"
          # @actualnamevalue = @actualvaluesarray[18].to_s
          # @actualcodevalue = @actualvaluesarray[17].to_s
          @modifiedvaluefromDB = @dbresultvalue[0]
          @parsedADBvalue = JSON.parse(@modifiedvaluefromDB.to_json)
          # print "\n"
          # print @parsedADBvalue
          # print "\n"
          @extractedADBValue1 = @parsedADBvalue["actuaryName"]
          @extractedADBValue2 = @parsedADBvalue["actuaryCode"]
          if @extractedADBValue1.to_s == @value1.to_s && @extractedADBValue2.to_s == @value2.to_s
            print "PASSED - The DB Value for the actuaryName and actuaryCode field having the values \"#{@extractedADBValue1}\", \"#{@extractedADBValue2}\" and the input parameter values \"#{@value1}\", \"#{@value2}\" are matched successfully.\n"
          else
            print "FAILED - The DB Value for the actuaryName and actuaryCode field having the values \"#{@extractedADBValue1}\", \"#{@extractedADBValue2}\" and the input parameter values \"#{@value1}\", \"#{@value2}\" failed to match.\n"
            fail "FAILED - The DB Value for the actuaryName and actuaryCode field having the values \"#{@extractedADBValue1}\", \"#{@extractedADBValue2}\" and the input parameter values \"#{@value1}\", \"#{@value2}\" failed to match.\n"
          end
      end
    end
  end


  def verifyBusinessClassfieldvalueIsUpdated(dealnumber,value,status)

    if status.to_s == "Bound"
      print "Skipping DB value to input value comparison as the deal is of status #{status}.\n"
    else
      @dealnumber = dealnumber
      @status = status
      @exposureCode = value
      @dbquery = DBQueries.new
      @sqlquery=@dbquery.getBusinessClass(dealnumber).to_s
      @dbresultvalue=BaseContainer.ExecuteQuery(@sqlquery)
      # @parsedDBvalue = JSON.parse(@dbresultvalue.to_json)
      # puts @dbresultvalue
      @businessClass = @dbresultvalue[0]["BusinessClass"]
      @riskCategory = @dbresultvalue[0]["RiskCategory"]
      @liabilityType = @dbresultvalue[0]["LiabilityType"]

      if @riskCategory == 3 and @liabilityType == 1
        case @exposureCode.to_i
          when 1125,5916,1126,1128,5922
            if @businessClass == 2
              print "PASSED - The DB Value RiskCategory \"#{@riskCategory}\" , Liability Type \"#{@liabilityType}\",Business Class \"#{@businessClass}\" are successfully updated for the deal \"#{@dealnumber}\" .\n"
            else
              print "FAILED - The DB Value RiskCategory \"#{@riskCategory}\" , Liability Type \"#{@liabilityType}\",Business Class \"#{@businessClass}\" are not updated for the deal \"#{@dealnumber}\" .\n"
              fail "FAILED - The DB Value RiskCategory \"#{@riskCategory}\" , Liability Type \"#{@liabilityType}\",Business Class \"#{@businessClass}\" are not correctly updated for the deal \"#{@dealnumber}\" .\n"
            end
            puts "Business Class = Casuality "
          when 5827,5795
            if @businessClass == 1
              print "PASSED - The DB Value RiskCategory \"#{@riskCategory}\" , Liability Type \"#{@liabilityType}\",Business Class \"#{@businessClass}\" are successfully updated for the deal \"#{@dealnumber}\" .\n"
            else
              print "FAILED - The DB Value RiskCategory \"#{@riskCategory}\" , Liability Type \"#{@liabilityType}\",Business Class \"#{@businessClass}\" are not updated for the deal \"#{@dealnumber}\" .\n"
              fail "FAILED - The DB Value RiskCategory \"#{@riskCategory}\" , Liability Type \"#{@liabilityType}\",Business Class \"#{@businessClass}\" are not correctly updated for the deal \"#{@dealnumber}\" .\n"
            end
            puts "Business Class = Property "
          when 1129,1130,5902,1131,1132,1133,1134,1135,1136,1137,1138,5907,1139,1020, 1113
            if @businessClass == 10
              print "PASSED - The DB Value RiskCategory \"#{@riskCategory}\" , Liability Type \"#{@liabilityType}\",Business Class \"#{@businessClass}\" are successfully updated for the deal \"#{@dealnumber}\" .\n"
            else
              print "FAILED - The DB Value RiskCategory \"#{@riskCategory}\" , Liability Type \"#{@liabilityType}\",Business Class \"#{@businessClass}\" are not updated for the deal \"#{@dealnumber}\" .\n"
              fail "FAILED - The DB Value RiskCategory \"#{@riskCategory}\" , Liability Type \"#{@liabilityType}\",Business Class \"#{@businessClass}\" are not correctly updated for the deal \"#{@dealnumber}\" .\n"
            end
            puts "Business class = Others"
          else
            print "FAILED - ExposureCode dose not belong to GRS"
            fail "FAILED - ExposureCode dose not belong to GRS"
        end
      end
    end

  end


  def verifyCedantfieldvalueIsUpdated(dealnumber,value,status,responseMessage)
    if status.to_s == "Bound"
      print "Skipping DB value to input value comparison as the deal is of status #{status}.\n"
    else
      @responseMessage = responseMessage
      @responseMessage =  JSON.parse(@responseMessage)
      # puts "response message : "+@responseMessage.to_s
      @responseMessage =  @responseMessage["messages"]
      # puts "response message : "+@responseMessage.to_s

      @dealnumber = dealnumber
      @status = status
      @cedantData = value
      @dbquery = DBQueries.new
      @sqlquery=@dbquery.getCedantInformation(dealnumber).to_s
      @dbresultvalue=BaseContainer.ExecuteQuery(@sqlquery)
      # @parsedDBvalue = JSON.parse(@dbresultvalue.to_json)
      # puts @dbresultvalue
      @cedantCode = @dbresultvalue[0]["CedentCode"].to_i
      @cedantLocationCode = @dbresultvalue[0]["CedentLocationCode"].to_i

      @goingtobeModifiedvaluesarray = BaseContainer.generateGoingTomodifyvaluesArray(@cedantData)
      # ----- Splitting the values if multiple values are passed -----
      if @goingtobeModifiedvaluesarray.length == 2
        @value1 = @goingtobeModifiedvaluesarray[0].to_i
        @value2 = @goingtobeModifiedvaluesarray[1].to_i
        #   -------Validating the filed values --------------
        if @cedantCode == @value1 and @cedantLocationCode == @value2 and @responseMessage == []
          print "PASSED : Cedant Information is updated in the database successfully values - CedantCode : \"#{@cedantCode}\" , CedantLocationCode : \"#{@cedantLocationCode}\" \n"
        elsif @cedantCode != @value1 and @cedantLocationCode != @value2 and @responseMessage != []
          print "PASSED : Cedant Information is not updated in the database values - CedantCode : \"#{@cedantCode}\" , CedantLocationCode : \"#{@cedantLocationCode}\" \n"
        else
          print "PASSED : Cedant Validation failed \n"
          fail "PASSED : Cedant Validation failed \n"
        end
      end




    end

  end

  def self.ValidateCedantWarnings(responseMessage,expectedWarningMessage)
    @responseMessage = responseMessage
    @expectedWarningMessage = expectedWarningMessage.to_s
    @responseMsg =  JSON.parse(@responseMessage)
    # puts "response message : "+@responseMsg.to_s
    @responseMessage =  @responseMsg["messages"]
    # puts "response message : "+@responseMessage.to_s
    if @responseMessage == []
      @expectedWarningMessageArray = BaseContainer.generateGoingTomodifyvaluesArray(@expectedWarningMessage)
      # ----- Splitting the expected Warining Messages if multiple values are passed -----
      if @expectedWarningMessageArray.length == 2
        @warning1 = @expectedWarningMessageArray[0].to_s
        @warning2 = @expectedWarningMessageArray[1].to_s
      end

      if @responseMsg.to_s.include? @warning1.to_s and @responseMsg.to_s.include? @warning2.to_s
        print "PASSED - Cedant information is successfully stamped to the database .\n"
      else
        print "PASSED - Cedant information is not stamped to the database .\n"
        fail "PASSED - Cedant information is not stamped to the database .\n"
      end
    else
      if @responseMessage.to_s.include? @expectedWarningMessage
        print "PASSED - Cedant warning message : "+"\""+@expectedWarningMessage +"\""+ "is valid for given cedant information.\n"
      else
        print "FAILED - Cedant warning message : "+"\""+@expectedWarningMessage +"\""+ "is not expected for given cedant information.\n"
        fail "FAILED - Cedant warning message : "+"\""+@expectedWarningMessage +"\""+ "is not expected for given cedant information"
      end
    end
  end


end