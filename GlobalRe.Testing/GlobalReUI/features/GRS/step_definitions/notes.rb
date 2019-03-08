
And(/^I click on the (.*) deal Number and validate quick Edit Screen is opened$/) do |textvalue|
  print "Step Name: Verify deal Number value " + textvalue + " is clicked and Quick edit screen is opened.\n"
  @dealNumberObj = @grshomepg.fetchSpecificDealNumElement(textvalue)
  @dealNumObj = @grshomepg.fetchSpecificDealNumColElement(textvalue)
  @grshomepg.ClickGridCol(@dealNumObj)
  # @location = 'top'
  # @grshomepg.scrollTo(@dealNumObj,@location)
  # sleep 3
  # @clickelement.focus
  @grshomepg.doubleClickOnElement(@dealNumberObj)
  sleep 1
  @quickEditObj = @grshomepg.fetchQuickEditSubmitButtonElement
  @grshomepg.verifyQuickEditScreen(@quickEditObj)
end

And(/^I will be able to see grid is filtered as per the (.*) column (.*) filter string with operator (.*)$/) do |columnname, textvalue, operator|
  print "Step Name: Verify grid data is as per the " + columnname + " column, filter value " + textvalue + " selected.\n"
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @dealgrid = @grshomepg.fetchDealGridtable
  @dealgridColumns = @grshomepg.fetchDealGridColumns
  @dealGridColCount  = @dealgridColumns.length
  @GridData = @grshomepg.getGridValues(@dealgrid,@dealGridColCount)
  # @grshomepg.verifyGridDataWithFilterResults(@GridData,@dealgridColumns,columnname,textvalue)
  @grshomepg.verifyGridDataIsAsperFilter(@GridData,columnname,textvalue,operator)
end

And(/^I click on Notes icon and open a note written by self$/) do
  print "Step Name: Verify Notes Icon is clicked and self written note is clicked.\n"
  @notesIconObj = @grshomepg.fetchQuickEditNotesButtonElement
  @notesWinObj = @grshomepg.fetchNotesWindowTextElement
  @notesAuthorObj = @grshomepg.fetchNotesAuthorClassElement
  @notesAllEleobj = @grshomepg.fetchNotesWindowAllElement
  # @location = 'top'
  # @grshomepg.scrollTo(@notesIconObj,@location)
  # @grshomepg.scrollTo(@notesIconObj,@location)
  # @notesIconObj.scroll_into_view
  # @notesIconObj.flash
  @grshomepg.scrollPage(@notesIconObj)
  # @notesIconObj.send_keys :control, :home
  # @grshomepg.ClickGridCol(@notesAllEleobj)
  @grshomepg.ClickGridCol(@notesIconObj)
  @grshomepg.verifyQuickEditScreen(@notesWinObj)
  @grshomepg.ClickGridCol(@notesAuthorObj)
  sleep 1
end


Then(/^I can edit the notes and save the note$/) do
  print "Step Name: Verify Note is editable and can able to save the note.\n"
  @notesInfoObj = @grshomepg.fetchNotesInfoArea
  @notesEditableObj = @grshomepg.fetchNotesReadOnlyArea
  @grshomepg.verifyElementIsReadOnly(@notesEditableObj, "Notes Info Text")
  @grshomepg.inputTextField(@notesInfoObj,"Automation Testing ")
  @notesSaveBtnObj = @grshomepg.fetchNotesSaveButton
  @grshomepg.ClickGridCol(@notesSaveBtnObj)

end

And(/^I click on Notes icon and open a note written by others (.*)$/) do |userName|
  print "Step Name: Verify Notes Icon is clicked and self written note is clicked.\n"
  @notesIconObj = @grshomepg.fetchQuickEditNotesButtonElement
  @notesWinObj = @grshomepg.fetchNotesWindowTextElement
  @notesAuthorObj = @grshomepg.fetchNotesWrittenByOthers(userName)
  @notesAllEleobj = @grshomepg.fetchNotesWindowAllElement
  @grshomepg.scrollPage(@notesIconObj)
  @grshomepg.ClickGridCol(@notesIconObj)
  @grshomepg.verifyQuickEditScreen(@notesWinObj)
  @grshomepg.ClickGridCol(@notesAuthorObj)
  sleep 10
end

