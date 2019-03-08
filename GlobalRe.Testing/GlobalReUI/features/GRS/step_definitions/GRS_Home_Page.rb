Given(/^I open the browser and navigate to GRS link$/) do
  print "Step Name: Opening browser and navigating to GRS Link.\n"
  #@loginpg = Login_page.new(@browser,@reporter)
  #@loginpg.loadwebpage
  #@loginpg.loginToApp("test","test")
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @grshomepg.loadwebpage
  # @grshomepg.loadwebpageInNewTab
  #sleep 20
end
Given(/^I open the browser and navigate as (.*) user to GRS link$/) do |user|
  print "Step Name: Opening browser and navigating to GRS Link as "+ user + " user.\n"
  #@loginpg = Login_page.new(@browser,@reporter)
  #@loginpg.loadwebpage
  #@loginpg.loginToApp("test","test")
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @grshomepg.loadUserwebpage(user)
  #sleep 20
end

Then(/^I see the GRS Homepage with the users userid$/) do
  print "Step Name: Verifying whether the GRS Homepage contains the users USERID.\n"
  @grshomepg.verifyGRSTitle
  @grshomepg.verifyUserID

  #modal = @browser.modal_dialog
  #modal.button(:id => 'close').click
  #sleep 20
end

Then(/^a panel is displayed with the wording (.*)$/) do |status|
  print "Step Name: Verifying whether a panel is displayed with the needed wording.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @panelElement = @grshomepg.fetchpanelElement(status)
  @elementText = status
  @grshomepg.verifyElementName(@panelElement,@elementText)
end


Then(/^a panel is displayed with the needed wording (.*) with the count of the Deals$/) do |status|
  print "Step Name: Verifying whether a panel is displayed with the " + status + " wording and the count of the deals.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @neededElement = @grshomepg.fetchpanelElement(status)
  @dbquery = DBQueries.new
  @sqlquery=@dbquery.getDBCountQuery(status).to_s
  @dbcount = @grshomepg.sendCountQuery(@sqlquery)
  @panelcount = @grshomepg.getpanelCount(@neededElement)
  @grshomepg.verifyCount(@panelcount,@dbcount)
end

Then(/^a panel is displayed with the needed wording (.*) with the count of the Deals of the (.*) user$/) do |status,user|
  print "Step Name: Verifying whether a panel is displayed with the " + status + " wording and the count of the deals of the " + user + " user.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @neededElement = @grshomepg.fetchpanelElement(status)
  @dbquery = DBQueries.new
  @sqlquery=@dbquery.getUserDBCountQuery(status,user).to_s
  print "\n"
  print @sqlquery
  print "\n"
  @dbcount = @grshomepg.sendCountQuery(@sqlquery)
  @panelcount = @grshomepg.getpanelCount(@neededElement)
  @grshomepg.verifyCount(@panelcount,@dbcount)
end

Given(/^I open the browser and navigate to GRS link and click on the (.*) Panel$/) do |status|
  print "Step Name: Open the browser and navigate to GRS link and click on the " + status + " panel.\n"
  @grshomepg = GRS_Home_Page.new(@browser,@reporter)
  @grshomepg.loadwebpage
  @status = status.split(',')
  sleep 3
  @status.each{|stat|
    #@loginpg.loginToApp("test","test")
    #Watir::Wait.until { @grshomepg.fetchpanelElement(status).visible? }
    #@grshomepg = GRS_Home_Page.new(@browser, @reporter)
    @clickelement = @grshomepg.fetchpanelElement(stat)
    #print @clickelement.text
    #@grshomepg.ClickOnHoldBtn
    @grshomepg.checkpanelAvailable(@clickelement)
    @grshomepg.ClickBtnwithCtrlKey(@clickelement)
    # @grshomepg.ClickBtn(@clickelement)
    #sleep 20
  }
end

Given(/^I open the browser and navigate as (.*) user to GRS link and click on the (.*) Panel$/) do |user,status|
  print "Step Name: Open the browser and navigate to GRS Link as " + user + " user and click on the " + status + " panel.\n"
  @grshomepg = GRS_Home_Page.new(@browser,@reporter)
  @grshomepg.loadUserwebpage(user)
  #@loginpg.loginToApp("test","test")
  sleep 4
  #Watir::Wait.until { @grshomepg.fetchpanelElement(status).visible? }
  #@grshomepg = GRS_Home_Page.new(@browser, @reporter)
  # print "\n"
  # print status
  # print "\n"
  @clickelement = @grshomepg.fetchpanelElement(status)
  #@grshomepg.ClickOnHoldBtn
  @grshomepg.checkpanelAvailable(@clickelement)
  @grshomepg.ClickBtn(@clickelement)
  #sleep 20
  #@@Testuser = user
end

And(/^the deals of (.*) is displayed in the Grid below in the descending order$/) do |status|
  print "Step Name: Verify whether the deals of the " + status + " status is displayed in the GRID and the deals are in descending order.\n"
  @grshomepg = GRS_Home_Page.new(@browser,@reporter)
  @dbquery = DBQueries.new
  @sqlquery=@dbquery.getDBQuery(status).to_s
  #print "\n"
  #print @sqlquery
  #print "\n"
  @dbresult = @grshomepg.sendQuery(@sqlquery)
  #print "\n"
  #print @dbresult
  #print "\n"
  @grshomepg.checkDealGridAvailable
  sleep 1
  @dealgrid = @grshomepg.fetchDealGridtable #fetchDealGridListElement#fetchGridElement#fetchDealGridElement
  sleep 1
  @dealgridColumns = @grshomepg.fetchDealGridColumns
  @dealGridColCount  = @dealgridColumns.length
  @GridData = @grshomepg.getGridValues(@dealgrid,@dealGridColCount)
  #print "\n"
  #print @GridData
  #print "\n"
  @GridDataModified = @grshomepg.ModifyGridCellData(@GridData)
  # print "\n"
  # print @GridDataModified
  # print "\n"
  # print @dbresult
  # print "\n"
  @grshomepg.CompareResults(@GridDataModified,@dbresult)
  @grshomepg.verifyDescendingOrderDealNumber(@GridData)
end

And(/^the deals of (.*) for the (.*) user are displayed in the Grid below in the descending order$/) do |status,user|
  print "Step Name: Verify whether the deals of the " + status + " status for the " + user + " are displayed in the GRID and the deals are in descending order.\n"
  @grshomepg = GRS_Home_Page.new(@browser,@reporter)
  @dbquery = DBQueries.new
  @sqlquery=@dbquery.getUserDBQuery(status,user).to_s
  print "\n"
  print @sqlquery
  print "\n"
  @dbresult = @grshomepg.sendQuery(@sqlquery)
  # print "\n"
  # print @dbresult
  # print "\n"
  sleep 4
  @grshomepg.checkDealGridAvailable
  #sleep 4
  @ModifiedDBResult = @grshomepg.modifyDBResult(@dbresult)

  puts "Modified DB Results"
  puts @ModifiedDBResult

  @dealgrid = @grshomepg.fetchDealGridtable
  #------Fetching Deal Colum count
  @dealgridColumns = @grshomepg.fetchDealGridColumns
  @dealGridColCount  = @dealgridColumns.length
  #Watir::Wait.until { @dealgrid.exists? }
  @GridData = @grshomepg.getGridValues(@dealgrid,@dealGridColCount)
  # sleep 3
  # @dealgrid = @grshomepg.fetchDealGridtable #fetchDealGridListElement#fetchGridElement#fetchDealGridElement
  # sleep 1
  # @GridData = @grshomepg.getGridValues(@dealgrid)
  # print "\n"
  # print @GridData
  # print "\n"
  # @GridDataModified = @grshomepg.ModifyAllGridCellData(@GridData)
  # -------Added New function to accomodate this functionaltity -------
  @GridDataModified = @grshomepg.GetModifyGridCellData(@GridData,@dealgridColumns)
  # print "\n"
  # print @GridDataModified
  # print "\n"
  # print @ModifiedDBResult
  # print "\n"

  @grshomepg.CompareResults(@GridDataModified.to_s,@ModifiedDBResult.to_s)
  @grshomepg.verifyDescendingOrderDealNumber(@GridData)
