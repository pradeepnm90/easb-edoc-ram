Given(/^I fetch the data from the view$/) do
  @dbquery = DBQueries.new
  @sqlquery=@dbquery.getGlobalReDataViewquery.to_s
  @dbresultvalue=BaseContainer.ExecuteQuery(@sqlquery)
end

Then(/^I see the fetched data has only GlobalRe related data$/) do
  @dbverifyactions = DBVerificationActions.new(@reporter)
  @dbverifyactions.VerifyGlobalReData(@dbresultvalue)
end

