And(/^I click on Documents icon and get all the folder structure$/) do
  print "Step Name: Verify Documents Icon is clicked and get the folder structure.\n"
  @docsIconObj = @grshomepg.fetchQuickEditDocumentsButtonElement

  # @notesAuthorObj = @grshomepg.fetchNotesAuthorClassElement
  # @notesAllEleobj = @grshomepg.fetchNotesWindowAllElement
  # @location = 'top'
  # @grshomepg.scrollTo(@notesIconObj,@location)
  # @grshomepg.scrollTo(@notesIconObj,@location)
  # @notesIconObj.scroll_into_view
  # @notesIconObj.flash
  @grshomepg.scrollPage(@docsIconObj)
  # @grshomepg.jump_to(@docsIconObj)
  # @notesIconObj.send_keys :control, :home
  # @grshomepg.ClickGridCol(@notesAllEleobj)
  @grshomepg.ClickGridCol(@docsIconObj)
  sleep 4
  @docsFolderStr = @grshomepg.fetchDocumentsFolderStruSchemaElement
  @docsFolderSchema = @grshomepg.getDocumentsFolderSchema(@docsFolderStr)
  @grshomepg.fetchDocumentsFolderStruExpandButton(@docsFolderSchema)
  # @docsFolderKeyDocs = @grshomepg.fetchDocumentKeyDocumentElements
  # @dbObj = DBQueries.new
  # @dbquery = @dbObj.getAllKeyDocs()
  sleep 1
end

Then(/^I validate key document of the deal (.*) with the database$/) do |textvalue|
  @docsFolderKeyDocs = @grshomepg.fetchDocumentKeyDocumentElements
  @dbObj = DBQueries.new
  @dbquery = @dbObj.getAllKeyDocs(textvalue)
  @dbresult = BrowserContainer.ExecuteQuery(@dbquery)
  @dbresulthash = @dbresult[0]
  @dbresultval = @dbresulthash["totalRecords"]
  @grshomepg.verifyKeyDocsCountWithDB(@docsFolderKeyDocs,@dbresultval)
end

And(/^I click on Documents icon and get all Document Types page count from the tree view$/) do
  print "Step Name: Verify Documents Icon is clicked and get the folder structure page count.\n"
  @docsIconObj = @grshomepg.fetchQuickEditDocumentsButtonElement
  @grshomepg.scrollPage(@docsIconObj)
  @grshomepg.ClickGridCol(@docsIconObj)
  sleep 4
  @docsFolderStr = @grshomepg.fetchDocumentsFolderStruSchemaElement
  @docsFolderPageCount = @grshomepg.getDocumentsFolderPageCount(@docsFolderStr)
  @docsFolderSchema = @grshomepg.getDocumentsFolderSchema(@docsFolderStr)
  @grshomepg.fetchDocumentsFolderStruExpandButton(@docsFolderSchema)
  # @docsFolderKeyDocs = @grshomepg.fetchDocumentKeyDocumentElements
  # @dbObj = DBQueries.new
  # @dbquery = @dbObj.getAllKeyDocs()
  sleep 4
end

Then(/^I validate key document type page count with the documents count for a given deal$/) do
  print "Step Name: Verify Documents type page count with the document count.\n"
  @docsFolderKeyDocs = @grshomepg.fetchDocumentKeyDocumentElements
  @docPageCount = @grshomepg.fetchDocumentPageCount(@docsFolderKeyDocs)
  # @dbObj = DBQueries.new
  # @dbquery = @dbObj.getAllKeyDocs(textvalue)
  # @dbresult = BrowserContainer.ExecuteQuery(@dbquery)
  # @dbresulthash = @dbresult[0]
  # @dbresultval = @dbresulthash["totalRecords"]
  # @grshomepg.verifyKeyDocsCountWithDB(@docsFolderKeyDocs,@dbresultval)
   @grshomepg.verifyCount(@docsFolderPageCount,@docPageCount)
end