end


And(/^I see the grid displayed below with the deals having (.*) status$/) do |status|
  print "Step Name: Verify whether the grid is displayed with deals in the " + status +  " status.\n"
  @grshomepg = GRS_Home_Page.new(@browser,@reporter)

  @GridListElement = @grshomepg.fetchDealGridListElement
  Watir::Wait.until { @GridListElement.visible? }
  @grshomepg.GridAvailable(@GridListElement)
  #sleep 3
  @dealgrid = @grshomepg.fetchDealGridtable
  @dealgridColumns = @grshomepg.fetchDealGridColumns #Boya Added
  @dealGridColCount  = @dealgridColumns.length #Boya Added
  # Watir::Wait.until { @dealgrid.exists? }
  @GridData = @grshomepg.getGridValues(@dealgrid,@dealGridColCount)
  @Status = status
  #sleep 1
  @grshomepg.VerifyGridDataDealStatus(@GridData, @Status)
end

And(/^I click on a column label$/) do
  print "Step Name: Click on column label.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @dealnameelement = @grshomepg.fetchdealnamegridcolumnElement
  @grshomepg.ClickGridCol(@dealnameelement)
  #@grshomepg.VerifyAscendingSort()
  #@dealnumberelement = @grshomepg.fetchdealnumbergridcolumnElement
  #@grshomepg.ClickGridCol(@dealnumberelement)
  #@contractnumberelement = @grshomepg.fetchcontractnumbergridcolumnElement
  #@grshomepg.ClickGridCol(@contractnumberelement)
  #@uwnameelement = @grshomepg.fetchUWNamegridcolumnElement
  #@grshomepg.ClickGridCol(@uwnameelement)
  #@dealgridelement = @grshomepg.fetchGridElement
  #@grshomepg.VerifyAscendingSort(@dealgridelement)
  #@grshomepg.VerifyAscendingSort(@dealnameelement)

end

And(/^I will be able to see the data sorted descending$/) do
  print "Step Name: Verify whether the grid is displayed with deals sorted on descending order.\n"
  @grshomepg.checkDealGridAvailable
  sleep 1
  @dealgrid = @grshomepg.fetchDealGridtable#fetchDealGridListElement#fetchGridElement#fetchDealGridElement
  sleep 1
  #@dealgrid.wait_until_present
  @dealgridColumns = @grshomepg.fetchDealGridColumns
  @dealGridColCount  = @dealgridColumns.length
  @GridData = @grshomepg.getGridValues(@dealgrid,@dealGridColCount)
  #@GridDataModified = @grshomepg.ModifyGridData(@GridData)
  @grshomepg.verifyDescendingOrderDealName(@GridData)

end

Then(/^I will be able to see the data sorted ascending$/) do
  print "Step Name: Verify whether the grid is displayed with deals sorted on ascending order.\n"
  @grshomepg.checkDealGridAvailable
  sleep 1
  @dealgrid = @grshomepg.fetchDealGridtable#fetchDealGridListElement#fetchGridElement#fetchDealGridElement
  sleep 1
  @dealgridColumns = @grshomepg.fetchDealGridColumns
  @dealGridColCount  = @dealgridColumns.length
  @GridData = @grshomepg.getGridValues(@dealgrid,@dealGridColCount)
  #@GridDataModified = @grshomepg.ModifyGridData(@GridData)
  @grshomepg.verifyAscendingOrderDealName(@GridData)

end

And(/^I select a label of the column on the left and drag the column to right and I should see the column moved to the right of the screen$/) do
  print "Step Name: Drag the column from left to right and verify whether the column is moved.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @dealnameelement = @grshomepg.fetchdealnamegridcolumnElement
  @statuselement = @grshomepg.fetchstatusgridcolumnElement
  @BeforeLocation = @grshomepg.verifyLocation(@dealnameelement)
  @grshomepg.draganelement(@dealnameelement,@statuselement)
  @Afterlocation = @grshomepg.verifyLocation(@dealnameelement)
  @grshomepg.verifyRightMove(@BeforeLocation,@Afterlocation)
end

Then(/^I select a label of the column on the right and drag the column to left I should see the column moved to the left of the screen$/) do
  print "Step Name: Drag the column from right to left and verify whether the column is moved.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @submittedelement = @grshomepg.fetchsubmittedgridcolumnElement
  @inceptionelement = @grshomepg.fetchinceptiongridcolumnElement
  @BeforeLocation = @grshomepg.verifyLocation(@submittedelement)
  @grshomepg.draganelement(@submittedelement,@inceptionelement)
  @Afterlocation = @grshomepg.verifyLocation(@submittedelement)
  @grshomepg.verifyLeftMove(@BeforeLocation,@Afterlocation)
end

When(/^the GRS Homepage is displayed$/) do
  print "Step Name: Verify whether the GRS Homepage is displayed.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @grshomepg.verifyGRSTitle
  @grshomepg.verifyUserID
end

Then(/^the deals details displayed has the needed set of fields$/) do
  print "Step Name: Verify whether the GRID displayed has the needed details of fields.\n"
  @grshomepg.VerifyNeededFieldsAvailable
end

Then(/^the (.*) status deals details displayed has the needed set of fields for the specific (.*) user$/) do |status,user|
  print "Step Name: Verify whether the GRID displayed has the " + status + " status deal with the needed details in fields for the " + user + " user.\n"
  @grshomepg.VerifyNeededFieldsAvailable
  sleep 3
  @dealgrid = @grshomepg.fetchDealGridtable
  @dealgridColumns = @grshomepg.fetchDealGridColumns
  @dealGridColCount  = @dealgridColumns.length
  @GridData = @grshomepg.getGridValues(@dealgrid,@dealGridColCount)
  @grshomepg.VerifyGridDataIsOfUser(@GridData,user,status)
end

And(/^I see the QuickLinks link and Clicked$/) do
  print "Step Name: Verify whether the GRS Homepage is displayed with the QuickLinks .\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @quickLinksElement = @grshomepg.fetchQuickLinksElement
  @grshomepg.verifyElementExist(@quickLinksElement)
  @grshomepg.ClickMenuBtn(@quickLinksElement)
end


And(/^I see the QlikView link$/) do
  print "Step Name: Verify whether the GRS Homepage is displayed with the QlikView Link.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @quickViewBtnElement = @grshomepg.fetchQlikViewBtnElement
  @grshomepg.verifyElementExist(@quickViewBtnElement)
end

And(/^I click on the QlikView link$/) do
  print "Step Name: Click on the QlikView Link.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @quickViewBtnElement = @grshomepg.fetchQlikViewBtnElement
  @grshomepg.ClickMenuBtn(@quickViewBtnElement)
end

Then(/^I will be able to see in Global Re Dashboard launched in new browser window$/) do
  print "Step Name: Verify the Qlikview opened in a new browser.\n"
  #@newbrowserwindow = @browser.window(:title, /Global Re Dashboard/)
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @grshomepg.VerifyQlikView()
end

And(/^I see the ERMS link$/) do
  print "Step Name: Verify whether the GRS Homepage is displayed with the ERMS Link.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @ERMSBtnElement = @grshomepg.fetchERMSBtnElement
  @grshomepg.verifyElementExist(@ERMSBtnElement)
  #@grshomepg.get_process_info
end

And(/^I click on the ERMS link$/) do
  print "Step Name: Click on the ERMS Link.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @ERMSBtnElement = @grshomepg.fetchERMSBtnElement
  @grshomepg.ClickMenuBtn(@ERMSBtnElement)
end

And(/^I click on the Tool menu in the column heading$/) do
  print "Step Name: Click on column heading menu.\n"
  @fieldElement = @grshomepg.fetchdealnumbergridcolumnElement
  @ElementToBEClicked = @grshomepg.fetchMenuFieldElement(@fieldElement)
  # @grshomepg.ClickGridCol(@ElementToBEClicked)
end

And(/^I click on the Tool menu Link in the column heading$/) do
  print "Step Name: Click on column heading menu.\n"
  @fieldElement = @grshomepg.fetchdealnumbergridcolumnElement
  @ElementToBEClicked = @grshomepg.fetchMenuFieldElement(@fieldElement)
  @grshomepg.ClickGridCol(@ElementToBEClicked)
