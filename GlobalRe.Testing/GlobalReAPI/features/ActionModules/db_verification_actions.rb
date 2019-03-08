require 'rest-client'
require 'json'
require 'json-schema'
require 'hashie'
#require File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","config.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","db_queries.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","base.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","sql_query.rb")
class DBVerificationActions < BaseContainer
  def VerifyGlobalReData(queryresult)
    @queryres = queryresult
    @expectedres = [{"division"=>"MGR"}, {"division"=>"FED"}, {"division"=>"CHUB"}]
    BaseContainer.CompareResults(@queryres,@expectedres)
  end
end