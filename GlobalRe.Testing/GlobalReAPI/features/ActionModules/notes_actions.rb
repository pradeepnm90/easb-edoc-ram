require 'rest-client'
require 'json'
require 'hashie'
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","config.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","db_queries.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","base.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","sql_query.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"ActionModules","deal_by_status_name_actions.rb")
class Notes < DealByStatusNameActions

  def initialize(reporter)
    @@reporter = reporter
  end

end