end

And(/^I will be able to see tool menu popup$/) do
  print "Verify whether the tool menu is displayed.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @grshomepg.verifyToolMenuAvailability

end

And(/^I click on the columns tab within the Tool menu popup$/) do
  print "Click on the columns tab of the Tool Menu.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  sleep 1
  @ToolMenuHeaderTabElement = @grshomepg.fetchToolMenuHeaderTabelement
  @grshomepg.ClickAgToolMenuColumnButton(@ToolMenuHeaderTabElement)
  #sleep 10
end

And(/^I will be able to see grid column names with checkboxes$/) do
  print "Verify whether the Column Select Panel is displayed.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @ElementtobChecked = @grshomepg.fetchToolMenuColumnSelectPanelelement
  @ElementName = "Tool Menu Column Select Panel"
  @grshomepg.verifyElementExistonPage(@ElementtobChecked, @ElementName)
  #@FieldtoBeChecked = @grshomepg.fetchdealnameToolMenuFieldElement
  #print "\n"
  #print @FieldtoBeChecked.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
  #print "\n"
  #@FieldtoBeChecked = @grshomepg.fetchExpirationToolMenuFieldElement
  #print "\n"
  #print @FieldtoBeChecked.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
  #print "\n"

  #yet to do the verification of selected fields in panel. Can be done using the child span firstElementChild has .ag-hidden
end

And(/^I will be able to see column checkboxes are checked for columns displayed in the grid$/) do
  print "Verify whether the Column Select Panel is displayed with the fields available in grid as checked.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  #@browser.driver.execute_script("body.style.zoom='80%'")
  #@wsh = WIN32OLE.new('Wscript.Shell')
  #@wsh.SendKeys("^-")
  #@browser.send_keys[:control,:subtract]*3
  #sleep 2
  # @fieldElement = @grshomepg.fetchdealnamegridcolumnElement
  # @ElementToBEClicked = @grshomepg.fetchMenuFieldElement(@fieldElement)
  # @grshomepg.ClickGridCol(@ElementToBEClicked)
  # @ToolMenuHeaderTabElement = @grshomepg.fetchToolMenuHeaderTabelement
  # @grshomepg.ClickAgToolMenuColumnButton(@ToolMenuHeaderTabElement)
  @grshomepg.verifyGridFieldscheckedInToolsColMenu
  #sleep 2
  #@wsh = WIN32OLE.new('Wscript.Shell')
  #@wsh.SendKeys("^{++}")
  #@browser.send_keys[:control,:add]*3
  #@browser.driver.execute_script("document.body.style.zoom='100%'")
end

And(/^I will be able to see column checkboxes are unchecked for columns not displayed in the grid$/) do
  print "Verify whether the Column Select Panel is displayed with the fields not available in grid as unchecked.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  #@browser.driver.execute_script("body.style.zoom='80%'")
  #@wsh = WIN32OLE.new('Wscript.Shell')
  #@wsh.SendKeys("^-")
  #sleep 2
  # @fieldElement = @grshomepg.fetchdealnamegridcolumnElement
  # @ElementToBEClicked = @grshomepg.fetchMenuFieldElement(@fieldElement)
  # @grshomepg.ClickGridCol(@ElementToBEClicked)
  # @ToolMenuHeaderTabElement = @grshomepg.fetchToolMenuHeaderTabelement
  # @grshomepg.ClickAgToolMenuColumnButton(@ToolMenuHeaderTabElement)
  @grshomepg.verifyGridFieldsNotAvailableUncheckedInToolsColMenu
  #sleep 2
  #@wsh = WIN32OLE.new('Wscript.Shell')
  #@wsh.SendKeys("^{++}")
    #@browser.send_keys[:control,:add]*3
  #@browser.driver.execute_script("document.body.style.zoom='100%'")
end

And(/^I click to uncheck the checkbox of the (.*) column not to be displayed in the grid$/) do |columnname|
  print "Unselect Tool Menu column " + columnname + ".\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  sleep 1
  # @ToolMenuBodyElement = @grshomepg.fetchToolMenuBodyelement
  # @ToolMenuBodyElement.click
  # @browser.send_keys :arrow_down
  # @browser.send_keys :arrow_down
  # @browser.send_keys :arrow_down
  # @browser.send_keys :arrow_down
  # @browser.send_keys :arrow_down
  # @browser.send_keys :arrow_down
  # @browser.send_keys :arrow_down
  # @browser.send_keys :arrow_down
  @ToolMenuFieldElement = @grshomepg.fetchtoolMenuFieldElement(columnname)
  # print "\n"
  # print @ToolMenuFieldElement.text
  # print "\n"
  sleep 1
  @grshomepg.unselectToolMenuFieldElement(@ToolMenuFieldElement)
  #@grshomepg.fetchtargetdateToolMenuFieldElement.click
  #@grshomepg.fetchpriorityToolMenuFieldElement.click
  #@browser.driver.execute_script("body.style.zoom='80%'")
  #@wsh = WIN32OLE.new('Wscript.Shell')
  #@wsh.SendKeys("^-")
  sleep 5

  #@grshomepg.verifyGridFieldUncheckedInToolsColMenuisNotAvailableinGRID(@ToolMenuFieldElement,columnname)
  #sleep 2
  #@wsh = WIN32OLE.new('Wscript.Shell')
  #@wsh.SendKeys("^{++}")
    #@browser.send_keys[:control,:add]*3
  #@browser.driver.execute_script("document.body.style.zoom='100%'")
end

And(/^I click to uncheck the checkbox of the (.*) column not to be displayed in the grid Using Tool Panel$/) do |columnname|
  print "Unselect Tool Menu column " + columnname + ".\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  sleep 1
  # @ToolMenuBodyElement = @grshomepg.fetchToolMenuBodyelement
  # @ToolMenuBodyElement.click
  # @browser.send_keys :arrow_down
  # @browser.send_keys :arrow_down
  # @browser.send_keys :arrow_down
  # @browser.send_keys :arrow_down
  # @browser.send_keys :arrow_down
  # @browser.send_keys :arrow_down
  # @browser.send_keys :arrow_down
  # @browser.send_keys :arrow_down
  @ToolMenuFieldElement = @grshomepg.fetchtoolMenuFieldElementFromGrid(columnname)
  # print "\n"
  # print @ToolMenuFieldElement.text
  # print "\n"
  # sleep 1
  # @grshomepg.unselectToolMenuField(@ToolMenuFieldElement)
  #@grshomepg.fetchtargetdateToolMenuFieldElement.click
  #@grshomepg.fetchpriorityToolMenuFieldElement.click
  #@browser.driver.execute_script("body.style.zoom='80%'")
  #@wsh = WIN32OLE.new('Wscript.Shell')
  #@wsh.SendKeys("^-")
  sleep 5

  #@grshomepg.verifyGridFieldUncheckedInToolsColMenuisNotAvailableinGRID(@ToolMenuFieldElement,columnname)
  #sleep 2
  #@wsh = WIN32OLE.new('Wscript.Shell')
  #@wsh.SendKeys("^{++}")
  #@browser.send_keys[:control,:add]*3
  #@browser.driver.execute_script("document.body.style.zoom='100%'")
end


Then(/^I will be able to see the grid not displaying the (.*) column$/) do |columnname|
  print "Verify whether the unselected Tool Menu column " + columnname + " is no longer displayed in the grid.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @arrayVal1 = columnname.split('*')
  puts @arrayVal1.to_s
  @arrayVal1.each { |row|
    # @ElementName = @grshomepg.fetchGridFieldElement(columnname)
    # @grshomepg.verifyGridFieldNotAvailableinGRID(@ElementName,columnname)
    @ToolMenuFieldElement = @grshomepg.fetchtoolMenuFieldElement(row)
    @grshomepg.verifyGridFieldUncheckedInToolsColMenuisNotAvailableinGRID(@ToolMenuFieldElement,row)
  }
end