Then(/^I can cannot edit the notes written by others and save the note$/) do
  print "Step Name: Verify Note written by others is not editable.\n"
  @notesInfoObj = @grshomepg.fetchNotesInfoArea
  @notesEditableObj = @grshomepg.fetchNotesReadOnlyArea
  if @grshomepg.verifyElementIsReadOnly(@notesEditableObj, "Notes Info Text") == true
    @notesCancelBtnObj = @grshomepg.fetchNotesCancelButton
    @grshomepg.ClickGridCol(@notesCancelBtnObj)
    sleep 1
  else
    fail "Element is not read only and hence note can be editable"
  end

end

Given(/^I validate the notes (.*) is updated in the database for the deal (.*)$/) do |notes, textvalue|
  @db = DBQueries.new
  @grshomepg = GRS_Home_Page.new(@browser, @reporter)
  @userName = @grshomepg.fetchLoginUsersName
  @sqlquery = @db.getNotesUpdates(textvalue,notes,@userName)
  puts "db : "+@sqlquery.to_s
  @dbresult = @grshomepg.sendQuery(@sqlquery)
  puts @dbresult.to_s
  @grshomepg.validateNotesDBResponse(@dbresult,notes)
  # @dbresulthash = @dbresult[0]
  # @dbresultval = @dbresulthash["displayName"]
end

Then(/^I can edit the notes (.*) and save the note$/) do |notes|
  print "Step Name: Verify Note is editable and can able to save the note.\n"
  @notesInfoObj = @grshomepg.fetchNotesInfoArea
  @notesEditableObj = @grshomepg.fetchNotesReadOnlyArea
  @grshomepg.verifyElementIsReadOnly(@notesEditableObj, "Notes Info Text")
  @grshomepg.inputTextField(@notesInfoObj,notes)
  @notesSaveBtnObj = @grshomepg.fetchNotesSaveButton
  @grshomepg.ClickGridCol(@notesSaveBtnObj)
end

Then(/^I can edit the notes (.*) and press cancel button and validate the cancel verification$/) do |notes|
  print "Step Name: Verify Note cancel verification.\n"
  @notesInfoObj = @grshomepg.fetchNotesInfoArea
  @notesEditableObj = @grshomepg.fetchNotesReadOnlyArea
  @grshomepg.verifyElementIsReadOnly(@notesEditableObj, "Notes Info Text")
  @grshomepg.inputTextField(@notesInfoObj,notes)
  @notesCancelBtnObj = @grshomepg.fetchNotesCancelButton
  @grshomepg.ClickGridCol(@notesCancelBtnObj)
  @notesCancelYesObj = @grshomepg.fetchNotesCancelPopupVerification
  @grshomepg.ClickGridCol(@notesCancelYesObj)
  sleep 5
end

And(/^I click on Notes icon and i click on Add Notes button and enter fields (.*) (.*)$/) do |noteType, notes|
  print "Step Name: Adding note and enter all mandatory fields.\n"

  @notesIconObj = @grshomepg.fetchQuickEditNotesButtonElement
  @notesWinObj = @grshomepg.fetchNotesWindowTextElement
  @grshomepg.scrollPage(@notesIconObj)
  @grshomepg.ClickGridCol(@notesIconObj)
  @grshomepg.verifyQuickEditScreen(@notesWinObj)

  @addNoteObj = @grshomepg.fetchAddNotesElement
  @grshomepg.ClickGridCol(@addNoteObj)
  @addNoteTypeObj = @grshomepg.fetchAddNotesTypeElement
  @grshomepg.ClickGridCol(@addNoteTypeObj)
  @selectNoteTypeObj = @grshomepg.fetchSelectNotesTypeElement
  @grshomepg.selectNoteType(@selectNoteTypeObj,noteType)

  @notesInfoObj = @grshomepg.fetchNotesInfoArea
  @notesEditableObj = @grshomepg.fetchNotesReadOnlyArea
  @grshomepg.verifyElementIsReadOnly(@notesEditableObj, "Notes Info Text")
  # @grshomepg.inputTextField(@notesInfoObj,"Automation Testing ")
  # @grshomepg.inputTextField(@notesInfoObj,notes)
end

