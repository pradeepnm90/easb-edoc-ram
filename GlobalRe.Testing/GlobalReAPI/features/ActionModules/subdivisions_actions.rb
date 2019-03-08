require 'rest-client'
require 'json'
require 'hashie'
require 'facets/hash/except'
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","config.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","db_queries.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","base.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","sql_query.rb")
class SubDivsionsActions < BaseContainer
  def initialize(reporter)
    @@reporter = reporter
  end
  def modifyResponseBody(respbody)
    @Responsebody = respbody
    @Responsebodyval = @Responsebody#[0]|@Responsebody[1]|@Responsebody[2]
    @Responsebodyval.extend Hashie::Extensions::DeepFind
    # print "\n"
    # print @Responsebodyval
    # print "\n"
    @hashieresponse = @Responsebodyval.deep_find_all('subDivisions')
    # print "\n"
    # print @hashieresponse
    # print "\n"
    @hashicombresponse = @hashieresponse.join(",")
    @hashicombresponse = @hashicombresponse.gsub(/,,,/,",")#.gsub(/,nil/,)
    @hashicombresponse = @hashicombresponse.gsub(/, nil/,"")
    @hashicombresponse = @hashicombresponse.gsub(/,,/,"")
    @hashicombresponse = @hashicombresponse.gsub(/, "subDivisions"=>nil/,"")
    for i in 0..@Responsebodyval.length-1
      @Responsebodyval[i] = deep_simplify_record(@Responsebodyval[i], ["id","name","sortOrder"])
    end
    @hashiecombinedresponse = @Responsebodyval[0].to_s + @Responsebodyval[1].to_s + @Responsebodyval[2].to_s + + @hashicombresponse.to_s
    @hashiecombinedresponse = @hashiecombinedresponse.gsub(/}{/, '},{')
  end
  def modifyDBresult(dbresult)
    @DBResultVal = dbresult
    #@DBResultVal = @DBResultVal.join(',')
    for i in 0..@DBResultVal.length-1
      @DBResultVal[i] = deep_simplify_record(@DBResultVal[i], ["id","name","sortOrder"])
    end
    @DBResultVal = @DBResultVal.join(',')
    #print "\n"
    return @DBResultVal
    #print "\n"
  end
  def deep_simplify_record(hsh, keep)
    # print "\n"
    # print hsh
    # print "\n"
    # print keep
    # print "\n"
    hsh.keep_if do |h, v|
      if v.is_a?(Hash)
        deep_simplify_record(v, keep)
      else
        keep.include?(h)
      end
    end
  end
  def CompareAPIandDBResponse(modifiedresponsebody,modifiedDBqueryresult)
    @apiResult = modifiedresponsebody
    @dbResultvalue = modifiedDBqueryresult
    # print "\n"
    # print @apiResult
    # print "\n"
    # print @dbResultvalue
    # print "\n"
    if @apiResult.to_s == @dbResultvalue.to_s
      print "PASSED - API Result and DB Query result matched successfully.\n"
    else
      print "FAILED - API Result and DB Query result failed to match.\n"
      fail "FAILED - API Result and DB Query result failed to match.\n"
    end

  end



end