Then(/^I will be able to see the grid not displaying the (.*) column in the grid$/) do |columnname|
  print "Verify whether the unselected Tool Menu column " + columnname + " is no longer displayed in the grid.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @arrayVal1 = columnname.split('*')
  puts @arrayVal1.to_s
  @arrayVal1.each { |row|
    # @ElementName = @grshomepg.fetchGridFieldElement(columnname)
    # @grshomepg.verifyGridFieldNotAvailableinGRID(@ElementName,columnname)
    @ToolMenuFieldElement = @grshomepg.fetchtoolMenuFieldElement(row)
    @grshomepg.verifyGridFieldUncheckedInGRIDColMenuisNotAvailableinGRID(@ToolMenuFieldElement,row)
  }
end

Then(/^I will be able to see the ERMS application launched$/) do
  print "Step Name: Verify the ERMS is opened.\n"
  #@newbrowserwindow = @browser.window(:url, /ERMSHome.Shell.exe alias=ONEDEV/)
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  sleep 4
  @grshomepg.VerifyERMSLaunch
end

#Then(/^i will not be able to see the ERMS application launched$/) do

#end

Then(/^the panel will display the list of (.*) wordings with the count of (.*) substatus deals$/) do |status,substatus|
  print "Step Name: Verifying whether a substatus " + substatus + " is displayed with the wording and the count of the deals.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @neededElement = @grshomepg.fetchpanelSubStatusElement(substatus)
  @dbquery = DBQueries.new
  @sqlquery=@dbquery.getDBCountQuery(substatus).to_s
  @dbcount = @grshomepg.sendCountQuery(@sqlquery)
  @panelcountelement = @grshomepg.getpanelCountElement(@neededElement)
  @panelcount = @grshomepg.getsubstatusCount(@panelcountelement)
  @grshomepg.verifyCount(@panelcount,@dbcount)
end

Then(/^the In Progress panel will display the (.*) panel if count greater than zero as enabled and selected by default$/) do |substatus|
  print "Step Name: Verify whether the " + substatus +  " sub status displayed is disabled and unselected if the count of the deals for the " + substatus + " substatus is zero.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  #@ElementName = substatus + " Check Box"
  @neededElement = @grshomepg.fetchpanelSubStatusElement(substatus)
  @ElementInputtobChecked = @grshomepg.fetchpanelSubStatusElementInput(substatus)
  @panelcountelement = @grshomepg.getpanelCountElement(@neededElement)
  @panelcount = @grshomepg.getsubstatusCount(@panelcountelement)
  @grshomepg.verifyZerocountandDisabled(substatus,@panelcount,@neededElement)
  @grshomepg.verifyZerocountandUnSelected(substatus,@panelcount,@neededElement,@ElementInputtobChecked)
end

Then(/^the panel will display the list of (.*) wordings when the (.*) Panel is clicked$/) do |substatus, status|
  print "Step Name: Verify whether the " + substatus +  " sub status is displayed when the " + status + " panel is clicked.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @subStatus = substatus.split('*')
  @subStatus.each { |substat|
    @ElementName = substat + " Check Box"
    @ElementtobeChecked = @grshomepg.fetchpanelSubStatusElement(substat)
    #print @ElementtobChecked.text
    @grshomepg.verifyElementExistonPage(@ElementtobeChecked,@ElementName)
    @grshomepg.VerifyElementText(@ElementtobeChecked,substat)
  }
end

Then(/^the panel will not display the list of (.*) wordings when the (.*) Panel is clicked$/) do |substatus, status|
  print "Step Name: Verify whether the " + substatus +  " sub status is displayed when the " + status + " panel is clicked.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @subStatus = substatus.split('*')
  @subStatus.each { |substat|
    @ElementName = substat + " Check Box"
    @ElementtobeChecked = @grshomepg.fetchpanelSubStatusElement(substat)
    #print @ElementtobChecked.text
    @grshomepg.verifyElementDoesNotExistonPage(@ElementtobeChecked,@ElementName)
    # @grshomepg.VerifyElementText(@ElementtobeChecked,substat)
  }
end

And(/^I uncheck all other substatus apart from (.*) substatus$/) do |substatus|
  print "Step Name: Uncheck all the substatus except the " + substatus +  " sub status.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @grshomepg.unselectallsubstatusCheckbox
  @paneltobeselected = @grshomepg.fetchpanelSubStatusElement(substatus)
  @grshomepg.ClickBtn(@paneltobeselected)
  # sleep 10
end

Then(/^the total count on the (.*) panel should be updated with the number of (.*) deals$/) do |status, substatus|
  print "Step Name: Verifying whether a panel is displayed with the " + status + " wording and the count of the " + substatus + " deals.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  sleep 5
  @neededElement = @grshomepg.fetchpanelElement(status)
  @dbquery = DBQueries.new
  @sqlquery=@dbquery.getDBCountQuery(substatus).to_s
  @dbcount = @grshomepg.sendCountQuery(@sqlquery)
  @panelcount = @grshomepg.getpanelCount(@neededElement)
  @grshomepg.verifyCount(@panelcount,@dbcount)
end

Then(/^the grid displaying the deals should now only show (.*) deals$/) do |substatus|
  print "Step Name: Verify whether the grid is displayed with deals in the " + substatus +  " substatus.\n"
  @grshomepg = GRS_Home_Page.new(@browser,@reporter)
  @GridListElement = @grshomepg.fetchDealGridListElement
  @grshomepg.GridAvailable(@GridListElement)
  @gridTableLoad = @grshomepg.fetchGridTableLoadingElement
  BrowserContainer.WaitUntilElementLoad(@gridTableLoad)
  @dealgrid = @grshomepg.fetchDealGridtable
  @dealgridColumns = @grshomepg.fetchDealGridColumns
  @dealGridColCount  = @dealgridColumns.length
  puts @dealGridColCount
  @GridData = @grshomepg.getGridValues(@dealgrid,@dealGridColCount)
  @SubStatus = substatus
  sleep 1
  @grshomepg.VerifyGridDataDealStatus(@GridData, @SubStatus)

end

And(/^I uncheck all other statuses apart from (.*) and (.*) substatus$/) do |substatus1, substatus2|
  print "Step Name: Uncheck all the substatus except the " + substatus1 + " and " + substatus2 + " sub status.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @grshomepg.unselectallsubstatusCheckbox
  @substatus1tobeselected = @grshomepg.fetchpanelSubStatusElementInput(substatus1)
  @substatus2tobeselected = @grshomepg.fetchpanelSubStatusElementInput(substatus2)
  @grshomepg.ClickBtn(@substatus1tobeselected)
  @grshomepg.ClickBtn(@substatus2tobeselected)
end

Then(/^the total count on the (.*) status panel should be updated with the count of (.*) and (.*) substatus deals$/) do |status, substatus1, substatus2|
  print "Step Name: Verifying whether a panel is displayed with the " + status + " wording and the total of the count of the " + substatus1 + " and " + substatus2 + " deals.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @neededElement = @grshomepg.fetchpanelElement(status)
  @dbquery = DBQueries.new
  @sqlquery1=@dbquery.getDBCountQuery(substatus1).to_s
  @dbcount1 = @grshomepg.sendCountQuery(@sqlquery1)
  @sqlquery2=@dbquery.getDBCountQuery(substatus2).to_s
  @dbcount2 = @grshomepg.sendCountQuery(@sqlquery2)
  @dbcount = @dbcount1.to_i + @dbcount2.to_i
  sleep 2
  @panelcount = @grshomepg.getpanelCount(@neededElement)
  @grshomepg.verifyCount(@panelcount,@dbcount.to_s)
end

Then(/^the total count on the (.*) status panel should be updated with the count of (.*) and (.*) substatus deals for the (.*) user$/) do |status, substatus1, substatus2,user|
  print "Step Name: Verifying whether a panel is displayed with the " + status + " wording and the total of the count of the " + substatus1 + " and " + substatus2 + " deals for the " + user + " user.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @neededElement = @grshomepg.fetchpanelElement(status)
  @dbquery = DBQueries.new
  @sqlquery1=@dbquery.getUserDBCountQuery(substatus1,user).to_s
  @dbcount1 = @grshomepg.sendCountQuery(@sqlquery1)
  @sqlquery2=@dbquery.getUserDBCountQuery(substatus2,user).to_s
  @dbcount2 = @grshomepg.sendCountQuery(@sqlquery2)
  @dbcount = @dbcount1.to_i + @dbcount2.to_i
  sleep 2
  @panelcount = @grshomepg.getpanelCount(@neededElement)
  @grshomepg.verifyCount(@panelcount,@dbcount.to_s)