Then(/^I can edit the notes (.*) and press cross button and validate the cancel verification$/) do |notes|
  print "Step Name: Verify Note cancel verification after clicking on cross button in the AddNote Window.\n"
  @notesInfoObj = @grshomepg.fetchNotesInfoArea
  @notesEditableObj = @grshomepg.fetchNotesReadOnlyArea
  @grshomepg.verifyElementIsReadOnly(@notesEditableObj, "Notes Info Text")
  @grshomepg.inputTextField(@notesInfoObj,notes)
  @notesCrossBtnObj = @grshomepg.fetchNoteCrossIconElement
  @grshomepg.ClickGridCol(@notesCrossBtnObj)
  @notesCancelYesObj = @grshomepg.fetchNotesCancelPopupVerification
  @grshomepg.ClickGridCol(@notesCancelYesObj)
  sleep 5
end

And(/^I validate cancel verification will not disappear after clicking on NO option$/) do
  print "Step Name: Verify Note cancel verification will not disappear after clicking on NO option.\n"
  @notesCancelNoObj = @grshomepg.fetchNotesCancelPopupVerificationNO
  @grshomepg.ClickGridCol(@notesCancelNoObj)
  sleep 5
end

And(/^I edit the notes (.*) and press cancel button and validate cancel verification will not disappear after clicking on NO option in warning popup$/) do |notes|
  print "Step Name: Verify Note cancel verification will not disappear after clicking on NO option.\n"
  @notesInfoObj = @grshomepg.fetchNotesInfoArea
  @notesEditableObj = @grshomepg.fetchNotesReadOnlyArea
  @grshomepg.verifyElementIsReadOnly(@notesEditableObj, "Notes Info Text")
  @grshomepg.inputTextField(@notesInfoObj,notes)
  @notesCrossBtnObj = @grshomepg.fetchNoteCrossIconElement
  @grshomepg.ClickGridCol(@notesCrossBtnObj)
  @notesCancelNoObj = @grshomepg.fetchNotesCancelPopupVerificationNO
  @grshomepg.ClickGridCol(@notesCancelNoObj)
  sleep 5
end


Then(/^I click on save button to save the note$/) do
  @notesSaveBtnObj = @grshomepg.fetchNotesSaveButton
  @grshomepg.ClickGridCol(@notesSaveBtnObj)
end

And(/^I click on Add Note button and select note type as (.*)$/) do |noteType|
  @notesIconObj = @grshomepg.fetchQuickEditNotesButtonElement
  @notesWinObj = @grshomepg.fetchNotesWindowTextElement
  @grshomepg.scrollPage(@notesIconObj)
  @grshomepg.ClickGridCol(@notesIconObj)
  @grshomepg.verifyQuickEditScreen(@notesWinObj)

  @addNoteObj = @grshomepg.fetchAddNotesElement
  @grshomepg.ClickGridCol(@addNoteObj)
  @addNoteTypeObj = @grshomepg.fetchAddNotesTypeElement
  @grshomepg.ClickGridCol(@addNoteTypeObj)
  @selectNoteTypeObj = @grshomepg.fetchSelectNotesTypeElement
  @grshomepg.selectNoteType(@selectNoteTypeObj,noteType)

  @notesInfoObj = @grshomepg.fetchNotesInfoArea
  @notesEditableObj = @grshomepg.fetchNotesReadOnlyArea
  @grshomepg.verifyElementIsReadOnly(@notesEditableObj, "Notes Info Text")
  # @grshomepg.inputTextField(@notesInfoObj,"Automation Testing ")
  # @grshomepg.inputTextField(@notesInfoObj,notes
end

And(/^I add text in notes as (.*) in input field$/) do |notes|
  @notesInfoObj = @grshomepg.fetchNotesInfoArea
  @notesEditableObj = @grshomepg.fetchNotesReadOnlyArea
  @grshomepg.verifyElementIsReadOnly(@notesEditableObj, "Notes Info Text")
  @grshomepg.inputTextField(@notesInfoObj,notes)
end

Then(/^I validate that save button is not enabled$/) do
  @notesSaveBtnObj = @grshomepg.fetchNotesSaveButton
  @attribute=@notesSaveBtnObj.attribute_value('disabled')
  puts "Attribute value="+@attribute.to_s
# @grshomepg.checkIfButtonIsDisabled(@attribute)
  if @attribute.nil?
    print "PASS- Button is disbaled"
  else
    fail "FAIL-Button is enabled"
  end
end