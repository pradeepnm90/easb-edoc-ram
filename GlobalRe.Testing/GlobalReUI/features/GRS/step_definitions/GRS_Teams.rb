And(/^I see the Team field on the GRS Home page$/) do
  print "Step Name: To verify the Team field on the GRS Home page.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @teamfield = @grshomepg.fetchTeamfield
  @grshomepg.verifyTeamfield(@teamfield)
end

And(/^I click on the Team field$/) do
  print "Step Name: To verify the Team field popup opens on clicking on Team field.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @teamfield = @grshomepg.fetchTeamfield
  @grshomepg.ClickBtn(@teamfield)
end

And(/^I will be abe to see (.*) team with (.*) and (.*) sub options$/) do |teams,sub1,sub2|
  print "Step Name: To verify the Team options with sub options.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @grshomepg.verifyTeamoptions(teams,sub1,sub2)
  #@grshomepg.ClickBtn(@teamfield)
end

And(/^I click to select the (.*) team option$/) do |teams|
  print "Step Name: To verify the selection of team options.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @grshomepg.selectTeamOptions(teams)

end

Then(/^I will be able to see (.*) and (.*) sub\-options getting selected by default on selecting the team option$/) do |sub1,sub2|
  print "Step Name: To verify the sub options getting selected by default on selecting the team option.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @grshomepg.verifyTeamsubOptions(sub1,sub2)
end

And(/^I click to unselect the (.*) sub option$/) do |sub1|
  print "Step Name: To verify the unselecting suboption of team.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @grshomepg.unselectTeamsubOptions(sub1)
end

Then(/^I will be able to see (.*) sub option and (.*) gets unselected$/) do |teams,sub1|
  print "Step Name: To verify the  suboption is unselected.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @grshomepg.verifyUnselectTeamsubOptions(teams,sub1)
end

Then(/^I will be able to see the count of deals on (.*) as per the selected (.*) team$/) do |panels,teams|
  print "Step Name: To verify the  counts of deals as per the selected teams.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  sleep 2
  @grshomepg.selectPanel
  @neededElement = @grshomepg.fetchpanelElement(panels)
  @dbquery = DBQueries.new
  @sqlquery=@dbquery.getDBSubdivisionCountQuery(panels,teams).to_s
  @dbcount = @grshomepg.sendCountQuery(@sqlquery)
  #print "\n"+ @dbcount
  @panelcount = @grshomepg.getpanelCount(@neededElement)
  sleep 5
  @grshomepg.verifyCount(@panelcount,@dbcount)

end

And(/^I click on the panel$/) do
  print "Step Name : To verify selecting of panel.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  sleep 2
  @grshomepg.selectPanel
end

And(/^I will be able to see the previously selected (.*) team options search criteria persist$/) do |teams|
  print "Step Name : To verify the previously selected "+teams.to_s+" team options search criteria persist.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @grshomepg.verifySelectedTeam(teams)

end