end

Then(/^the grid displaying the deals should now only show specific deals of (.*) and (.*) substatus when the (.*) status panel is selected$/) do |substatus1, substatus2, status|
  print "Step Name: Verify whether the grid displayed selecting the " + status + " panel displays deals in the " + substatus1 + " or " +  substatus2 +" substatus.\n"
  @grshomepg = GRS_Home_Page.new(@browser,@reporter)
  @GridListElement = @grshomepg.fetchDealGridListElement
  sleep 3
  @grshomepg.GridAvailable(@GridListElement)
  #sleep 6
  @dealgrid = @grshomepg.fetchDealGridtable
  @dealgridColumns = @grshomepg.fetchDealGridColumns
  @dealGridColCount  = @dealgridColumns.length
  @GridData = @grshomepg.getGridValues(@dealgrid,@dealGridColCount)
  @Status = status
  @SubStatus1 = substatus1
  @SubStatus2 = substatus2
  #print "\n"
  #print @GridData
  #print "\n"
  sleep 1
  @grshomepg.VerifyGridDataMultipleDealStatus(@GridData, @Status, @SubStatus1, @SubStatus2)

end
Then(/^the grid displaying the deals should now only show specific deals of (.*) and (.*) substatus of the (.*) user when the (.*) status panel is selected$/) do |substatus1, substatus2, user,status|
  print "Step Name: Verify whether the grid displayed selecting the " + status + " panel displays deals in the " + substatus1 + " or " +  substatus2 +" substatus for the specific " + user + " user.\n"
  @grshomepg = GRS_Home_Page.new(@browser,@reporter)
  sleep 4
  @GridListElement = @grshomepg.fetchDealGridListElement
  @grshomepg.GridAvailableforstatus(@GridListElement,status)
  #sleep 6
  @dealgrid = @grshomepg.fetchDealGridtable
  @dealgridColumns = @grshomepg.fetchDealGridColumns
  @dealGridColCount  = @dealgridColumns.length
  @GridData = @grshomepg.getGridValues(@dealgrid,@dealGridColCount)
  @Status = status
  @SubStatus1 = substatus1
  @SubStatus2 = substatus2
  #print "\n"
  #print @GridData
  #print "\n"
  #sleep 1
  @grshomepg.VerifyGridDataMultipleDealStatus(@GridData, @Status, @SubStatus1, @SubStatus2)
  @grshomepg.VerifyGridDataIsOfUser(@GridData,user,status)

end

And(/^the needed (.*) substatus are displayed when the (.*) panel is clicked$/) do |substatus, status|
  print "Step Name: Verifying whether the needed substatus " + substatus + " are displayed when the " + status + " status panel is clicked.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @substatusarr = substatus.split(",")
  #print @substatusarr
  @grshomepg.VerifySubstatusIsdisplayed(@substatusarr)
end

Then(/^I see a (.*) as disabled if the count is zero$/) do |substatus|
  print "Step Name: Verifying whether the substatus " + substatus + " disabled if the count is zero.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @substatusarr = substatus.split(",")
  #print @substatusarr
  @grshomepg.VerifySubstatusIsChecked(@substatusarr)
end

Then(/^the (.*) with count greater than zero should be enabled and selected by default$/) do |substatus|
  print "Step Name: Verifying whether the substatus " + substatus + " with count greater than zero should be enabled and selected by default.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @substatusarr = substatus.split(",")
  #print @substatusarr
  @grshomepg.VerifySubstatusIsChecked(@substatusarr)
end

And(/^I click to select the Tool Panel option$/) do
  print "Step Name: Select the Tool Panel option from the Tool Menu.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @ElementToBEClicked = @grshomepg.fetchToolMenuToolPanelOptionelement
  @grshomepg.ClickMenuBtn(@ElementToBEClicked)
  #@slidebar = @grshomepg.fetchGridRightFloatingBottomElement
  #@grshomepg.ClickBtn(@slidebar)
  #@GridListElement = @grshomepg.fetchDealGridListElement
  #@GridListElement.scroll.to.by 0,1000

end

And(/^I will be able to see Tool Panel displayed on the right side of the grid$/) do
  print "Step Name: Verifying whether the tool panel is displayed on the rightside of the grid.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @ElementtobChecked = @grshomepg.fetchToolMenuColumnSelectPanelelement
  @ElementName = "Tool Menu Column Select Panel"
  @grshomepg.verifyElementExistonPage(@ElementtobChecked, @ElementName)
end

And(/^I drag the (.*) column out of the grid$/) do |columnname|
  print "Step Name: Drag the " + columnname + " column out of the grid.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @elementtobemoved = @grshomepg.fetchGridFieldElement(columnname)
  @AppNameElement = @browser.element(:id => "shellContainer_ApplicationName")
  #@elementtowheremoved = @grshomepg.fetchActuarygridcolumnElement
  #print "\n"
  #print @elementtowheremoved.wd.location
  #print "\n"
  ######@grshomepg.draganelementtolocation(@elementtobemoved,0,-500)
  @grshomepg.draganelement(@elementtobemoved,@AppNameElement)

  sleep 3
end

Then(/^removed column (.*) is not displayed in the grid$/) do |columnname|
  print "Verify whether the column " + columnname + " dragged out of the grid is no longer available in the grid.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  sleep 7
  @gridFieldElement = @grshomepg.fetchGridFieldElement(columnname)
  #@grshomepg.unselectToolMenuFieldElement(@ToolMenuFieldElement)
  #@browser.driver.execute_script("body.style.zoom='80%'")
  #@wsh = WIN32OLE.new('Wscript.Shell')
  #@wsh.SendKeys("^-")
  #sleep 2
  @grshomepg.verifyGridFieldNotAvailableinGRID(@gridFieldElement,columnname)
  #sleep 2
  #@wsh = WIN32OLE.new('Wscript.Shell')
  #@wsh.SendKeys("^{++}")
  #@browser.send_keys[:control,:add]*3
  #@browser.driver.execute_script("document.body.style.zoom='100%'")
end

And(/^I select the checkbox to check the (.*) column to be displayed in the grid$/) do |columnname|
  print "Select Tool Menu column " + columnname + ".\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  sleep 1
  @ToolMenuFieldElement = @grshomepg.fetchtoolMenuFieldElement(columnname)
  #print "\n"
  #print @ToolMenuFieldElement.text
  #print "\n"
  sleep 1
  @grshomepg.selectToolMenuFieldElement(@ToolMenuFieldElement)
  #@grshomepg.fetchtargetdateToolMenuFieldElement.click
  #@grshomepg.fetchpriorityToolMenuFieldElement.click
  #@browser.driver.execute_script("body.style.zoom='80%'")
  #@wsh = WIN32OLE.new('Wscript.Shell')
  #@wsh.SendKeys("^-")
  #sleep 2

  #@grshomepg.verifyGridFieldUncheckedInToolsColMenuisNotAvailableinGRID(@ToolMenuFieldElement,columnname)
  #sleep 2
  #@wsh = WIN32OLE.new('Wscript.Shell')
  #@wsh.SendKeys("^{++}")
  #@browser.send_keys[:control,:add]*3
  #@browser.driver.execute_script("document.body.style.zoom='100%'")

end

Then(/^I will be able to see the grid displaying the (.*) column$/) do |columnname|
  print "Verify whether the Column Select Panel is displayed with the " + columnname + " field available in grid as checked.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  sleep 2
  @Elementtobeverified = @grshomepg.fetchGridFieldElement(columnname)
  @grshomepg.verifyFieldAvailableinGRID(@Elementtobeverified,columnname)

end

And(/^I click to hold and drag the (.*) columns into the grid$/) do |columnname|
  print "Step Name: Drag the " + columnname + " column in to the grid.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @elementtobemoved = @grshomepg.fetchtoolMenuFieldElement(columnname)
  @elementtowheremoved = @grshomepg.fetchdealnumbergridcolumnElement
  #print "\n"
  #print @elementtowheremoved.wd.location
  #print "\n"
  @grshomepg.draganelement(@elementtobemoved,@elementtowheremoved)

  sleep 3