And(/^I click on Documents icon and click on Tree View and get all Document Types page count from key document view$/) do
  print "Step Name: Verify Documents Icon and Tree view is clicked and get the folder structure page count.\n"
  @docsIconObj = @grshomepg.fetchQuickEditDocumentsButtonElement
  @grshomepg.scrollPage(@docsIconObj)
  @grshomepg.ClickGridCol(@docsIconObj)
  sleep 1
  @docsTreeViewLink = @grshomepg.fetchDocumentTreeViewLink
  puts @docsTreeViewLink.text
  @grshomepg.ClickGridCol(@docsTreeViewLink)
  @docsFolderStr = @grshomepg.fetchDocumentsFolderStruSchemaElement
  @docsFolderPageCount = @grshomepg.getDocumentsFolderPageCount(@docsFolderStr)
  @docsFolderSchema = @grshomepg.getDocumentsFolderSchema(@docsFolderStr)
  @grshomepg.fetchDocumentsFolderStruExpandButton(@docsFolderSchema)
  # @docsFolderKeyDocs = @grshomepg.fetchDocumentKeyDocumentElements
  # @dbObj = DBQueries.new
  # @dbquery = @dbObj.getAllKeyDocs()
  sleep 4
end

Then(/^I validate key document view document type page count with the documents count for a given deal$/) do
  print "Step Name: Verify key document view - Documents type page count with the document count.\n"
  @docsFolderKeyDocs = @grshomepg.fetchKeyDocumentViewAllDocsElement
  @docPageCount = @grshomepg.fetchDocumentPageCount(@docsFolderKeyDocs)
  # @dbObj = DBQueries.new
  # @dbquery = @dbObj.getAllKeyDocs(textvalue)
  # @dbresult = BrowserContainer.ExecuteQuery(@dbquery)
  # @dbresulthash = @dbresult[0]
  # @dbresultval = @dbresulthash["totalRecords"]
  # @grshomepg.verifyKeyDocsCountWithDB(@docsFolderKeyDocs,@dbresultval)
  @grshomepg.verifyCount(@docsFolderPageCount,@docPageCount)
end

And(/^I click on Documents icon and get all the key documents from Tree view$/) do
  print "Step Name: Verify Documents Icon and get all the key documents from Tree view .\n"
  @docsIconObj = @grshomepg.fetchQuickEditDocumentsButtonElement
  @grshomepg.scrollPage(@docsIconObj)
  @grshomepg.ClickGridCol(@docsIconObj)
  sleep 1
  @docsFolderStr = @grshomepg.fetchDocumentsFolderStruSchemaElement
  # @docsFolderPageCount = @grshomepg.getDocumentsFolderPageCount(@docsFolderStr)
  @docsFolderSchema = @grshomepg.getDocumentsFolderSchema(@docsFolderStr)
  @grshomepg.fetchDocumentsFolderStruExpandButton(@docsFolderSchema)
  @docsFolderKeyDocs = @grshomepg.fetchDocumentKeyDocumentElements
  @treeViewDocsName = @grshomepg.fetchKeyDocumentNames(@docsFolderKeyDocs)
  # puts @treeViewDocsName
end

And(/^I click on Tree View and get all key Document from key document view$/) do
  print "Step Name: Fetching all the key documents from key document view .\n"
  @docsTreeViewLink = @grshomepg.fetchDocumentTreeViewLink
  puts @docsTreeViewLink.text
  @grshomepg.ClickGridCol(@docsTreeViewLink)
  @grshomepg.scrollPage(@docsTreeViewLink)
  @docsFolderStr = @grshomepg.fetchDocumentsFolderStruSchemaElement
  # @docsFolderPageCount = @grshomepg.getDocumentsFolderPageCount(@docsFolderStr)
  @docsFolderSchema = @grshomepg.getDocumentsFolderSchema(@docsFolderStr)
  @grshomepg.fetchDocumentsFolderStruExpandButton(@docsFolderSchema)
  @keyDocsViewKeyDocsListEle = @grshomepg.fetchKeyDocViewKeyDocuments
  @keyDocsViewKeyDocsList = @grshomepg.fetchKeyDocumentNamesInKeyDocView(@keyDocsViewKeyDocsListEle)
  # puts @keyDocsViewKeyDocsList
  # @docsFolderKeyDocs = @grshomepg.fetchDocumentKeyDocumentElements
  # @dbObj = DBQueries.new
  # @dbquery = @dbObj.getAllKeyDocs()
  sleep 4
end

Then(/^I validate key documents present in both Tree view and Key document view are matched successfully$/) do
  print "Step Name: Comparing the Key Documents from both Tree view and key document view .\n"
  BrowserContainer.CompareArrayResults(@treeViewDocsName,@keyDocsViewKeyDocsList)
end