end

Then(/^I will be able to see the (.*) panel is grayed out and disabled if the count is zero and if available (.*) substatus count is zero$/) do |status,substatus|
  print "Step Name: Verifying whether the " + status + " panel is grayed out and disabled if the count is zero  and if available " + substatus + " substatus count is zero.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  sleep 2
  @neededstatusElement = @grshomepg.fetchpanelElement(status)
  @panelcount = @grshomepg.getpanelCount(@neededstatusElement)
  @grshomepg.ClickBtn(@neededstatusElement)
  @substatusarr = substatus.split(",")
  #print @substatusarr
  @nonZeroSubstatuselements = @grshomepg.findSubstatushavingnonZeroCount(status,@substatusarr)
  @grshomepg.verifyPanelDisabled(status,@neededstatusElement,@panelcount,@nonZeroSubstatuselements)
end

And(/^I unselect all of the (.*) status (.*) substatuses having a count greater than zero$/) do |status,substatus|
  print "Unselect all of the " + status + " status " + substatus + " substatuses having a count greater than zero.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @neededstatusElement = @grshomepg.fetchpanelElement(status)
  #@panelcount = @grshomepg.getpanelCount(@neededstatusElement)
  sleep 2
  #@grshomepg.ClickBtn(@neededstatusElement)
  #sleep 3
  #@substatusarr = substatus.split(",")
  @grshomepg.uncheckallsubstatusCheckbox(substatus,status)
end

Then(/^I will be able to see the (.*) status panel is not disabled for selecting$/) do |status|
  print "Verify whether user will be able to see the " + status + " status panel is not disabled for selecting.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @neededstatusElement = @grshomepg.fetchpanelElement(status)
  #@panelcount = @grshomepg.getpanelCount(@neededstatusElement)
  @grshomepg.checkpanelnotDisabled(@neededstatusElement,status)
end

Given(/^I open the browser and navigate to GRS link and ctrl select on the (.*) and (.*) Panel$/) do |status1, status2|
  print "Step Name: Opening browser and navigating to GRS Link and ctrl select " + status1 + " and " + status2 + " status panels.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @grshomepg.loadwebpage
  @neededstatusElement1 = @grshomepg.fetchpanelElement(status1)
  @neededstatusElement2 = @grshomepg.fetchpanelElement(status2)
  # @grshomepg.ClickBtn(@neededstatusElement1)
  # @grshomepg.ClickBtn(@neededstatusElement2)
  @grshomepg.ClickBtnwithCtrlKey(@neededstatusElement1)
  @grshomepg.ClickBtnwithCtrlKey(@neededstatusElement2)
  print status1 + " and " + status2 + " status panels are successfully selected.\n"

end


Given(/^I open the browser and navigate to GRS link and select on the (.*) and (.*) Panel$/) do |status1, status2|
  print "Step Name: Opening browser and navigating to GRS Link and select " + status1 + " and " + status2 + " status panels.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @grshomepg.loadwebpage
  @neededstatusElement1 = @grshomepg.fetchpanelElement(status1)
  @neededstatusElement2 = @grshomepg.fetchpanelElement(status2)
  @grshomepg.ClickBtn(@neededstatusElement1)
  @grshomepg.ClickBtn(@neededstatusElement2)
  # @grshomepg.ClickBtnwithCtrlKey(@neededstatusElement1)
  # @grshomepg.ClickBtnwithCtrlKey(@neededstatusElement2)
  print status1 + " and " + status2 + " status panels are successfully selected.\n"

end

Then(/^the deals details displayed has the deals displayed with the status as (.*) or (.*)$/) do |status1, status2|
  print "Step Name: Verify whether the deals are displayed in the grid of " + status1 + " and " + status2 + " status.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @GridListElement = @grshomepg.fetchDealGridListElement
  @grshomepg.GridAvailable(@GridListElement)
  sleep 6
  @dealgrid = @grshomepg.fetchDealGridtable
  @dealgridColumns = @grshomepg.fetchDealGridColumns
  @dealGridColCount  = @dealgridColumns.length
  @GridData = @grshomepg.getGridValues(@dealgrid,@dealGridColCount)
  @Status = status1.to_s + ", " + status2.to_s
  @Neededstatus1 = status1
  @Neededstatus2 = status2
  #print "\n"
  #print @GridData
  #print "\n"
  #sleep 1
  @grshomepg.VerifyGridDataMultipleDealStatus(@GridData, @Status, @Neededstatus1, @Neededstatus2)
end

Then(/^I ctrl unselect the (.*) and the (.*) only remains selected and the deals displayed is of the selected status$/) do |status1, status2|
  print "Step Name: Verify whether the deals are displayed in the grid of " + status2 + " if I ctrl unselect the " + status1 + " status panel.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @neededstatusElement1 = @grshomepg.fetchpanelElement(status1)
  @neededstatusElement2 = @grshomepg.fetchpanelElement(status2)
  @grshomepg.ClickBtnwithCtrlKey(@neededstatusElement1)
  sleep 6
  @dealgrid = @grshomepg.fetchDealGridtable
  @dealgridColumns = @grshomepg.fetchDealGridColumns
  @dealGridColCount  = @dealgridColumns.length
  @GridData = @grshomepg.getGridValues(@dealgrid,@dealGridColCount)
  @Status = status2
  #sleep 1
  @grshomepg.VerifyGridDataDealStatus(@GridData, @Status)
end

And(/^I ctrl click on the (.*) status panel$/) do |status|
  print "Step Name: Select the " + status + " status panel.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @neededstatusElement = @grshomepg.fetchpanelElement(status)
  #@grshomepg.ClickBtn(@neededstatusElement)
  @grshomepg.ClickBtnwithCtrlKey(@neededstatusElement)
  print status + " status panel is successfully selected.\n"
end

And(/^I click on the (.*) status panel$/) do |status|
  print "Step Name: Select the " + status + " status panel.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @neededstatusElement = @grshomepg.fetchpanelElement(status)
  @grshomepg.ClickBtn(@neededstatusElement)
  print status + " status panel is successfully selected.\n"
end

Then(/^I will be able to see previously selected (.*) and (.*) status panels get unselected and (.*) panel gets selected$/) do |status1, status2, status3|
  print "Step Name: Verify whether previously selected " + status1 + " and " + status2 + " status panels get unselected and " + status3 + " status panel gets selected.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @neededstatusElement1 = @grshomepg.fetchpanelElement(status1)
  @neededstatusElement2 = @grshomepg.fetchpanelElement(status2)
  @neededstatusElement3 = @grshomepg.fetchpanelElement(status3)
  @grshomepg.verifyPanelButtonUnChecked(@neededstatusElement1,status1)
  @grshomepg.verifyPanelButtonUnChecked(@neededstatusElement2,status2)
  @grshomepg.verifyPanelButtonChecked(@neededstatusElement3,status3)
end


Then(/^I will be able to see (.*) panel gets unselected and (.*) status panel get selected$/) do |status1, status2|
  print "Step Name: Verify whether "+ status1 + " status panel gets unselected and " + status2 + "status panel get selected.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @neededstatusElement1 = @grshomepg.fetchpanelElement(status1)
  sleep 2
  @neededstatusElement2 = @grshomepg.fetchpanelElement(status2)
  @grshomepg.verifyPanelButtonUnChecked(@neededstatusElement1,status1)
  @grshomepg.verifyPanelButtonChecked(@neededstatusElement2,status2)
end

Then(/^I will be able to see the counts of status panels refreshes with most recent counts$/) do
  print "Step Name: Verify whether counts of status panels refreshes with most recent counts.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @grshomepg.verifypanelCountsUpToDate
end


Then(/^I see the GRS Homepage with the users first name and last name$/) do
  print "Step Name: Verifying whether the GRS Homepage contains the users USERID.\n"
  @grshomepg.verifyGRSTitle
  @grshomepg.verifyUsersName
end

And(/^I click on the tool menu of the (.*) column$/) do |columnname|
  print "Step Name: Click on the tool menu " + columnname + " column.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @fieldElement = @grshomepg.fetchGridFieldElement(columnname)
  @ElementToBEClicked = @grshomepg.fetchMenuFieldElement(@fieldElement)
  @grshomepg.ClickGridCol(@ElementToBEClicked)
end


And(/^I click on the Filter tab within the tool menu popup$/) do
  print "Step Name: Click on the tool menu popup filter tab.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  sleep 5
  @ParentofElementtobeClicked = @grshomepg.fetchToolMenuHeaderelement
  @ElementToBEClicked = @ParentofElementtobeClicked.span(index:2)
  @grshomepg.ClickGridCol(@ElementToBEClicked)
end

And(/^I click on the Filter operator drop down with the (.*) list$/) do |operator|
  print "Step Name: select the " + operator + "filter operator.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @neededElement = @grshomepg.fetchGridAgColumnFilterElement
  @grshomepg.selectOperator(@neededElement,operator)
  #sleep 8
end

And(/^I select the (.*) operator and enter the (.*) filter text in the Filter text field$/) do |operator, textvalue|
  print "Step Name: select the " + operator + "filter operator and enter the " + textvalue + " value to be filtered on.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @neededElement = @grshomepg.fetchGridAgColumnFilterElement
  @grshomepg.selectOperator(@neededElement,operator)
  @grshomepg.inputFilterValue(textvalue)
end

And(/^I click on the Apply filter button$/) do
  print "Step Name: Click the Apply filter button.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @neededapplyelement = @grshomepg.fetchGridAgColumnFilterApplyButtonElement
  @grshomepg.ClickGridCol(@neededapplyelement)
end

Then(/^I will be able to see grid is filtered as per the (.*) column (.*) filter string and (.*) operator selected$/) do |columnname, textvalue, operator|
  print "Step Name: Verify grid data is as per the " + columnname + " column, filter value " + textvalue + " and " + operator + " operator selected.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @dealgrid = @grshomepg.fetchDealGridtable
  @dealgridColumns = @grshomepg.fetchDealGridColumns
  @dealGridColCount  = @dealgridColumns.length
  @GridData = @grshomepg.getGridValues(@dealgrid,@dealGridColCount)
  @grshomepg.verifyGridDataIsAsperFilter(@GridData,columnname,textvalue,operator)
end

And(/^I click on the corresponding (.*) selection$/) do |selection|
  print "Step Name: Click on the " + selection + " selection.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  sleep 3
  @SelectAllElement = @grshomepg.fetchSelectedFilterselectionElement("Select All")
  @grshomepg.ClickGridCol(@SelectAllElement)
  @SelectionElement = @grshomepg.fetchSelectedFilterselectionElement(selection.to_s)
  # print "\n"
  # print @SelectionElement.text
  # print "\n"
  #@CheckboxElementToBEClicked = @SelectionElement.parent
  @grshomepg.ClickGridCol(@SelectionElement)
end

Then(/^I will be able to see grid is filtered as per the (.*) column and (.*) selection$/) do |columnname, selection|
  print "Step Name: Verify grid data is as per the " + columnname + " column " + " and " + selection + " selection.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @dealgrid = @grshomepg.fetchDealGridtable
  @dealgridColumns = @grshomepg.fetchDealGridColumns
  @dealGridColCount  = @dealgridColumns.length
  @GridData = @grshomepg.getGridValues(@dealgrid,@dealGridColCount)
  @grshomepg.verifyGridDataIsAsperFilterSelection(@GridData,columnname,selection)
end

Then(/^I will be able to see default team options used by (.*) user are automatically selected$/) do |user|
  print "Step Name: Verify default team option automatic selections for the " + user + " user.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  sleep 3
  @grshomepg.verifyUserTeamSelection(user)
end

And(/^I right click on the grid click on the Export option and select the Excel Export option$/) do
  print "Step Name: Right click on the grid click on the Export option and select the Excel Export option.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  sleep 4
  BrowserContainer.fetchenv
  @grshomepg.deleteexportfiles($FirefoxExportFolder)
  @NeededGridCellElement = @grshomepg.fetchAgStatusGridElement
  @grshomepg.rightclickCell(@NeededGridCellElement)
  @NeededGridMenuElement = @grshomepg.fetchAgGridMenuElement
  @grshomepg.verifyGridMenuDisplayed(@NeededGridMenuElement)
  @NeededExportMenuItem = @grshomepg.fetchAgGridMenuExportOtionElement
  @grshomepg.ClickMenuBtn(@NeededExportMenuItem)
  @NeededExcelExportMenuItem = @grshomepg.fetchAgGridMenuExcelExportOtionElement
  @grshomepg.ClickMenuBtn(@NeededExcelExportMenuItem)
  sleep 10
  #@grshomepg.handleIESAVEASdownloadPopup
  @grshomepg.handleIESAVEASdownloadPopup



end

Then(/^UI grid data should be exported in Excel format$/) do

end


And(/^I filter the deal as a (.*) user and click on a deal having the dealnumber (.*) and I see the deal details displayed on the right side of the screen$/) do |user,dealnumber|
  print "Step Name: Filter the deal as a #{user} user and click on a deal having the dealnumber #{dealnumber} and I see the deal details displayed on the right side of the screen.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  sleep 4
  @grshomepg.filtergridByDealnumber(dealnumber)
  @dealgrid = @grshomepg.fetchDealGridtable
  @dealgridColumns = @grshomepg.fetchDealGridColumns
  @dealGridColCount  = @dealgridColumns.length
  @GridData = @grshomepg.getGridValues(@dealgrid,@dealGridColCount)
  $dataAvailability = @grshomepg.verifydealavailableinGrid(@GridData)
  @grshomepg.clickdealrow(dealnumber,$dataAvailability)
  #sleep 10
end

And(/^as a (.*) user I see the mentioned fields non editable on the page$/) do |user|
  print "Step Name: Verify non editable fields on the deal edit page when logged in as a #{user} user .\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @nonEditableFields = "CONTRACT NUMBER,DEAL NUMBER,INCEPTION DATE,SUBMITTED"
  @editableFields = "DEAL NAME,TARGET DATE,STATUS,PRIORITY,UNDERWRITER,UNDERWRITER 2,TA,MODELER,ACTUARY"
  @grshomepg.verifynonEditableFields(@nonEditableFields,"0",$dataAvailability)
  @grshomepg.verifynonEditableFields(@editableFields,"1",$dataAvailability)
end

And(/^as a (.*) user I edit the fields with the mentioned values (.*) as Deal Name, (.*) as Target Date, (.*) as Priority, (.*) as Status, (.*) as primary Underwriter, (.*) as secondary Underwriter, (.*) as Technical Assistant, (.*) as Modelername, (.*) as Actuaryname$/) do |user, dealname, targetdate, priority, status, primaryUnderwriter, secondaryUnderwriter, ta, modeler, actuary|
  print "Step Name: Edit the fields as a #{user} user with the mentioned values #{dealname} as Deal Name, #{targetdate} as Target Date, #{priority} as Priority, #{status} as Status, #{primaryUnderwriter} as primary Underwriter, #{secondaryUnderwriter} as secondary Underwriter, #{ta} as Technical Assistant, #{modeler} as Modelername, #{actuary} as Actuaryname.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @fieldstobeUpdated = "#{dealname}*#{targetdate.to_s}*#{priority.to_s}*#{status.to_s}*#{primaryUnderwriter.to_s}*#{secondaryUnderwriter.to_s}*#{ta.to_s}*#{modeler.to_s}*#{actuary.to_s}"
  @grshomepg.updateeditablefields(user,@fieldstobeUpdated,$dataAvailability)
  sleep 4

end
And(/^as a (.*) user I edit the fields with the mentioned values (.*) as Deal Name, (.*) as Target Date using date picker, (.*) as Priority, (.*) as Status, (.*) as primary Underwriter, (.*) as secondary Underwriter, (.*) as Technical Assistant, (.*) as Modelername, (.*) as Actuaryname$/) do |user, dealname, targetdate, priority, status, primaryUnderwriter, secondaryUnderwriter, ta, modeler, actuary|
  print "Step Name: Edit the fields as a #{user} user with the mentioned values #{dealname} as Deal Name, #{targetdate} as Target Date, #{priority} as Priority, #{status} as Status, #{primaryUnderwriter} as primary Underwriter, #{secondaryUnderwriter} as secondary Underwriter, #{ta} as Technical Assistant, #{modeler} as Modelername, #{actuary} as Actuaryname.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @fieldstobeUpdated = "#{dealname}*#{targetdate}*#{priority.to_s}*#{status.to_s}*#{primaryUnderwriter.to_s}*#{secondaryUnderwriter.to_s}*#{ta.to_s}*#{modeler.to_s}*#{actuary.to_s}"
  @grshomepg.updateeditablefieldsusingdatepicker(user,@fieldstobeUpdated,$dataAvailability)
  sleep 4

end

And(/^as a (.*) user I am able to see the deal having the dealnumber (.*) updated successfully in DB with the new details (.*) as Deal Name, (.*) as Target Date, (.*) as Priority, (.*) as Status, (.*) as primary Underwriter, (.*) as secondary Underwriter, (.*) as Technical Assistant, (.*) as Modelername, (.*) as Actuaryname$/) do |user, dealnumber,dealname, targetdate, priority, status, primaryUnderwriter, secondaryUnderwriter, ta, modeler, actuary|
  print "Step Name: Verify whether the deal having the dealnumber #{dealnumber} updated successfully in DB with the new details #{dealname} as Deal Name, #{targetdate} as Target Date, #{priority} as Priority, #{status} as Status, #{primaryUnderwriter} as primary Underwriter, #{secondaryUnderwriter} as secondary Underwriter, #{ta} as Technical Assistant, #{modeler} as Modelername, #{actuary} as Actuaryname for  #{user} user .\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  #@fieldvaluestobeExpectedArr = [dealname.to_s,targetdate.to_s,priority.to_s,status.to_s,primaryUnderwriter.to_s,secondaryUnderwriter.to_s,ta.to_s,modeler.to_s,actuary.to_s]
  @fieldvaluestobeExpectedArr = "#{dealname}*#{targetdate.to_s}*#{priority.to_s}*#{status.to_s}*#{primaryUnderwriter.to_s}*#{secondaryUnderwriter.to_s}*#{ta.to_s}*#{modeler.to_s}*#{actuary.to_s}"
  @generatedExpectedResult = @grshomepg.generateexpectedvalues(dealnumber,@fieldvaluestobeExpectedArr)
  @dbquery = DBQueries.new
  @sqlquery=@dbquery.getDealNeededDetailsByDealNumber(dealnumber).to_s
  #print "\n"
  #print @sqlquery
  #print "\n"
  @dbresult = @grshomepg.sendQuery(@sqlquery)
  # print "\n"
  # print @dbresult
  # print "\n"
  # print @generatedExpectedResult
  # print "\n"
  BrowserContainer.CompareUserSpecificActualtoExpected(user,@dbresult,@generatedExpectedResult)
end

And(/^as a (.*) user I click the cancel button to go back to the deal details grid having the deal with deal number (.*) under the (.*) panel$/) do |user, dealnumber,status|
  print "Step Name: Connected as a #{user} user and Click the cancel button to go back to the deal details grid having the deal with deal number #{dealnumber}.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @editablegridcancelButton = @grshomepg.fetchEditableDealFormCancelButtonElement
  @grshomepg.ClickMenuBtn(@editablegridcancelButton)
  sleep 7
  # @Elementtobeverifiedtobenotpresent = @grshomepg.fetchEditableDealFormDealDetailsContainerElement
  # @grshomepg.verifyElementDoesNotExistonPage(@Elementtobeverifiedtobenotpresent,"Deal Details Screen")
  @Elementtobeverifiedtobepresent = @grshomepg.fetchAgGridRootElement
  @grshomepg.verifyElementExistonPage(@Elementtobeverifiedtobepresent,"Deal Summary Grid")
  @sqlQuery = @dbquery.getDealDetailsByDealNumber(dealnumber).to_s
  @dbresult = @grshomepg.sendQuery(@sqlQuery)
  @ModifiedDBResult = @grshomepg.modifyDBResult(@dbresult)
  #sleep 7
  @clickelement = @grshomepg.fetchpanelElement(status)
  @grshomepg.ClickBtn(@clickelement)
  @grshomepg.ClickBtn(@clickelement)
  sleep 4
  @grshomepg.filtergridByDealnumber(dealnumber)
  @dealgrid = @grshomepg.fetchDealGridtable
  @dealgridColumns = @grshomepg.fetchDealGridColumns
  @dealGridColCount  = @dealgridColumns.length
  @GridData = @grshomepg.getGridValues(@dealgrid,@dealGridColCount)
  @GridDataModified = @grshomepg.ModifyAllGridCellData(@GridData)
  # print "\n"
  # print @GridDataModified
  # print "\n"
  # print @ModifiedDBResult
  # print "\n"

  @grshomepg.CompareResults(@GridDataModified.to_s,@ModifiedDBResult.to_s)

end

And(/^as a (.*) user I click on a deal having the dealnumber (.*) and I see the deal details displayed on the right side of the screen$/) do |user, dealnumber|
  print "Step Name: Connected as a #{user} user Click on a deal having the dealnumber #{dealnumber} and I see the deal details displayed on the right side of the screen.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @grshomepg.clickdealrow(dealnumber,$dataAvailability)

end

Then(/^as a (.*) user I reset the deal values with the actual values (.*)$/) do |user,actualvalues|
  print "Step Name: Connected as a #{user} user Reset the deal values with the actual values.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @fieldstobeUpdatedArr = actualvalues
  @grshomepg.reseteditablefields(user,@fieldstobeUpdatedArr,$dataAvailability)

end


And(/^I will be able to see a Time Counter and Refresh button on the top\-right corner$/) do
  print "\n Step Name : To verify the Time Counter and Refresh button displayed on GRS Home screen\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @timeCounterEle = @grshomepg.fetchTimeCounterEle
  @grshomepg.verify
end

And(/^I will be able to see the Time Element filter above the grid$/) do
  print "\n Step Name : To verify the Time Element filters above the grid in GRS Home page"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  sleep 2
  @grshomepg.verifyTimeElementFilters
end

And(/^I click on the (.*) filter$/) do |timeElement|
  print"\n Step Name : To verify clicking on "+ timeElement.to_s+" time element filter\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @timeEleFilter = @grshomepg.fetchTimeEleFilter(timeElement)
  @grshomepg.ClickBtn(@timeEleFilter)
  sleep 3
end

Then(/^I will be able to see the grid data filter as per the (.*) filter$/) do |timeElement|
  print "\n Step Name : To verify the grid data filtered as per the "+ timeElement.to_s+" filter\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  sleep 3
  @grshomepg.verifying_GridData(timeElement)
end

And(/^I will be able to see a Time Counter on the page$/) do
  print "Step Name: Verify that the Time counter exists on the GRS Homepage.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @TimeCounterEle = @grshomepg.fetchTimeCounterelement
  @grshomepg.VerifyTimeCounter(@TimeCounterEle)
end

And(/^I can see that the Time Counter is incrementing in minutes$/) do
  print "Step Name: Verify that the Time counter increments successfully on the GRS Homepage.\n"
  $VERBOSE = nil
  @@timeMin = Time.now.min
  sleep 120
  @grshomepg.VerifyTimeCounterelementtext("2 minutes ago")
end


Then(/^the time lapsed in the Time Counter should match with the time lapsed in the System clock$/) do
  print "Step Name: Verify that the Time counter change on the GRS Homepage matches the change in System time.\n"
  @grshomepg.SysTimeVsCounter(@@timeMin)
end

Then(/^the counter should start from zero and continue incrementing$/) do
  print "Step Name: Verify whether the counter should start from zero and continue incrementing.\n"
  @grshomepg.VerifyTimeCounterelementtext("a few seconds ago")
end

And(/^I will be able to see a Refresh Icon and clicked on the icon$/) do
  print "Step Name: Verify that the Refresh Icon exists on the GRS Homepage.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @RefreshIconEle = @grshomepg.fetchRefreshButtonIconelement
  sleep 1
  @grshomepg.VerifyRefreshIcon(@RefreshIconEle)
  @grshomepg.ClickBtn(@RefreshIconEle)
end