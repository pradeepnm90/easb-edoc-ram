require 'selenium-webdriver'
require 'watir'
require 'watir/elements/table'
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Pages","grs_home_page.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","config.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","db_queries.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","fetch_element_details.rb")
require File.join(File.absolute_path('../', File.dirname(__FILE__)),"Base","browser_container.rb")
require 'report_builder'
require 'win32ole'
require 'rautomation'
require 'httparty'
require 'nokogiri'
require 'pry'
require 'csv'
require 'date'
require 'fileutils'
# require File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","winClicker.rb")
# require File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","enabled_popup.rb")


class GRS_Home_Page < BrowserContainer
  def initialize(browser,reporter)
    #puts "In Login Page"
    #BrowserContainer.new()
    super(browser,reporter)
  end
  @fetchelement = FetchElementDetails.new
  @PanelButtonClass = @fetchelement.getPanelButtonClass
  @onholdbtnid = @fetchelement.getOnHoldPanelButton
  @boundbtnid =@fetchelement.getBoundPanelButton
  @inprogressbtnid = @fetchelement.getInProgressPanelButton
  @renewable6monthsbtnid =@fetchelement.getRenewable6MonthsPanelButton
  @QlikViewBtnid = @fetchelement.getQlikViewButton
  @quickLinksElement = @fetchelement.getQuickLinks
  @quickLinksElementclass = @fetchelement.getQuickLinksClassElement
  @ERMSBtnid = @fetchelement.getERMSButton
  @dealnamegridcolumn = @fetchelement.getGridDealName
  @contractnumbergridcolumn = @fetchelement.getGridContractNumber
  @inceptiongridcolumn = @fetchelement.getGridInception
  @targetdategridcolumn = @fetchelement.getGridTargetDate
  @prioritygridcolumn = @fetchelement.getGridPriority
  @submittedgridcolumn = @fetchelement.getGridSubmitted
  @statusgridcolumn = @fetchelement.getGridStatus
  @dealnumbergridcolumn = @fetchelement.getGridDealNumber
  @underwriternamegridcolumn = @fetchelement.getGridUnderwriterName
  @underwriter2namegridcolumn = @fetchelement.getGridUnderwriter2Name
  @tagridcolumn = @fetchelement.getGridTAName
  @modelergridcolumn = @fetchelement.getGridModelerName
  @actuarygridcolumn = @fetchelement.getGridActuaryName
  @expirationgridcolumn = @fetchelement.getGridExpiration
  @brokernamegridcolumn = @fetchelement.getGridBrokerName
  @brokercontactgridcolumn = @fetchelement.getGridBrokerContact
  @Inprogress_underreviewcheckbox = @fetchelement.getInProgress_UnderReviewCheckBox
  @Inprogress_authorizecheckbox = @fetchelement.getInProgress_AuthorizeCheckBox
  @Inporgress_outstandingquotecheckbox = @fetchelement.getInProgress_OutStandingQuoteCheckBox
  @Inprogress_tobedeclinedcheckbox = @fetchelement.getInProgress_ToBeDeclinedCheckBox
  @Inprogress_boundpendingdataentrycheckbox = @fetchelement.getInProgress_BoundPendingDataEntryCheckBox
  @Inprogress_underreviewcheckboxInput = @fetchelement.getInProgress_UnderReviewCheckBoxInput
  @Inprogress_authorizecheckboxInput = @fetchelement.getInProgress_AuthorizeCheckBoxInput
  @Inporgress_outstandingquotecheckboxInput = @fetchelement.getInProgress_OutStandingQuoteCheckBoxInput
  @Inprogress_tobedeclinedcheckboxInput = @fetchelement.getInProgress_ToBeDeclinedCheckBoxInput
  @Inprogress_boundpendingdataentrycheckboxInput = @fetchelement.getInProgress_BoundPendingDataEntryCheckBoxInput
  @GridFieldNameClass = @fetchelement.getGridFieldNameClass
  @GridCellClass = @fetchelement.getAgGridCellclass
  @GridRowLevel0Class = @fetchelement.getAgRowLevel0Class
  #@underwriternamegridcolumn = @fetchelement.getGridUnderwriterName
  @dealgrid = @fetchelement.getStatusGrid
  @dealgridlist = @fetchelement.getDealGridList
  # ENV['USERNAME'] = "Venkatesh Boya"
  @useridtext = ENV['USERNAME']
  # puts "UserId : "+@useridtext
  @dealGD = @fetchelement.getDealGridContainer
  @ToolMenuBodyid = @fetchelement.getAgToolMenuBody
  @ToolMenuHeaderid = @fetchelement.getAgToolMenuHeader
  @ToolMenuHeaderTab = @fetchelement.getAgToolMenuHeaderTab
  @ToolMenuColumnSelectPanel = @fetchelement.getAgToolMenuColumnSelectPanel
  @ToolMenuColSelPanelFieldClass = @fetchelement.getAgToolMenuColSelPanelFieldClass
  @ToolMenuToolPanelOptionID = @fetchelement.getAgToolMenuToolPanelOptionID
  @ToolMenuToolPanelOptionText = @fetchelement.getAgToolMenuToolPanelOptionText
  @GridRightFloatingBottomClass = @fetchelement.getAgGridRightFloatingBottomClass
  @TeamButtonid = @fetchelement.getTeamButtonid
  @SubDivisionOverlayPaneid = @fetchelement.getSubDivisionOverlayPaneid
  @SubDivisionLabelClass = @fetchelement.getSubDivisionLabelClass
  @SubDivisionCasualtyText = @fetchelement.getSubDivisionCasualtyText
  @SubDivisionCasualtyTreatyText = @fetchelement.getSubDivisionCasualtyTreatyText
  @SubDivisionCasFacText = @fetchelement.getSubDivisionCasFacText
  @SubDivisionPropertyText = @fetchelement.getSubDivisionPropertyText
  @SubDivisionIntlPropertyText = @fetchelement.getSubDivisionIntlPropertyText
  @SubDivisionNAPropertyText = @fetchelement.getSubDivisionNAPropertyText
  @SubDivisionSpecialtyText = @fetchelement.getSubDivisionSpecialtyText
  @SubDivisionSpecialtyNonPEText = @fetchelement.getSubDivisionSpecialtyNonPEText
  @SubDivisionPublicEntityText = @fetchelement.getSubDivisionPublicEntityText
  @DummywithSubstatusButtonid =@fetchelement.getDummywithSubstatusButton
  @DummyInquiredsubstatusid =@fetchelement.getDummyInquiredsubstatus
  @DummyInDueDiligencesubstatusid =@fetchelement.getDummyInDueDiligencesubstatus
  @DummyInformationalAgreementsubstatusid =@fetchelement.getDummyInformationalAgreementsubstatus
  @DummyInquiredsubstatusidinput =@fetchelement.getDummyInquiredsubstatus
  @DummyInDueDiligencesubstatusidinput =@fetchelement.getDummyInDueDiligencesubstatus
  @DummyInformationalAgreementsubstatusidinput =@fetchelement.getDummyInformationalAgreementsubstatus
  @DummywithnoSubstatusButtonid =@fetchelement.getDummywithnoSubstatusButton
  @GridAgIconFilterClass = @fetchelement.getAgIconFilterClass
  @GridAgColumnFilterID = @fetchelement.getAgColumnFilterID
  @GridAgColumnFilterEqualOption = @fetchelement.getAgColumnFilterEqualOption
  @GridAgColumnFilterGTOption = @fetchelement.getAgColumnFilterGTOption
  @GridAgColumnFilterLTOption = @fetchelement.getAgColumnFilterLTOption
  @GridAgColumnFilterNEOption = @fetchelement.getAgColumnFilterNEOption
  @GridAgColumnFilterIROption = @fetchelement.getAgColumnFilterIROption
  @GridAgColumnFilterLTOEqualOption = @fetchelement.getAgColumnFilterLTOEqualOption
  @GridAgColumnFilterGTOEqualOption = @fetchelement.getAgColumnFilterGTOEqualOption
  @GridAgColumnFilterContainsOption = @fetchelement.getAgColumnFilterContainsOption
  @GridAgColumnFilternotContainsOption = @fetchelement.getAgColumnFilternotContainsOption
  @GridAgColumnFilterSelectAllId = @fetchelement.getAgColumnFilterSelectAllId
  @GridAgColumnFilterSelectClass = @fetchelement.getAgColumnFilterSelectClass
  @GridAgColumnFilterSelectAuthorizetext = @fetchelement.getAgColumnFilterSelectAuthorizetext
  @GridAgColumnFilterSelectURtext = @fetchelement.getAgColumnFilterSelectURtext
  @GridAgColumnFilterSelectBPDEtext = @fetchelement.getAgColumnFilterSelectBPDEtext
  @GridAgColumnFilterSelectOQtext = @fetchelement.getAgColumnFilterSelectOQtext
  @GridAgColumnFilterSelectTBDtext = @fetchelement.getAgColumnFilterSelectTBDtext
  @GridAgColumnFilterSelectOHtext = @fetchelement.getAgColumnFilterSelectOHtext
  @GridAgColumnFilterSelectBoundtext = @fetchelement.getAgColumnFilterSelectBoundtext
  @GridAgColumnFilterStatusInputID = @fetchelement.getAgColumnFilterStatusInputID
  @GridAgColumnFilterInputClass = @fetchelement.getAgColumnFilterInputClass
  @GridAgColumnFilterApplyButtonID = @fetchelement.getAgColumnFilterApplyButtonID
  @GridAgColumnFilterDateInputID = @fetchelement.getAgColumnFilterDateInputID
  @GridAgColumnFilterTextInputID = @fetchelement.getAgColumnFilterTextInputID
  @GridAgColumnFilterApplyButtonText = @fetchelement.getAgColumnFilterApplyButtonText
  @GridAgColumnFilterDateInputClass = @fetchelement.getAgColumnFilterDateInputClass
  @GridAgColumnFilterTextInputClass = @fetchelement.getAgColumnFilterTextInputClass
  @EditableDealFormDealNameId = @fetchelement.getEditableDealFormDealNameId
  @EditableDealFormContactNumberID = @fetchelement.getEditableDealFormContactNumberID
  @EditableDealFormDealStatusID = @fetchelement.getEditableDealFormDealStatusID
  @EditableDealFormDealNumberID = @fetchelement.getEditableDealFormDealNumberID
  @EditableDealFormInceptionDateID = @fetchelement.getEditableDealFormInceptionDateID
  @EditableDealFormInceptionDateToggleID = @fetchelement.getEditableDealFormInceptionDateToggleID
  @EditableDealFormInceptionDatepickerID = @fetchelement.getEditableDealFormInceptionDatepickerID
  @EditableDealFormTargetDateID = @fetchelement.getEditableDealFormTargetDateID
  @EditableDealFormTargetDateToggleID = @fetchelement.getEditableDealFormTargetDateToggleID
  @EditableDealFormTargetDatePickerID = @fetchelement.getEditableDealFormTargetDatePickerID
  @EditableDealFormTargetDatePickerClass = @fetchelement.getEditableDealFormTargetDatePickerClass
  @EditableDealFormSubmittedID = @fetchelement.getEditableDealFormSubmittedID
  @EditableDealFormSubmittedToggleID = @fetchelement.getEditableDealFormSubmittedToggleID
  @EditableDealFormSubmittedDatePickerID = @fetchelement.getEditableDealFormSubmittedDatePickerID
  @EditableDealFormPriorityID = @fetchelement.getEditableDealFormPriorityID
  @EditableDealFormUnderwritterID = @fetchelement.getEditableDealFormUnderwritterID
  @EditableDealFormUnderwritter2ID = @fetchelement.getEditableDealFormUnderwritter2ID
  @EditableDealFormTechAsstID = @fetchelement.getEditableDealFormTechAsstID
  @EditableDealFormActuaryID = @fetchelement.getEditableDealFormActuaryID
  @EditableDealFormModelerID = @fetchelement.getEditableDealFormModelerID
  @AgGridMenuClass = @fetchelement.getAgMenuClass
  @AgGridMenuExportOtionID = @fetchelement.getAgMenuExportOptionID
  @AgGridMenuExportOptionText = @fetchelement.getAgMenuExportOptionText
  @AgGridMenuExcelExportOptionText = @fetchelement.getAgMenuExcelExportOptionText
  @EditableDealFormCancelButtonID = @fetchelement.getEditableDealFormCancelButtonID
  @EditableDealFormSubmitButtonID = @fetchelement.getEditableDealFormSubmitButtonID
  @AgStatusGridClass = @fetchelement.getAgStatusGridClass
  @EditableDealFormmatoptionclass = @fetchelement.getEditableDealFormmatoptionclass
  @EditableDealFormDealDetailsContainerClass = @fetchelement.getEditableDealFormDealDetailsContainerClass
  @AgGridRootClass = @fetchelement.getAgGridRootClass
  @EditableDealFormTargetDateMATCalendarPeriodClass = @fetchelement.getEditableDealFormTargetDateMATCalendarPeriodClass
  @@EditableDealFormTargetDateMATCalendarPeriodCellContentClass = @fetchelement.getEditableDealFormTargetDateMATCalendarPeriodCellContentClass
  @EditableDealFormTargetDateMATCalendarNextButtonClass = @fetchelement.getEditableDealFormTargetDateMATCalendarNextButtonClass
  @EditableDealFormTargetDateMATCalendarPreviousButtonClass = @fetchelement.getEditableDealFormTargetDateMATCalendarPreviousButtonClass
  @RefreshButtonClass = @fetchelement.getRefreshButtonClass
  @dealGridTableColumns = @fetchelement.getDealGridHeaderColumns
  @gridTableLoadingEle = @fetchelement.getAgGridTableLoadingEle
  #-----Notes Elements-------
  @quickEditSubButtonID = @fetchelement.getQuickEditSubmitButton
  @quickEditNotesButtonClass = @fetchelement.getQuickEditNotesButton
  @noteWindowText = @fetchelement.getNotesWindow
  @noteAuthorClass = @fetchelement.getNotesAuthor
  @noteInfo = @fetchelement.getNotesInfoTextArea
  @noteReadonly = @fetchelement.getNoteReadOnlyArea
  @noteEditWindowClass = @fetchelement.getEditNoteWindowClass
  @notesCancelBtn = @fetchelement.getNotesCancelButtonClass
  @notesCancelBtnType = @fetchelement.getNotesCancelButtonType
  @notesCancelPopup = @fetchelement.getNotesCancelPopupXpath('Yes')
  @notesCancelPopupNO = @fetchelement.getNotesCancelPopupXpath('No')
  @addNotesEle = @fetchelement.getAddNotesElement
  @addNoteTypeEle = @fetchelement.getAddNotesTypeElement
  @selectNoteTypeEle = @fetchelement.getSelectNotesTypeElement
  @noteCrossIcon = @fetchelement.getNoteCrossIcon
  #-------Documents Elements------
  @quickEditDocsButtonClass = @fetchelement.getDocumentsIconButton
  @documentFolderStructure = @fetchelement.getDocumentsFolders
  @documentFolderKeyDocsXPath = @fetchelement.getDocumentsKeyDocsXpath
  @keyViewdocumentFolderAllDocsXPath = @fetchelement.getkeyViewDocumentsAllDocsXpath
  @documentTreeViewLink = @fetchelement.getTreeViewLink
  @keyDocsViewDocsList = @fetchelement.getTreeDocumentViewDocsList
  @keyDocsViewKeyDocsList = @fetchelement.getKeyDocViewKeyDocumentList

  #---------------------------------------------------------------------------------------------------------------------------------------------#
  #---------------------------------------------------------------------------------------------------------------------------------------------#

  #print '\n'
  #print @useridtext
  #print '\n'
  #@dealnamegridcolumn
  @@OnHoldbuttonlocator = {:id => @onholdbtnid,:class => /#{@PanelButtonClass}/}
  @@Boundbuttonlocator = {:id => @boundbtnid,:class => /#{@PanelButtonClass}/}
  @@Inprogressbuttonlocator = {:id => @inprogressbtnid,:class => /#{@PanelButtonClass}/}
  @@Renewable6monthsbuttonlocator = {:id => @renewable6monthsbtnid,:class => /#{@PanelButtonClass}/}
  @@Inprogress_URCheckboxlocator = {:id => @Inprogress_underreviewcheckbox}
  @@Inprogress_AuthorizeCheckboxlocator = {:id => @Inprogress_authorizecheckbox}
  @@InprogressOQCheckboxlocator = {:id => @Inporgress_outstandingquotecheckbox}
  @@InprogressTBDCheckboxlocator = {:id => @Inprogress_tobedeclinedcheckbox}
  @@Inprogress_BPDECheckboxlocator = {:id => @Inprogress_boundpendingdataentrycheckbox}
  @@Inprogress_URCheckboxInputlocator = {:id => @Inprogress_underreviewcheckboxInput}
  @@Inprogress_AuthorizeCheckboxInputlocator = {:id => @Inprogress_authorizecheckboxInput}
  @@InprogressOQCheckboxInputlocator = {:id => @Inporgress_outstandingquotecheckboxInput}
  @@InprogressTBDCheckboxInputlocator = {:id => @Inprogress_tobedeclinedcheckboxInput}
  @@Inprogress_BPDECheckboxInputlocator = {:id => @Inprogress_boundpendingdataentrycheckboxInput}
  @@Inprogress_URCheckboxInputlocator1 = {:id => @Inprogress_underreviewcheckboxInput1}
  @@TeamButtonlocator = {:id => @TeamButtonid}
  @@SubDivisionOverlayPanelocator = {:id => @SubDivisionOverlayPaneid}
  @@SubDivisionCasualtylocator = {visible_text: @SubDivisionCasualtyText, class: @SubDivisionLabelClass}
  @@SubDivisionCasualtyTreatylocator = {visible_text: @SubDivisionCasualtyTreatyText, class: @SubDivisionLabelClass}
  @@SubDivisionCasFaclocator = {visible_text: @SubDivisionCasFacText, class: @SubDivisionLabelClass}
  @@SubDivisionPropertylocator = {visible_text: @SubDivisionPropertyText, class: @SubDivisionLabelClass}
  @@SubDivisionIntlPropertylocator = {visible_text: @SubDivisionIntlPropertyText, class: @SubDivisionLabelClass}
  @@SubDivisionNAPropertylocator = {visible_text: @SubDivisionNAPropertyText, class: @SubDivisionLabelClass}
  @@SubDivisionSpecialtylocator = {visible_text: @SubDivisionSpecialtyText, class: @SubDivisionLabelClass}
  @@SubDivisionSpecialtyNonPElocator = {visible_text: @SubDivisionSpecialtyNonPEText, class: @SubDivisionLabelClass}
  @@SubDivisionPublicEntitylocator = {visible_text: @SubDivisionPublicEntityText, class: @SubDivisionLabelClass}
  @@QlikViewButtonLocator = {:class => @QlikViewBtnid}
  @@quickLinksLocator = {:class => @quickLinksElementclass}
  @@ERMSButtonLocator = {visible_text: /#{@ERMSBtnid}/}
  @@DealNameCollocator = {visible_text: /#{@dealnamegridcolumn}/, class: /#{@GridFieldNameClass}/}
  @@ContractNumberCollocator = {visible_text: /#{@contractnumbergridcolumn}/, class: /#{@GridFieldNameClass}/}
  @@InceptionCollocator = {visible_text: /#{@inceptiongridcolumn}/, class: /#{@GridFieldNameClass}/}
  @@TargetDateCollocator = {visible_text: /#{@targetdategridcolumn}/, class: /#{@GridFieldNameClass}/}
  @@PriorityCollocator = {visible_text: /#{@prioritygridcolumn}/, class: /#{@GridFieldNameClass}/}
  @@SubmittedCollocator = {visible_text: /#{@submittedgridcolumn}/, class: /#{@GridFieldNameClass}/}
  @@StatusCollocator = {visible_text: /#{@statusgridcolumn}/, class: /#{@GridFieldNameClass}/}
  @@DealNumberCollocator = {visible_text: /#{@dealnumbergridcolumn}/, class: /#{@GridFieldNameClass}/}
  @@UWNameCollocator = {visible_text: /#{@underwriternamegridcolumn}/, class: /#{@GridFieldNameClass}/}
  @@UW2NameCollocator = {visible_text: /#{@underwriter2namegridcolumn}/, class: /#{@GridFieldNameClass}/}
  @@TACollocator = {visible_text: /#{@tagridcolumn}/, class: /#{@GridFieldNameClass}/}
  @@ModelerCollocator = {visible_text: /#{@modelergridcolumn}/, class: /#{@GridFieldNameClass}/}
  @@ActuaryCollocator = {visible_text: /#{@actuarygridcolumn}/, class: /#{@GridFieldNameClass}/}
  @@ExpirationCollocator = {visible_text: /#{@expirationgridcolumn}/, class: /#{@GridFieldNameClass}/}
  @@BrokerNameCollocator = {visible_text: /#{@brokernamegridcolumn}/, class: /#{@GridFieldNameClass}/}
  @@BrokerContactCollocator = {visible_text: /#{@brokercontactgridcolumn}/, class: /#{@GridFieldNameClass}/}
  # @@ToolMenuBodylocator = {:id => @ToolMenuBodyid}
  @@ToolMenuBodylocator = {:class => @ToolMenuBodyid}
  # @@ToolMenuHeaderlocator = {:id => @ToolMenuHeaderid}
  @@ToolMenuHeaderlocator = {:class => @ToolMenuHeaderid}
  @@ToolMenuHeaderTablocator = {:class => /#{@ToolMenuHeaderTab}/}
  @@ToolMenuColumnSelectPanellocator = {:class => /#{@ToolMenuColumnSelectPanel}/}
  @@DealNameToolMenFieldlocator = {visible_text: @dealnamegridcolumn, class: /#{@ToolMenuColSelPanelFieldClass}/}
  @@ContractNumberToolMenFieldlocator = {visible_text: /#{@contractnumbergridcolumn}/, class: /#{@ToolMenuColSelPanelFieldClass}/}
  @@InceptionToolMenFieldlocator = {visible_text: @inceptiongridcolumn, class: /#{@ToolMenuColSelPanelFieldClass}/}
  @@TargetDateToolMenFieldlocator = {visible_text: @targetdategridcolumn, class: /#{@ToolMenuColSelPanelFieldClass}/}
  @@PriorityToolMenFieldlocator = {visible_text: @prioritygridcolumn, class: /#{@ToolMenuColSelPanelFieldClass}/}
  @@SubmittedToolMenFieldlocator = {visible_text: @submittedgridcolumn, class: /#{@ToolMenuColSelPanelFieldClass}/}
  @@StatusToolMenFieldlocator = {visible_text: @statusgridcolumn, class: /#{@ToolMenuColSelPanelFieldClass}/}
  @@DealNumberToolMenFieldlocator = {visible_text: @dealnumbergridcolumn, class: /#{@ToolMenuColSelPanelFieldClass}/}
  @@UWNameToolMenFieldlocator = {visible_text: @underwriternamegridcolumn, class: /#{@ToolMenuColSelPanelFieldClass}/}
  @@UW2NameToolMenFieldlocator = {visible_text: @underwriter2namegridcolumn, class: /#{@ToolMenuColSelPanelFieldClass}/}
  @@TAToolMenFieldlocator = {visible_text: @tagridcolumn, class: /#{@ToolMenuColSelPanelFieldClass}/}
  @@ModelerToolMenFieldlocator = {visible_text: @modelergridcolumn, class: /#{@ToolMenuColSelPanelFieldClass}/}
  @@ActuaryToolMenFieldlocator = {visible_text: @actuarygridcolumn, class: /#{@ToolMenuColSelPanelFieldClass}/}
  @@ExpirationToolMenFieldlocator = {visible_text: @expirationgridcolumn, class: /#{@ToolMenuColSelPanelFieldClass}/}
  @@BrokerNameToolMenFieldlocator = {visible_text: @brokernamegridcolumn, class: /#{@ToolMenuColSelPanelFieldClass}/}
  @@BrokerContactToolMenFieldlocator = {visible_text: @brokercontactgridcolumn, class: /#{@ToolMenuColSelPanelFieldClass}/}
  # @@ToolMenuToolPanelOptionFieldlocator = {id: @ToolMenuToolPanelOptionID, visible_text: @ToolMenuToolPanelOptionText}
  @@gridTableLoadingEleLocator = {:xpath => @gridTableLoadingEle}
  @@ToolMenuToolPanelOptionFieldlocator = {:class => /#{@ToolMenuToolPanelOptionText}/}
  @@GridRightFloatingBottomlocator = {class: @GridRightFloatingBottomClass}
  @@GridCelllocator = {class: /#{@GridCelllocator}/}
  @@RowLevel0locator = {class: /#{@RowLevel0locator}/}
  @@GridAgIconFilterClasslocator = {:class => /#{@GridAgIconFilterClass}/}
  @@GridAgColumnFilterlocator = {:id => @GridAgColumnFilterID}
  @@GridAgColumnFilterEqualOptionlocator = {:value => @GridAgColumnFilterEqualOption}
  @@GridAgColumnFilterGTOptionlocator = {:value => @GridAgColumnFilterGTOption}
  @@GridAgColumnFilterLTOptionlocator = {:value => @GridAgColumnFilterLTOption}
  @@GridAgColumnFilterNEOptionlocator = {:value => @GridAgColumnFilterNEOption}
  @@GridAgColumnFilterIROptionlocator = {:value => @GridAgColumnFilterIROption}
  @@GridAgColumnFilterLTOEqualOptionlocator = {:value => @GridAgColumnFilterLTOEqualOption}
  @@GridAgColumnFilterGTOEqualOptionlocator = {:value => @GridAgColumnFilterGTOEqualOption}
  @@GridAgColumnFilterContainsOptionlocator = {:value => @GridAgColumnFilterContainsOption}
  @@GridAgColumnFilternotContainsOptionlocator = {:value => @GridAgColumnFilternotContainsOption}
  @@GridAgColumnFilterSelectAllIdlocator = {:id => @GridAgColumnFilterSelectAllId}
  @@GridAgColumnFilterSelectAuthorizeoptionlocator = {:visible_text => /#{@GridAgColumnFilterSelectAuthorizetext}/, :class => @GridAgColumnFilterSelectClass}
  @@GridAgColumnFilterSelectURoptionlocator = {:visible_text => /#{@GridAgColumnFilterSelectAuthorizetext}/, :class => @GridAgColumnFilterSelectClass}
  @@GridAgColumnFilterSelectBPDEoptionlocator = {:visible_text => /#{@GridAgColumnFilterSelectBPDEtext}/, :class => @GridAgColumnFilterSelectClass}
  @@GridAgColumnFilterSelectOQoptionlocator = {:visible_text => /#{@GridAgColumnFilterSelectOQtext}/, :class => @GridAgColumnFilterSelectClass}
  @@GridAgColumnFilterSelectTBDoptionlocator = {:visible_text => /#{@GridAgColumnFilterSelectTBDtext}/, :class => @GridAgColumnFilterSelectClass}
  @@GridAgColumnFilterSelectOHoptionlocator = {:visible_text => /#{@GridAgColumnFilterSelectOHtext}/, :class => @GridAgColumnFilterSelectClass}
  @@GridAgColumnFilterSelectBoundoptionlocator = {:visible_text => /#{@GridAgColumnFilterSelectBoundtext}/, :class => @GridAgColumnFilterSelectClass}
  @@GridAgColumnFilterStatusInputlocator = {:id => @GridAgColumnFilterStatusInputID}
  @@GridAgColumnFilterInputlocator = {:class => @GridAgColumnFilterInputClass}
  @@GridAgColumnFilterApplyButtonlocator = {id: @GridAgColumnFilterApplyButtonID, visible_text: @GridAgColumnFilterApplyButtonText}
  @@GridAgColumnFilterDateInputlocator = {:id => @GridAgColumnFilterDateInputID,:class => @GridAgColumnFilterDateInputClass}
  @@GridAgColumnFilterTextInputlocator = {:id => @GridAgColumnFilterTextInputID,:class => @GridAgColumnFilterTextInputClass}
  @@GridAgStatusGridClasslocator = {:class =>@AgStatusGridClass}
  @@DummywithSubstatusButton = {:id => @DummywithSubstatusButtonid}
  @@DummyInquiredsubstatus = {:id => @DummyInquiredsubstatusid}
  @@DummyInDueDiligencesubstatus = {:id => @DummyInDueDiligencesubstatusid}
  @@DummyInformationalAgreementsubstatus = {:id => @DummyInformationalAgreementsubstatusid}
  @@DummyInquiredsubstatusinput = {:id => @DummyInquiredsubstatusidinput}
  @@DummyInDueDiligencesubstatusinput = {:id => @DummyInDueDiligencesubstatusidinput}
  @@DummyInformationalAgreementsubstatusinput = {:id => @DummyInformationalAgreementsubstatusidinput}
  @@DummywithnoSubstatusButton = {:id => @DummywithnoSubstatusButtonid}
  @@EditableDealFormDealNamelocator = {:id => @EditableDealFormDealNameId}
  @@EditableDealFormContactNumberlocator = {:id => @EditableDealFormContactNumberID}
  @@EditableDealFormDealStatuslocator = {:id => @EditableDealFormDealStatusID}
  @@EditableDealFormDealNumberlocator = {:id => @EditableDealFormDealNumberID}
  @@EditableDealFormInceptionDatelocator = {:id => @EditableDealFormInceptionDateID}
  @@EditableDealFormInceptionDateTogglelocator = {:id => @EditableDealFormInceptionDateToggleID}
  @@EditableDealFormInceptionDatepickerlocator = {:id => @EditableDealFormInceptionDatepickerID}
  @@EditableDealFormTargetDatelocator = {:id => @EditableDealFormTargetDateID}
  @@EditableDealFormTargetDateTogglelocator = {:id => @EditableDealFormTargetDateToggleID,:class => @EditableDealFormTargetDatePickerClass}
  @@EditableDealFormTargetDatePickerlocator = {:id => @EditableDealFormTargetDatePickerID}
  @@EditableDealFormSubmittedlocator = {:id => @EditableDealFormSubmittedID}
  @@EditableDealFormSubmittedTogglelocator = {:id => @EditableDealFormSubmittedToggleID}
  @@EditableDealFormSubmittedDatePickerlocator = {:id => @EditableDealFormSubmittedDatePickerID}
  @@EditableDealFormPrioritylocator = {:id => @EditableDealFormPriorityID}
  @@EditableDealFormUnderwritterlocator = {:id => @EditableDealFormUnderwritterID}
  @@EditableDealFormUnderwritter2locator = {:id => @EditableDealFormUnderwritter2ID}
  @@EditableDealFormTechAsstlocator = {:id => @EditableDealFormTechAsstID}
  @@EditableDealFormActuarylocator = {:id => @EditableDealFormActuaryID}
  @@EditableDealFormModelerlocator = {:id => @EditableDealFormModelerID}
  @@AgGridMenulocator = {:class => @AgGridMenuClass}
  @@AgGridMenuExportOtionlocator = {:id => @AgGridMenuExportOtionID, :visible_text => @AgGridMenuExportOptionText}
  @@AgGridMenuExcelExportOtionlocator = {:id => @AgGridMenuExportOtionID, :visible_text => @AgGridMenuExcelExportOptionText}
  @@EditableDealFormCancelButtonlocator = {:id => @EditableDealFormCancelButtonID}
  @@EditableDealFormSubmitButtonlocator = {:id => @EditableDealFormSubmitButtonID}
  @@EditableDealFormDealDetailsContainerlocator = {:class => /#{@EditableDealFormDealDetailsContainerClass}/}
  @@AgGridRootlocator = {:class => /#{@AgGridRootClass}/}
  @@EditableDealFormTargetDateMATCalendarPeriodlocator = {:class => /#{@EditableDealFormTargetDateMATCalendarPeriodClass}/}
  @@EditableDealFormTargetDateMATCalendarNextButtonlocator = {:class => /#{@EditableDealFormTargetDateMATCalendarNextButtonClass}/}
  @@EditableDealFormTargetDateMATCalendarPreviousButtonlocator = {:class => /#{@EditableDealFormTargetDateMATCalendarPreviousButtonClass}/}
  @@TimeCounter = {:id => "deal_lastUpdatedTime"}
  @@TimeCounterText = {:xpath => ".//*[@id='deal_lastUpdatedTime']/time"}
  @@RefreshButtonLocator = {:class => /#{@RefreshButtonClass}/}
  @@temp = {:xpath => "//div[@class='ag-menu-column-select-wrapper']//span[@class='ag-column-tool-panel-column-label'][contains(text(),'Inception')]"}
  @@gridHeaderCol = {:xpath => @dealGridTableColumns}
  #@@DealNameCollocator = {visible_text: @dealnamegridcolumn}#/Deal Name/}
  #@@ContractNumberCollocator = {visible_text: @contractnumbergridcolumn}#/Contract/}
  #@@InceptionCollocator = {visible_text: @inceptiongridcolumn}#/Inception/}
  #@@TargetDateCollocator = {visible_text: @targetdategridcolumn}#/Target Date/}
  #@@PriorityCollocator = {visible_text: @prioritygridcolumn}#/Priority/}
  #@@SubmittedCollocator = {visible_text: @submittedgridcolumn}#/Submitted/}
  #@@StatusCollocator = {visible_text: @statusgridcolumn}#/Status/}
  #@@DealNumberCollocator = {visible_text: @dealnamegridcolumn}#/Deal Number/}
  #@@UWNameCollocator = {visible_text: @underwriternamegridcolumn}#/Underwriter/}
  #@@UW2NameCollocator = {visible_text: @underwriter2namegridcolumn}#/Underwriter 2/}
  #@@TACollocator = {visible_text: @tagridcolumn}#/TA/}
  #@@ModelerCollocator = {visible_text: @modelergridcolumn}#/Modeler/}
  #@@ActuaryCollocator = {visible_text: @actuarygridcolumn}#/Actuary/}
  #@@UWNameCollocator = {visible_text: /Underwriter Name/}
  #@@DealGridListlocator = {:id => @dealgridlist}
  #@@
  #@@ToolMenuBodylocator = {:id => @ToolMenuBodyid}
  #@@ToolMenuHeaderlocator = {:id => @ToolMenuHeaderid}


  @fetchDbElement = DBQueries.new
  @dbquery = @fetchDbElement.getFirstAndLastName(@useridtext)
  # puts @dbquery
  @dbresult = BrowserContainer.ExecuteQuery(@dbquery)
  @dbresulthash = @dbresult[0]
  @dbresultval = @dbresulthash["displayName"]
  @Fullname = @dbresultval
  @@UserIDlocator = {:visible_text => @Fullname}

  #@DealGridlocator = {:id => @dealgrid}
  @@DealGridListlocator = {:tag_name => @dealgridlist}
  @@DealGridlocator = {:xpath => @dealgrid}
  @@DealGridloc = {:class => /ag-cell/}
  @@OnHoldlabelval = []
  @@Boundlabelval = []
  #---Notes Elements -----
  @@quickEditSubButtonElement = {:id => @quickEditSubButtonID}
  @@quickEditNotesButtonElement = {:visible_text => @quickEditNotesButtonClass}
  @@notesWindowElement = {:visible_text => @noteWindowText}
  @@notesAuthorElement = {:class => /#{@noteAuthorClass}/, :visible_text => @Fullname} #.span(:index => 0)
  @@notesWindowAllEle = {:xpath => "//div[@class='flexRow']"}
  @@notesInfoText = {:formcontrolname => @noteInfo}
  @@notesReadOnly = {:readonly => @noteReadonly}
  @@notesEditWinClass = {:class => /#{@noteEditWindowClass}/}
  @@notesCancelButtonClass = {:class => /#{@notesCancelBtn}/}
  @@notesCancelButtonType = {:type => @notesCancelBtnType}
  @@notesCancelPopupEle = {:xpath => @notesCancelPopup}
  @@notesCancelPopupNOEle = {:xpath => @notesCancelPopupNO}
  @@addNotesElement = {:visible_text => @addNotesEle}
  @@addNoteTypeEle = {:xpath => @addNoteTypeEle}
  @@selectNoteTypeEle = {:class => @selectNoteTypeEle}
  @@noteCrossIconEle = {:visible_text => @noteCrossIcon}
  #-----Documents Elements--------
  @@quickEditDocumentButtonElement = {:visible_text => @quickEditDocsButtonClass}
  @@documentsFolderSchemaEle = {:id => @documentFolderStructure}
  @@documentFolderKeyDocumentsEle = {:xpath => @documentFolderKeyDocsXPath}
  @@keyViewdocumentFolderAllDocsEle = {:xpath => @keyViewdocumentFolderAllDocsXPath}
  @@documentTreeViewLinkEle = {:xpath => @documentTreeViewLink}
  @@keyDocsViewKeyDocsEle = {:xpath => @keyDocsViewKeyDocsList}

  def fetchEditableFormElement(fieldname)
    case fieldname
      when "DEAL NAME"
        return fetchEditableDealFormDealNameElement
      when "CONTRACT NUMBER"
        return fetchEditableDealFormContactNumberElement
      when "STATUS"
        return fetchEditableDealFormDealStatusElement
      when "DEAL NUMBER"
        return fetchEditableDealFormDealNumberElement
      when "INCEPTION DATE"
        return fetchEditableDealFormInceptionDateElement
      when "Inception Date Toggle"
        return fetchEditableDealFormInceptionDateToggleElement
      when "Inception Date Picker"
        return fetchEditableDealFormInceptionDatepickerElement
      when "TARGET DATE"
        return fetchEditableDealFormTargetDateElement
      when "Target Date Toggle"
        return fetchEditableDealFormTargetDateToggleElement
      when "Target Date Picker"
        return fetchEditableDealFormTargetDatePickerElement
      when "SUBMITTED"
        return fetchEditableDealFormSubmittedElement
      when "Submitted Toggle"
        return fetchEditableDealFormSubmittedToggleElement
      when "Submitted Date Picker"
        return fetchEditableDealFormSubmittedDatePickerElement
      when "PRIORITY"
        return fetchEditableDealFormPriorityElement
      when "UNDERWRITER"
        return fetchEditableDealFormUnderwritterElement
      when "UNDERWRITER 2"
        return fetchEditableDealFormUnderwritter2Element
      when "TA"
        return fetchEditableDealFormTechAsstElement
      when "ACTUARY"
        return fetchEditableDealFormActuaryElement
      when "MODELER"
        return fetchEditableDealFormModelerElement
    end
  end


  def fetchpanelElement(status)
    @NeededStatus = status
    case @NeededStatus
      when "On Hold"
        @NeededElement = fetchOnHoldBtnElement
        return @NeededElement
      when "Bound - Pending Actions"
        @NeededElement = fetchBoundBtnElement
        return @NeededElement
      when "In Progress"
        @NeededElement = fetchInprogressBtnElement
        return @NeededElement
      when "Renewable - 6 Months"
        @NeededElement = fetchRenewable6monthsBtnElement
        return @NeededElement
      when "DUMMY - DEV USE Only"
        @NeededElement = fetchDummywithSubstatusButton
        return @NeededElement
      when "DUMMY - QA USE Only"
        @NeededElement = fetchDummywithnoSubstatusButton
        return @NeededElement
    end
  end
  def fetchpanelSubStatusElement(substatus)
    @NeededSubStatus = substatus
    case @NeededSubStatus
      when "Under Review"
        @NeededElement = fetchInprogress_UR_CHKBX_Element
        return @NeededElement
      when "Authorize"
        @NeededElement = fetchInprogress_Authorize_CHKBX_Element
        return @NeededElement
      when "Outstanding Quote"
        @NeededElement = fetchInprogress_OQ_CHKBX_Element
        return @NeededElement
      when "To Be Declined"
        @NeededElement = fetchInprogress_TBD_CHKBX_Element
        return @NeededElement
      when "Bound Pending Data Entry"
        @NeededElement = fetchInprogress_BPDE_CHKBX_Element
        return @NeededElement
      when "Inquired"
        @NeededElement = fetchDummyInquiredsubstatus
        return @NeededElement
      when "In Due Diligence"
        @NeededElement = fetchDummyInDueDiligencesubstatus
        return @NeededElement
      when "Informational Agreement"
        @NeededElement = fetchDummyInformationalAgreementsubstatus
        return @NeededElement
    end
  end
  def fetchpanelSubStatusElementInput(substatus)
    @NeededSubStatus = substatus
    case @NeededSubStatus
      when "Under Review"
        @NeededElement = fetchInprogress_UR_CHKBX_ElementInput
        return @NeededElement
      when "Authorize"
        @NeededElement = fetchInprogress_Authorize_CHKBX_ElementInput
        return @NeededElement
      when "Outstanding Quote"
        @NeededElement = fetchInprogress_OQ_CHKBX_ElementInput
        return @NeededElement
      when "To Be Declined"
        @NeededElement = fetchInprogress_TBD_CHKBX_ElementInput
        return @NeededElement
      when "Bound Pending Data Entry"
        @NeededElement = fetchInprogress_BPDE_CHKBX_ElementInput
        return @NeededElement
      when "Inquired"
        @NeededElement = fetchDummyInquiredsubstatus_Input
        return @NeededElement
      when "In Due Diligence"
        @NeededElement = fetchDummyInDueDiligencesubstatus_Input
        return @NeededElement
      when "Informational Agreement"
        @NeededElement = fetchDummyInformationalAgreementsubstatus_Input
        return @NeededElement
    end
  end
  def fetchtoolMenuFieldElementFromGrid(columnname)

    @arrayVal = columnname.split('*')
    puts @arrayVal.to_s
    @arrayVal.each { |row|

      @NeededElement = @browser.element(:xpath => "(//div[@ref='eToolPanelColumnsContainerComp']//span[contains(text(),"+"'"+row+"'"+")])[2]//parent::div//span[@class='ag-checkbox-checked']") #.span(:class => 'ag-column-select-checkbox').span(:index => 0).
      # @NeededElement = fetchdealnameToolMenuFieldElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
      # return @NeededElement
      unselectToolMenuField(@NeededElement)

    }
  end

  def fetchGRIDFieldElementFromGrid(columnname)

      @NeededElements = @browser.element(:xpath => "//span[@class='ag-header-cell-text'][contains(text(),"+"'"+columnname+"'"+")]")
      # @NeededElement = fetchdealnameToolMenuFieldElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
      return @NeededElements
  end


  def fetchtoolMenuFieldElement(columnname)
    @FieldName = columnname
    case @FieldName
      when "Deal Name"
        # @NeededElement = @browser.element(@@temp) #.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        @NeededElement = fetchdealnameToolMenuFieldElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
      when "Contract"
        @NeededElement = fetchcontractnumberToolMenuFieldElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
      when "Inception"
        @NeededElement = fetchinceptionToolMenuFieldElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
      when "Target Date"
        @NeededElement = fetchtargetdateToolMenuFieldElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
      when "Priority"
        @NeededElement = fetchpriorityToolMenuFieldElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
      when "Submitted"
        @NeededElement = fetchsubmittedToolMenuFieldElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
      when "Status"
        @NeededElement = fetchstatusToolMenuFieldElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
      when "Deal Number"
        @NeededElement = fetchdealnumberToolMenuFieldElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
      when "Underwriter"
        @NeededElement = fetchUWNameToolMenuFieldElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
      when "Underwriter 2"
        @NeededElement = fetchUW2NameToolMenuFieldElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
      when "TA"
        @NeededElement = fetchTAToolMenuFieldElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
      when "Modeler"
        @NeededElement = fetchModelerToolMenuFieldElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
      when "Actuary"
        @NeededElement = fetchActuaryToolMenuFieldElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
      when "Expiration"
        @NeededElement = fetchExpirationToolMenuFieldElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
      when "Broker Name"
        @NeededElement = fetchbrokernameToolMenuFieldElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
      when "Broker Contact"
        @NeededElement = fetchbrokercontactToolMenuFieldElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
    end
  end
  def fetchGridFieldElement(columnname)
    @FieldName = columnname
    case @FieldName
      when "Deal Name"
        @NeededElement = fetchdealnamegridcolumnElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
      when "Contract"
        @NeededElement = fetchcontractnumbergridcolumnElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
      when "Inception"
        @NeededElement = fetchinceptiongridcolumnElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
      when "Target Date"
        @NeededElement = fetchtargetdategridcolumnElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
      when "Priority"
        @NeededElement = fetchprioritygridcolumnElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
      when "Submitted"
        @NeededElement = fetchsubmittedgridcolumnElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
      when "Status"
        @NeededElement = fetchstatusgridcolumnElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
      when "Deal Number"
        @NeededElement = fetchdealnumbergridcolumnElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
      when "Underwriter"
        @NeededElement = fetchUWNamegridcolumnElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
      when "Underwriter 2"
        @NeededElement = fetchUW2NamegridcolumnElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
      when "TA"
        @NeededElement = fetchTAgridcolumnElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
      when "Modeler"
        @NeededElement = fetchModelergridcolumnElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
      when "Actuary"
        @NeededElement = fetchActuarygridcolumnElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
      when "Expiration"
        @NeededElement = fetchExpirationgridcolumnElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
      when "Broker Name"
        @NeededElement = fetchbrokernamegridcolumnElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
      when "Broker Contact"
        @NeededElement = fetchbrokercontactgridcolumnElement#.span(:class => 'ag-column-select-checkbox').span(:index => 0).
        return @NeededElement
    end
  end

  def fetchSelectedFilterselectionElement(selection)
    @NeededSelection = selection
    case @NeededSelection
      when "Select All"
        @NeededElement = fetchGridAgColumnFilterSelectAllElement
        return @NeededElement
      when "Under Review"
        @NeededElement = fetchGridAgColumnFilterSelectURoptionElement
        return @NeededElement
      when "Authorize"
        @NeededElement = fetchGridAgColumnFilterSelectAuthorizeoptionElement
        return @NeededElement
      when "Outstanding Quote"
        @NeededElement = fetchGridAgColumnFilterSelectOQoptionElement
        return @NeededElement
      when "To Be Declined"
        @NeededElement = fetchGridAgColumnFilterSelectTBDoptionElement
        return @NeededElement
      when "Bound Pending Data Entry"
        @NeededElement = fetchGridAgColumnFilterSelectBPDEoptionElement
        return @NeededElement
      when "On Hold"
        @NeededElement = fetchGridAgColumnFilterSelectOHoptionElement
        return @NeededElement
      when "Bound"
        @NeededElement = fetchGridAgColumnFilterBoundoptionElement
        return @NeededElement
    end
  end


  def fetchGridElement
    @homepage_grid = @browser.div(@@DealGridlocator)
    return @homepage_grid
  end
  def fetchDealGridListElement
    @homepage_DealGridList = @browser.element(@@DealGridListlocator)
    return @homepage_DealGridList
  end

  def fetchDealGridElement
    @homepage_dealgrid = @browser.div(@@DealGridloc)
    return @homepage_dealgrid
  end
  def fetchDealGridtable
    #sleep 1
    @homepage_dealgrid = @browser.divs(@@DealGridloc)
    return @homepage_dealgrid
  end

  def fetchSpecificDealNumElement(dealNumber)
    #sleep 1
    @dealNumberEle = @browser.div({:visible_text => dealNumber, :class => /ag-cell ag-cell-not-inline-editing ag-cell-with-height ag-cell-no-focus ag-cell-value/})
    return @dealNumberEle
  end

  def fetchSpecificDealNumColElement(dealNumber)
    #sleep 1
    @dealNumberEle = @browser.div({:visible_text => dealNumber})
    return @dealNumberEle
  end

  def fetchDealGridColumns
    #sleep 1
    @homepage_dealgridCol = @browser.elements(@@gridHeaderCol)
    return @homepage_dealgridCol
  end

  def fetchOnHoldBtnElement
    @homepage_OnHoldBTN = @browser.element(@@OnHoldbuttonlocator)
    return @homepage_OnHoldBTN
  end
  def fetchBoundBtnElement
    @homepage_BoundBTN = @browser.element(@@Boundbuttonlocator)
    return @homepage_BoundBTN
  end
  def fetchInprogressBtnElement
    @homepage_InprogressBTN = @browser.element(@@Inprogressbuttonlocator)
    return @homepage_InprogressBTN
  end
  def fetchRenewable6monthsBtnElement
    @homepage_Renewable6monthsBTN = @browser.element(@@Renewable6monthsbuttonlocator)
    return @homepage_Renewable6monthsBTN
  end
  def fetchInprogress_UR_CHKBX_Element
    @homepage_Inprogress_UR_CHKBX = @browser.element(@@Inprogress_URCheckboxlocator)
    return @homepage_Inprogress_UR_CHKBX
  end
  def fetchInprogress_Authorize_CHKBX_Element
    @homepage_Inprogress_Authorize_CHKBX = @browser.element(@@Inprogress_AuthorizeCheckboxlocator)
    return @homepage_Inprogress_Authorize_CHKBX
  end
  def fetchInprogress_OQ_CHKBX_Element
    @homepage_Inprogress_OQ_CHKBX = @browser.element(@@InprogressOQCheckboxlocator)
    return @homepage_Inprogress_OQ_CHKBX
  end
  def fetchInprogress_TBD_CHKBX_Element
    @homepage_Inprogress_TBD_CHKBX = @browser.element(@@InprogressTBDCheckboxlocator)
    return @homepage_Inprogress_TBD_CHKBX
  end
  def fetchInprogress_BPDE_CHKBX_Element
    @homepage_Inprogress_BPDE_CHKBX = @browser.element(@@Inprogress_BPDECheckboxlocator)
    return @homepage_Inprogress_BPDE_CHKBX
  end

  def fetchInprogress_UR_CHKBX_ElementInput
    @homepage_Inprogress_UR_CHKBXInput = @browser.element(@@Inprogress_URCheckboxInputlocator)
    return @homepage_Inprogress_UR_CHKBXInput
  end
  def fetchInprogress_Authorize_CHKBX_ElementInput
    @homepage_Inprogress_Authorize_CHKBXInput = @browser.element(@@Inprogress_AuthorizeCheckboxInputlocator)
    return @homepage_Inprogress_Authorize_CHKBXInput
  end
  def fetchInprogress_OQ_CHKBX_ElementInput
    @homepage_Inprogress_OQ_CHKBXInput = @browser.element(@@InprogressOQCheckboxInputlocator)
    return @homepage_Inprogress_OQ_CHKBXInput
  end
  def fetchInprogress_TBD_CHKBX_ElementInput
    @homepage_Inprogress_TBD_CHKBXInput = @browser.element(@@InprogressTBDCheckboxInputlocator)
    return @homepage_Inprogress_TBD_CHKBXInput
  end
  def fetchInprogress_BPDE_CHKBX_ElementInput
    @homepage_Inprogress_BPDE_CHKBXInput = @browser.element(@@Inprogress_BPDECheckboxInputlocator)
    return @homepage_Inprogress_BPDE_CHKBXInput
  end

  def fetchInprogress_UR_CHKBX_ElementInput1
    @homepage_Inprogress_UR_CHKBXInput = @browser.element(@@Inprogress_URCheckboxInputlocator1)
    return @homepage_Inprogress_UR_CHKBXInput
  end
  def fetchQlikViewBtnElement
    @homepage_QlikViewBTN = @browser.element(@@QlikViewButtonLocator)
    return @homepage_QlikViewBTN
  end

  def fetchQuickLinksElement
    @homepage_QuickLinks = @browser.element(@@quickLinksLocator)
    return @homepage_QuickLinks
  end

  def fetchERMSBtnElement
    @homepage_ERMSBTN = @browser.a(@@ERMSButtonLocator)
    return @homepage_ERMSBTN
  end
  def fetchdealnamegridcolumnElement
    @homepage_dealnamegridcolumn = @browser.div(@@DealNameCollocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_dealnamegridcolumn
  end
  def fetchcontractnumbergridcolumnElement
    @homepage_contractnumbergridcolumn = @browser.div(@@ContractNumberCollocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_contractnumbergridcolumn
  end
  def fetchinceptiongridcolumnElement
    @homepage_inceptiongridcolumn = @browser.div(@@InceptionCollocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_inceptiongridcolumn
  end
  def fetchtargetdategridcolumnElement
    @homepage_targetdategridcolumn = @browser.div(@@TargetDateCollocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_targetdategridcolumn
  end
  def fetchprioritygridcolumnElement
    @homepage_prioritygridcolumn = @browser.div(@@PriorityCollocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_prioritygridcolumn
  end
  def fetchsubmittedgridcolumnElement
    @homepage_submittedgridcolumn = @browser.div(@@SubmittedCollocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_submittedgridcolumn
  end
  def fetchstatusgridcolumnElement
    @homepage_statusgridcolumn = @browser.div(@@StatusCollocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_statusgridcolumn
  end
  def fetchdealnumbergridcolumnElement
    @homepage_dealnumbergridcolumn = @browser.div(@@DealNumberCollocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_dealnumbergridcolumn
  end
  def fetchUWNamegridcolumnElement
    @homepage_underwriternamegridcolumn = @browser.div(@@UWNameCollocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_underwriternamegridcolumn
  end
  def fetchUW2NamegridcolumnElement
    @homepage_underwriter2namegridcolumn = @browser.div(@@UW2NameCollocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_underwriter2namegridcolumn
  end
  def fetchTAgridcolumnElement
    @homepage_TAnamegridcolumn = @browser.div(@@TACollocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_TAnamegridcolumn
  end
  def fetchModelergridcolumnElement
    @homepage_modelernamegridcolumn = @browser.div(@@ModelerCollocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_modelernamegridcolumn
  end
  def fetchActuarygridcolumnElement
    @homepage_actuarynamegridcolumn = @browser.div(@@ActuaryCollocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_actuarynamegridcolumn
  end
  def fetchExpirationgridcolumnElement
    @homepage_expirationgridcolumn = @browser.div(@@ExpirationCollocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_expirationgridcolumn
  end
  def fetchbrokernamegridcolumnElement
    @homepage_brokernamegridcolumn = @browser.div(@@BrokerNameCollocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_brokernamegridcolumn
  end
  def fetchbrokercontactgridcolumnElement
    @homepage_brokercontactgridcolumn = @browser.div(@@BrokerContactCollocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_brokercontactgridcolumn
  end

  def fetchuseridelement
    @homepage_UserID = @browser.element(@@UserIDlocator)
    return @homepage_UserID
  end
  def fetchToolMenuHeaderelement
    @homepage_ToolMenuHeader = @browser.element(@@ToolMenuHeaderlocator)
    return @homepage_ToolMenuHeader
  end
  def fetchToolMenuBodyelement
    @homepage_ToolMenuBody = @browser.element(@@ToolMenuBodylocator)
    return @homepage_ToolMenuBody
  end

  def fetchToolMenuHeaderTabelement
    @homepage_ToolMenuHeaderTab = @browser.element(@@ToolMenuHeaderTablocator)
    return @homepage_ToolMenuHeaderTab
  end

  def fetchToolMenuColumnSelectPanelelement
    @homepage_ToolMenuColumnSelectPanel = @browser.element(@@ToolMenuColumnSelectPanellocator)
    return @homepage_ToolMenuColumnSelectPanel
  end
  def fetchToolMenuToolPanelOptionelement
    @homepage_ToolMenuToolPanelOptionelement = @browser.element(@@ToolMenuToolPanelOptionFieldlocator)
    return @homepage_ToolMenuToolPanelOptionelement
  end

  def fetchMenuFieldElement(fieldelement)
    @FieldEle = fieldelement
    # @FieldEle.
    #@ElementToBeClick = @browser.span(:xpath => "//div[@id='borderLayout_eGridPanel']/div/div/div/div[3]/div/div/div[4]/div[2]/span/span") #:class => /ag-header-cell-menu-button/)
    @FieldMenuElement =  @FieldEle.parent.span(:class => /ag-header-cell-menu-button/)
    #print @FieldMenuElement.text#(:class => /ag-header-cell-menu-button/)
    return @FieldMenuElement
  end

  def fetchTeamButtonelement
    @homepage_TeamButtonelement = @browser.element(@@TeamButtonlocator)
    return @homepage_TeamButtonelement
  end
  def fetchSubDivisionOverlayPaneelement
    @homepage_SubDivisionOverlayPaneelement = @browser.element(@@SubDivisionOverlayPanelocator)
    return @homepage_SubDivisionOverlayPaneelement
  end
  def fetchSubDivisionCasualtyelement
    @homepage_SubDivisionCasualtyelement = @browser.element(@@SubDivisionCasualtylocator)
    return @homepage_SubDivisionCasualtyelement
  end
  def fetchSubDivisionCasualtyTreatyelement
    @homepage_SubDivisionCasualtyTreatyelement = @browser.element(@@SubDivisionCasualtyTreatylocator)
    return @homepage_SubDivisionCasualtyTreatyelement
  end
  def fetchSubDivisionCasFacelement
    @homepage_SubDivisionCasFacelement = @browser.element(@@SubDivisionCasFaclocator)
    return @homepage_SubDivisionCasFacelement
  end
  def fetchSubDivisionPropertyelement
    @homepage_SubDivisionPropertyelement = @browser.element(@@SubDivisionPropertylocator)
    return @homepage_SubDivisionPropertyelement
  end
  def fetchSubDivisionIntlPropertyelement
    @homepage_SubDivisionIntlPropertyelement = @browser.element(@@SubDivisionIntlPropertylocator)
    return @homepage_SubDivisionIntlPropertyelement
  end
  def fetchSubDivisionNAPropertyelement
    @homepage_SubDivisionNAPropertyelement = @browser.element(@@SubDivisionNAPropertylocator)
    return @homepage_SubDivisionNAPropertyelement
  end
  def fetchSubDivisionSpecialtyelement
    @homepage_SubDivisionSpecialtyelement = @browser.element(@@SubDivisionSpecialtylocator)
    return @homepage_SubDivisionSpecialtyelement
  end
  def fetchSubDivisionSpecialtyNonPEelement
    @homepage_SubDivisionSpecialtyNonPEelement = @browser.element(@@SubDivisionSpecialtyNonPElocator)
    return @homepage_SubDivisionSpecialtyNonPEelement
  end
  def fetchSubDivisionPublicEntityelement
    @homepage_SubDivisionPublicEntityelement = @browser.element(@@SubDivisionPublicEntitylocator)
    return @homepage_SubDivisionPublicEntityelement
  end

  def fetchdealnameToolMenuFieldElement
    @homepage_dealnameToolMenuFieldElement = @browser.div(@@DealNameToolMenFieldlocator)#$DealNameCollocator)
    # @homepage_dealnameToolMenuFieldElement = @browser.span(visible_text: /Deal Number/ )
    # @@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_dealnameToolMenuFieldElement
  end
  def fetchcontractnumberToolMenuFieldElement
    @homepage_contractnumberToolMenuFieldElement = @browser.div(@@ContractNumberToolMenFieldlocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_contractnumberToolMenuFieldElement
  end
  def fetchinceptionToolMenuFieldElement
    @homepage_inceptionToolMenuFieldElement = @browser.div(@@InceptionToolMenFieldlocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_inceptionToolMenuFieldElement
  end
  def fetchtargetdateToolMenuFieldElement
    @homepage_targetdateToolMenuFieldElement = @browser.div(@@TargetDateToolMenFieldlocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_targetdateToolMenuFieldElement
  end
  def fetchpriorityToolMenuFieldElement
    @homepage_priorityToolMenuFieldElement = @browser.div(@@PriorityToolMenFieldlocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_priorityToolMenuFieldElement
  end
  def fetchsubmittedToolMenuFieldElement
    @homepage_submittedToolMenuFieldElement = @browser.div(@@SubmittedToolMenFieldlocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_submittedToolMenuFieldElement
  end
  def fetchstatusToolMenuFieldElement
    @homepage_statusToolMenuFieldElement = @browser.div(@@StatusToolMenFieldlocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_statusToolMenuFieldElement
  end
  def fetchdealnumberToolMenuFieldElement
    @homepage_dealnumberToolMenuFieldElement = @browser.div(@@DealNumberToolMenFieldlocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_dealnumberToolMenuFieldElement
  end
  def fetchUWNameToolMenuFieldElement
    @homepage_underwriternameToolMenuFieldElement = @browser.div(@@UWNameToolMenFieldlocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_underwriternameToolMenuFieldElement
  end
  def fetchUW2NameToolMenuFieldElement
    @homepage_underwriter2nameToolMenuFieldElement = @browser.div(@@UW2NameToolMenFieldlocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_underwriter2nameToolMenuFieldElement
  end
  def fetchTAToolMenuFieldElement
    @homepage_TAnameToolMenuFieldElement = @browser.div(@@TAToolMenFieldlocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_TAnameToolMenuFieldElement
  end

  def fetchModelerToolMenuFieldElement
    @homepage_modelernameToolMenuFieldElement = @browser.div(@@ModelerToolMenFieldlocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_modelernameToolMenuFieldElement
  end
  def fetchActuaryToolMenuFieldElement
    @homepage_actuarynameToolMenuFieldElement = @browser.div(@@ActuaryToolMenFieldlocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_actuarynameToolMenuFieldElement
  end
  def fetchExpirationToolMenuFieldElement
    @homepage_expirationToolMenuFieldElement = @browser.div(@@ExpirationToolMenFieldlocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_expirationToolMenuFieldElement
  end
  def fetchbrokernameToolMenuFieldElement
    @homepage_brokernameToolMenuFieldElement = @browser.div(@@BrokerNameToolMenFieldlocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_brokernameToolMenuFieldElement
  end
  def fetchbrokercontactToolMenuFieldElement
    @homepage_brokercontactToolMenuFieldElement = @browser.div(@@BrokerContactToolMenFieldlocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_brokercontactToolMenuFieldElement
  end
  def fetchGridRightFloatingBottomElement
    @homepage_GridRightFloatingBottomElement = @browser.div(@@GridRightFloatingBottomlocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_GridRightFloatingBottomElement
  end
  def fetchGridCellElement
    @homepage_GridCellElement = @browser.div(@@GridCelllocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_GridCellElement
  end
  def fetchGridRowLevel0locator
    @homepage_GridRowLevel0Element = @browser.element(@@RowLevel0locator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_GridRowLevel0Element
  end
  def fetchGridAgIconFilterElement
    @homepage_GridAgIconFilterElement = @browser.span(@@GridAgIconFilterClasslocator)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_GridAgIconFilterElement
  end
  def fetchDummywithSubstatusButton
    @homepage_DummywithSubstatusButton = @browser.element(@@DummywithSubstatusButton)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_DummywithSubstatusButton
  end
  def fetchDummyInquiredsubstatus
    @homepage_DummyInquiredsubstatus = @browser.element(@@DummyInquiredsubstatus)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_DummyInquiredsubstatus
  end
  def fetchDummyInDueDiligencesubstatus
    @homepage_DummyInDueDiligencesubstatus = @browser.element(@@DummyInDueDiligencesubstatus)#$DealNameCollocator)
    #@@homepage_dealnamegridcolumn = @browser.span(:visible_text, /Deal Number/)#$DealNameCollocator)
    return @homepage_DummyInDueDiligencesubstatus
  end
  def fetchDummyInformationalAgreementsubstatus
    @homepage_DummyInformationalAgreementsubstatus = @browser.element(@@DummyInformationalAgreementsubstatus)#$DealNameCollocator)
    return @homepage_DummyInformationalAgreementsubstatus
  end

  def fetchDummyInquiredsubstatus_Input
    @homepage_DummyInquiredsubstatusinput = @browser.element(@@DummyInquiredsubstatusinput)#$DealNameCollocator)
    return @homepage_DummyInquiredsubstatusinput
  end
  def fetchDummyInDueDiligencesubstatus_Input
    @homepage_DummyInDueDiligencesubstatusinput = @browser.element(@@DummyInDueDiligencesubstatusinput)#$DealNameCollocator)
    return @homepage_DummyInDueDiligencesubstatusinput
  end
  def fetchDummyInformationalAgreementsubstatus_Input
    @homepage_DummyInformationalAgreementsubstatusinput = @browser.element(@@DummyInformationalAgreementsubstatusinput)#$DealNameCollocator)
    return @homepage_DummyInformationalAgreementsubstatusinput
  end
  def fetchDummywithnoSubstatusButton
    @homepage_DummywithnoSubstatusButton = @browser.element(@@DummywithnoSubstatusButton)#$DealNameCollocator)
    return @homepage_DummywithnoSubstatusButton
  end
  def fetchGridAgColumnFilterElement
    @homepage_GridAgColumnFilterElement = @browser.select_list(@@GridAgColumnFilterlocator)#$DealNameCollocator)
    return @homepage_GridAgColumnFilterElement
  end
  def fetchGridAgColumnFilterEqualOptionElement
    @homepage_GridAgColumnFilterEqualOptionElement = @browser.element(@@GridAgColumnFilterEqualOptionlocator)#$DealNameCollocator)
    return @homepage_GridAgColumnFilterEqualOptionElement
  end
  def fetchGridAgColumnFilterGTOptionElement
    @homepage_GridAgColumnFilterGTOptionElement = @browser.element(@@GridAgColumnFilterGTOptionlocator)#$DealNameCollocator)
    return @homepage_GridAgColumnFilterGTOptionElement
  end
  def fetchGridAgColumnFilterLTOptionElement
    @homepage_GridAgColumnFilterLTOptionElement = @browser.element(@@GridAgColumnFilterLTOptionlocator)#$DealNameCollocator)
    return @homepage_GridAgColumnFilterLTOptionElement
  end
  def fetchGridAgColumnFilterNEOptionElement
    @homepage_GridAgColumnFilterNEOptionElement = @browser.element(@@GridAgColumnFilterNEOptionlocator)#$DealNameCollocator)
    return @homepage_GridAgColumnFilterNEOptionElement
  end
  def fetchGridAgColumnFilterIROptionElement
    @homepage_GridAgColumnFilterIROptionElement = @browser.element(@@GridAgColumnFilterIROptionlocator)#$DealNameCollocator)
    return @homepage_GridAgColumnFilterIROptionElement
  end
  def fetchGridAgColumnFilterLTOEqualOptionElement
    @homepage_GridAgColumnFilterLTOEqualOptionElement = @browser.element(@@GridAgColumnFilterLTOEqualOptionlocator)#$DealNameCollocator)
    return @homepage_GridAgColumnFilterLTOEqualOptionElement
  end
  def fetchGridAgColumnFilterGTOEqualOptionElement
    @homepage_GridAgColumnFilterGTOEqualOptionElement = @browser.element(@@GridAgColumnFilterGTOEqualOptionlocator)#$DealNameCollocator)
    return @homepage_GridAgColumnFilterGTOEqualOptionElement
  end
  def fetchGridAgColumnFilterContainsOptionElement
    @homepage_GridAgColumnFilterContainsOptionElement = @browser.element(@@GridAgColumnFilterContainsOptionlocator)#$DealNameCollocator)
    return @homepage_GridAgColumnFilterContainsOptionElement
  end
  def fetchGridAgColumnFilternotContainsOptionElement
    @homepage_GridAgColumnFilternotContainsOptionElement = @browser.element(@@GridAgColumnFilternotContainsOptionlocator)#$DealNameCollocator)
    return @homepage_GridAgColumnFilternotContainsOptionElement
  end
  def fetchGridAgColumnFilterSelectAllElement
    @homepage_GridAgColumnFilterSelectAllElement = @browser.element(@@GridAgColumnFilterSelectAllIdlocator)#$DealNameCollocator)
    return @homepage_GridAgColumnFilterSelectAllElement
  end
  def fetchGridAgColumnFilterSelectAuthorizeoptionElement
    @homepage_GridAgColumnFilterSelectAuthorizeoptionElement = @browser.element(@@GridAgColumnFilterSelectAuthorizeoptionlocator)#$DealNameCollocator)
    return @homepage_GridAgColumnFilterSelectAuthorizeoptionElement
  end
  def fetchGridAgColumnFilterSelectURoptionElement
    @homepage_GridAgColumnFilterSelectURoptionElement = @browser.element(@@GridAgColumnFilterSelectURoptionlocator)#$DealNameCollocator)
    return @homepage_GridAgColumnFilterSelectURoptionElement
  end
  def fetchGridAgColumnFilterSelectBPDEoptionElement
    @homepage_GridAgColumnFilterSelectBPDEoptionElement = @browser.element(@@GridAgColumnFilterSelectBPDEoptionlocator)#$DealNameCollocator)
    return @homepage_GridAgColumnFilterSelectBPDEoptionElement
  end
  def fetchGridAgColumnFilterSelectOQoptionElement
    @homepage_GridAgColumnFilterSelectOQoptionElement = @browser.element(@@GridAgColumnFilterSelectOQoptionlocator)#$DealNameCollocator)
    return @homepage_GridAgColumnFilterSelectOQoptionElement
  end
  def fetchGridAgColumnFilterSelectTBDoptionElement
    @homepage_GridAgColumnFilterSelectTBDoptionElement = @browser.element(@@GridAgColumnFilterSelectTBDoptionlocator)#$DealNameCollocator)
    return @homepage_GridAgColumnFilterSelectTBDoptionElement
  end
  def fetchGridAgColumnFilterSelectOHoptionElement
    @homepage_GridAgColumnFilterSelectOHoptionElement = @browser.element(@@GridAgColumnFilterSelectOHoptionlocator)#$DealNameCollocator)
    return @homepage_GridAgColumnFilterSelectOHoptionElement
  end
  def fetchGridAgColumnFilterBoundoptionElement
    @homepage_GridAgColumnFilterBoundoptionElement = @browser.element(@@GridAgColumnFilterBoundoptionlocator)#$DealNameCollocator)
    return @homepage_GridAgColumnFilterBoundoptionElement
  end
  def fetchGridAgColumnFilterStatusInputElement
    @homepage_GridAgColumnFilterStatusInputElement = @browser.element(@@GridAgColumnFilterStatusInputlocator)#$DealNameCollocator)
    return @homepage_GridAgColumnFilterStatusInputElement
  end
  def fetchGridAgColumnFilterInputElement
    @homepage_GridAgColumnFilterInputElement = @browser.element(@@GridAgColumnFilterInputlocator)#$DealNameCollocator)
    return @homepage_GridAgColumnFilterInputElement
  end
  def fetchGridAgColumnFilterApplyButtonElement
    @homepage_GridAgColumnFilterApplyButtonElement = @browser.button(@@GridAgColumnFilterApplyButtonlocator)#$DealNameCollocator)
    return @homepage_GridAgColumnFilterApplyButtonElement
  end
  def fetchGridAgColumnFilterDateInputElement
    @homepage_GridAgColumnFilterDateInputElement = @browser.element(@@GridAgColumnFilterDateInputlocator)#$DealNameCollocator)
    return @homepage_GridAgColumnFilterDateInputElement
  end
  def fetchGridAgColumnFilterTextInputElement
    @homepage_GridAgColumnFilterTextInputElement = @browser.element(@@GridAgColumnFilterTextInputlocator)#$DealNameCollocator)
    return @homepage_GridAgColumnFilterTextInputElement
  end
  def fetchEditableDealFormDealNameElement
    @homepage_EditableDealFormDealNameElement = @browser.text_field(@@EditableDealFormDealNamelocator)#$DealNameCollocator)
    return @homepage_EditableDealFormDealNameElement
  end
  def fetchEditableDealFormContactNumberElement
    @homepage_EditableDealFormContactNumberElement = @browser.element(@@EditableDealFormContactNumberlocator)
    return @homepage_EditableDealFormContactNumberElement
  end
  def fetchEditableDealFormDealStatusElement
    @homepage_EditableDealFormDealStatusElement = @browser.element(@@EditableDealFormDealStatuslocator)
    return @homepage_EditableDealFormDealStatusElement
    end
  def fetchEditableDealFormDealNumberElement
    @homepage_EditableDealFormDealNumberElement = @browser.element(@@EditableDealFormDealNumberlocator)
    return @homepage_EditableDealFormDealNumberElement
  end
  def fetchEditableDealFormInceptionDateElement
    @homepage_EditableDealFormInceptionDateElement = @browser.element(@@EditableDealFormInceptionDatelocator)
    return @homepage_EditableDealFormInceptionDateElement
  end
  def fetchEditableDealFormInceptionDateToggleElement
    @homepage_EditableDealFormInceptionDateToggleElement = @browser.element(@@EditableDealFormInceptionDateToggle)
    return @homepage_EditableDealFormInceptionDateToggleElement
  end
  def fetchEditableDealFormInceptionDatepickerElement
    @homepage_EditableDealFormInceptionDatepickerElement = @browser.element(@@EditableDealFormInceptionDatepickerlocator)
    return @homepage_EditableDealFormInceptionDatepickerElement
  end
  def fetchEditableDealFormTargetDateElement
    @homepage_EditableDealFormTargetDateElement = @browser.text_field(@@EditableDealFormTargetDatelocator)
    return @homepage_EditableDealFormTargetDateElement
  end
  def fetchEditableDealFormTargetDateToggleElement
    @homepage_EditableDealFormTargetDateToggleElement = @browser.element(@@EditableDealFormTargetDateTogglelocator)
    return @homepage_EditableDealFormTargetDateToggleElement
  end
  def fetchEditableDealFormTargetDatePickerElement
    @homepage_EditableDealFormTargetDatePickerElement = @browser.span(@@EditableDealFormTargetDatePickerlocator)
    return @homepage_EditableDealFormTargetDatePickerElement
  end
  def fetchEditableDealFormSubmittedElement
    @homepage_EditableDealFormSubmittedElement = @browser.element(@@EditableDealFormSubmittedlocator)
    return @homepage_EditableDealFormSubmittedElement
  end
  def fetchEditableDealFormSubmittedToggleElement
    @homepage_EditableDealFormSubmittedToggleElement = @browser.element(@@EditableDealFormSubmittedTogglelocator)
    return @homepage_EditableDealFormSubmittedToggleElement
  end
  def fetchEditableDealFormSubmittedDatePickerElement
    @homepage_EditableDealFormSubmittedDatePickerElement = @browser.element(@@EditableDealFormSubmittedDatePickerlocator)
    return @homepage_EditableDealFormSubmittedDatePickerElement
  end
  def fetchEditableDealFormPriorityElement
    @homepage_EditableDealFormPriorityElement = @browser.text_field(@@EditableDealFormPrioritylocator)
    return @homepage_EditableDealFormPriorityElement
  end
  def fetchEditableDealFormUnderwritterElement
    @homepage_EditableDealFormUnderwritterElement = @browser.element(@@EditableDealFormUnderwritterlocator)
    return @homepage_EditableDealFormUnderwritterElement
  end
  def fetchEditableDealFormUnderwritter2Element
    @homepage_EditableDealFormUnderwritter2Element = @browser.element(@@EditableDealFormUnderwritter2locator)
    return @homepage_EditableDealFormUnderwritter2Element
  end
  def fetchEditableDealFormTechAsstElement
    @homepage_EditableDealFormTechAsstElement = @browser.element(@@EditableDealFormTechAsstlocator)
    return @homepage_EditableDealFormTechAsstElement
  end
  def fetchEditableDealFormActuaryElement
    @homepage_EditableDealFormActuaryElement = @browser.element(@@EditableDealFormActuarylocator)
    return @homepage_EditableDealFormActuaryElement
  end
  def fetchEditableDealFormModelerElement
    @homepage_EditableDealFormModelerElement = @browser.element(@@EditableDealFormModelerlocator)
    return @homepage_EditableDealFormModelerElement
  end
  def fetchEditableDealFormCancelButtonElement
    @homepage_EditableDealFormCancelButtonElement = @browser.element(@@EditableDealFormCancelButtonlocator)
    return @homepage_EditableDealFormCancelButtonElement
  end
  def fetchEditableDealFormSubmitButtonElement
    @homepage_EditableDealFormSubmitButtonElement = @browser.element(@@EditableDealFormSubmitButtonlocator)
    return @homepage_EditableDealFormSubmitButtonElement
  end
  def fetchAgGridMenuElement
    @homepage_AgGridMenuElement = @browser.element(@@AgGridMenulocator)
    return @homepage_AgGridMenuElement
  end
  def fetchAgGridMenuExportOtionElement
    @homepage_AgGridMenuExportOtionElement = @browser.element(@@AgGridMenuExportOtionlocator)
    return @homepage_AgGridMenuExportOtionElement
  end
  def fetchAgGridMenuExcelExportOtionElement
    @homepage_AgGridMenuExcelExportOtionElement = @browser.element(@@AgGridMenuExcelExportOtionlocator)
    return @homepage_AgGridMenuExcelExportOtionElement
  end
  def fetchAgStatusGridElement
    @homepage_AgStatusGridElement = @browser.element(@@GridAgStatusGridClasslocator)
    return @homepage_AgStatusGridElement
  end
  def fetchEditableDealFormDealDetailsContainerElement
    @homepage_EditableDealFormDealDetailsContainerElement = @browser.element(@@EditableDealFormDealDetailsContainerlocator)
    return @homepage_EditableDealFormDealDetailsContainerElement
  end
  def fetchAgGridRootElement
    @homepage_AgGridRootElement = @browser.element(@@AgGridRootlocator)
    return @homepage_AgGridRootElement
  end
  def fetchEditableDealFormTargetDateMATCalendarPeriodElement
    @homepage_EditableDealFormTargetDateMATCalendarPeriodElement = @browser.element(@@EditableDealFormTargetDateMATCalendarPeriodlocator)
    return @homepage_EditableDealFormTargetDateMATCalendarPeriodElement
  end
  def fetchEditableDealFormTargetDateMATCalendarNextButtonElement
    @homepage_EditableDealFormTargetDateMATCalendarNextButtonElement = @browser.button(@@EditableDealFormTargetDateMATCalendarNextButtonlocator)
    return @homepage_EditableDealFormTargetDateMATCalendarNextButtonElement
  end
  def fetchEditableDealFormTargetDateMATCalendarPreviousButtonElement
    @homepage_EditableDealFormTargetDateMATCalendarPreviousButtonElement = @browser.button(@@EditableDealFormTargetDateMATCalendarPreviousButtonlocator)
    return @homepage_EditableDealFormTargetDateMATCalendarPreviousButtonElement
  end

  def fetchTimeCounterelement
    @homepage_TimeCounterlement = @browser.element(@@TimeCounter)
    return @homepage_TimeCounterlement
  end

  # ----- Boya Created the funciton on 10 jan 2019
  def fetchRefreshButtonIconelement
    @homepage_RefreshIconlement = @browser.element(@@RefreshButtonLocator)
    return @homepage_RefreshIconlement
  end

  def scrollPage(element)
    @configObj = Config.new
    if @configObj.getBrowserMode == "ie"
      element.send_keys :control, :home
    else
      @action = @browser.driver.action
      @action.key_down(:control)
      @action.perform
      sleep 1
      # @action.send_keys :control
      @action.key_down(:home)
      # @clickedelement.click
      @action.key_up(:control)
      @action.key_up(:home)
      @action.perform
    end

  end
  def ClickBtnwithCtrlKey(celement)
    @clickedelement = celement
    @action = @browser.driver.action
    @action.key_down(:control)
    @action.perform
    sleep 1
    @action.send_keys :control
    @clickedelement.click
    @action.key_up(:control)
    @action.perform
  end
  def rightclickCell(fetchedcellelement)
    @gridcellelement = fetchedcellelement
    @gridcellelement.right_click
    print "Successfully right clicked on the mentioned element.\n"
  end
  def verifyGridMenuDisplayed(gMenuElement)
    @GridMenuElement = gMenuElement
    if @GridMenuElement.exist?
      print "PASSED - Grid Menu is displayed.\n"
    else
      print "FAILED - Grid Menu is not displayed.\n"
      fail "FAILED - Grid Menu is not displayed.\n"
    end
  end



  def ClickBtn(celement)
    #fetchOnHoldBtnElement
    @clickedelement = celement
    #print "\n"
    #print @clickedelement.text
    #print "\n"
    #@elementavailable = @clickedelement.exist?
    #print @elementavailable
    #@elementenabled = @clickedelement.attribute_value('disabled')
    #print @elementenabled
    #if @elementavailable==true && @elementenabled==false
    sleep 2
    @clickedelement.click

    #  print "PASSED - Element successfully Clicked.\n"
    #  @reporter.ReportAction("PASSED - Element successfully Clicked.")
    #else
    #  print "Failed - Element Not available or is disabled.\n"
    #  @reporter.ReportAction("Failed - Element Not available or is disabled.")
    #end

      #@@homepage_OnHoldBTN.click
    #sleep(20)

    @elementenabled = @clickedelement.enabled?
    puts 'element : ' + @elementenabled.to_s
    if @elementenabled == true
      print "PASSED - Element successfully Clicked and is enabled.\n"
    else
      print "Failed - Element not Clicked and is disabled.\n"
      fail "Failed - Element not Clicked and is disabled.\n"
    end



  end
  #def ClickBoundBtn
  #  fetchBoundBtnElement
  #  @@homepage_BoundBTN.click
  #  #sleep(20)
  #end
  #def fetchGridvalues

  #end
  def ClickGridCol(fetchedcolelement)
    #@gridcolelement = fetchdealnamegridcolumnElement#fetchedcolelement
    @gridcolelement = fetchedcolelement
    #fetchdealnamegridcolumnElement
    #@gridcolelement = @@homepage_dealnamegridcolumn
      sleep 3
      @gridcolelement.focus()
    # @gridcolelement.focus
    # sleep 2
    @gridcolelement.click
    #sleep 2
    #fetchdealnamegridcolumnElement
    #@@homepage_dealnamegridcolumn.click
    #sleep(20)
  end

  def scrollTo(obj,location)
    sleep 1
    if location == "top"
    obj.scroll.to :top
    end
  end


  def doubleClickOnElement(fetchedcolelement)
    #@gridcolelement = fetchdealnamegridcolumnElement#fetchedcolelement
    @gridcolelement = fetchedcolelement
    #fetchdealnamegridcolumnElement
    #@gridcolelement = @@homepage_dealnamegridcolumn
    sleep 2
    @gridcolelement.focus

    # @gridcolelement.focus
    # sleep 2
    @gridcolelement.double_click
    #sleep 2
    #fetchdealnamegridcolumnElement
    #@@homepage_dealnamegridcolumn.click
    #sleep(20)
  end

  def VerifyDescendingSort(fetchedcolelement)
    @gridcolelementval = fetchedcolelement
    #@gridcolelement.collect do |item,index|
    #print @gridcolelementval.size
  end
  def VerifyAscendingSort(fetchedcolelement)
    @dealgridelement = fetchedcolelement
    #@dealgridelement.each do |dgriddiv|
    #  print dgriddiv.tr.text
    #end
    #print @dealgridelement.tr.text

    #@gridcolelementval = Array.new
    #@gridcolelementval = @gridcolelement[1].text
    #@gridcolelement.collect do |item,index|
    #print @gridcolelementval
      #print cell.text
    #end
    #print @gridcolelementval.cell.first.to_s
    #@gridcolelement.trs.each do |tr|
      #tb.tr.each do |tr|
    #    tr.td.each do |td|
    #      print td.text
    #    end
      #end
    #end
    #@gridcolelement.trs.last.click
    #sleep 10
    #@dealgridrows = Array.new
    #@dealgridrows = @dealgridelement.trs
    #@dealgridrows.each do |row|
      #print row.text
    #end
    #@dealgridtab = @dealgridelement.trs
    #@dealgridarr = @dealgridelement.tbody.tr.td.text
    #print @dealgridtab
    #@dealgridtab.each do |tr|
    #  tr.td.each do |td|
    #    print td.text
    #  end
      #print tr.text
    #end

    #sleep 10

    #print "\n"
    #print @dealgridrows.last.text
    #print "\n"
      #print "\nThe length of table is: " + @gridcolelement.width.to_s + "\n"
  end

  def checkpanelAvailable(panelelement)
    @PElement = panelelement
    sleep 3
    #@PElement.wait_until_present
    @panelavailable = @PElement.exist?
    #print "\n"
    #print @panelavailable
    #print "\n"
    #@@PanelTextValue = @panelavailable.attribute_value('innerText').to_s
    #@@Panellabelval = @@PanelTextValue.split("\n")
    if @panelavailable==true
      #print "PASSED - The " + @@Panellabelval[1] + " panel is available in the GRS Home Page.\n"
      print "PASSED - The panel is available in the GRS Home Page.\n"
      #@reporter.ReportAction("PASSED - The " + @@Panellabelval[1] + " panel is available in the GRS Home Page.")
      #@reporter.ReportAction("PASSED - The panel is available in the GRS Home Page.")
    else
      print "Failed - The panel is not available in the GRS Home Page.\n"
      fail "Failed - The panel is not available in the GRS Home Page.\n"
      #@reporter.ReportAction("Failed - The panel is not available in the GRS Home Page.")
    end
  end


  def GridAvailable(gridelement)
    @Gelement = gridelement
    sleep 1
    @GridVisible = @Gelement.exist?

    if @GridVisible==true
      print "PASSED - Grid is visible.\n"
      #@reporter.ReportAction("PASSED - " + @Gelement.attribute_value('id') +" grid is visible.")
    elsif @GridVisible==false
      print "Failed - Grid is not visible.\n"
      fail "Failed - Grid is not visible.\n"
      #@reporter.ReportAction("Failed - Grid is not visible.")
    end
  end
  def GridAvailableforstatus(gridelement,status)
    @Gelement = gridelement
    sleep 1
    @GridVisible = @Gelement.exist?
    @neededstatusElement = fetchpanelElement(status)
    @panelcount = getpanelCount(@neededstatusElement)
    # print "\n"
    # print @panelcount
    # print "\n"


    if @GridVisible.to_s == "true"
      print "PASSED - Grid is visible.\n"
      #@reporter.ReportAction("PASSED - " + @Gelement.attribute_value('id') +" grid is visible.")
    elsif @GridVisible.to_s == "false" && (@panelcount.to_i == 0 || @panelcount == "" || @panelcount == nil)
      print "PASSED - Panel Count for the " + status + " status selected is " + @panelcount + " and grid is not visible.\n"
    elsif @GridVisible.to_s == "false" && @panelcount.to_i > 0
      print "Failed - Grid is not visible.\n"
      fail "Failed - Grid is not visible.\n"
      #@reporter.ReportAction("Failed - Grid is not visible.")
    end
  end
  def draganelement(elementtobedragged,elementtowheretobedragged)
    @draggedelement =elementtobedragged
    @towheredraggedelement = elementtowheretobedragged
    @draggedelement.drag_and_drop_on(@towheredraggedelement)

      #sleep 20
  end
  def draganelementtolocation(elementtobedragged,xloc,yloc)
    @draggedelement =elementtobedragged
    @xlocation = xloc.to_i
    @ylocation = yloc.to_i
    #@AppNameElement = @browser.element(:id => "shellContainer_ApplicationName")
    #@browser.driver.click_and_hold(@draggedelement.wd).perform
    #sleep 2
    #@browser.driver.move_to(xloc,yloc).perform
    #sleep 2
    #@browser.fire_event("onmouseup")
    #@browser.driver.action.click_and_hold(@draggedelement.wd).perform
    #sleep 2
    #@location = "#{@xlocation},#{@ylocation}"
    #@browser.driver.action.move_to(@location).perform
    #sleep 1
    #@browser.driver.action.release.perform
    #@draggedelement.drag_and_drop_by @xlocation,@ylocation
    @draggedelement.drag_and_drop_on(@AppNameElement)

    #@browser.driver.action.drag_and_drop_by(@draggedelement,@xlocation,@ylocation).perform
    #@browser.driver.action.move_to(@draggedelement,@xlocation,@ylocation).perform
    #sleep 20
  end

  def loadUserwebpage(user)
    BrowserContainer.fetchenv
    begin
      if user.to_s == "All Access"
        @browser.goto($GRSApplicationURL)
        print "Loaded the GRS webpage as a " + user + " user using the link " + $GRSApplicationURL + ".\n"
      elsif user.to_s == "UW"
        @browser.goto($GRSUWApplicationURL)
        print "Loaded the GRS webpage as a " + user + " user using the link " + $GRSUWApplicationURL + ".\n"
      elsif user.to_s == "NPTA"
        @browser.goto($GRSNPTAApplicationURL)
        print "Loaded the GRS webpage as a " + user + " user using the link " + $GRSNPTAApplicationURL + ".\n"
      elsif user.to_s == "PTA"
        @browser.goto($GRSPTAApplicationURL)
        print "Loaded the GRS webpage as a " + user + " user using the link " + $GRSPTAApplicationURL + ".\n"
      elsif user.to_s == "Actuary"
        @browser.goto($GRSActuaryApplicationURL)
        print "Loaded the GRS webpage as a " + user + " user using the link " + $GRSActuaryApplicationURL + ".\n"
      elsif user.to_s == "Actuary Manager"
        @browser.goto($GRSActuaryManagerApplicationURL)
        print "Loaded the GRS webpage as a " + user + " user using the link " + $GRSActuaryManagerApplicationURL + ".\n"
      elsif user.to_s == "UW Manager"
        @browser.goto($GRSUWManagerApplicationURL)
        print "Loaded the GRS webpage as a " + user + " user using the link " + $GRSUWManagerApplicationURL + ".\n"
      elsif user.to_s == "Read Only Access"
        @browser.goto($GRSReadonlyUserApplicationURL)
        print "Loaded the GRS webpage as a " + user + " user using the link " + $GRSReadonlyUserApplicationURL + ".\n"
      end

      sleep 3
        #@browser.alert.wait_until_present
    rescue Exception => e
      puts "Trapped Error, expecting modal dialog exception"
      puts e.backtrace
      puts "Continuing"
    end
    if @browser.alert.exists?
      @wsh = WIN32OLE.new('Wscript.Shell')
      @wsh.SendKeys('sjanardanannair')
      @wsh.SendKeys('{TAB}')
      @wsh.SendKeys('Summer18')
      @browser.alert.ok
    end
    sleep 1
    # @Boundelement = fetchBoundBtnElement
    # @OnHoldelement = fetchOnHoldBtnElement
    # if (@Boundelement.exist? == false) || (@OnHoldelement.exist? == false) || (@browser.url.to_s !=  "http://d-rdc-core01:50092/ng/#/dashboard")
    #   print "\nI am Here. All Panels failed to Load and I am reloading the web page.\n"
    #   reloadUserwebpage()
    # end
  end


  def loadwebpage()
    BrowserContainer.fetchenv
    begin
      #sleep 2
      @browser.goto($GRSApplicationURL) #-- Working fine
      # @browser.goto("http://d-rdc-core01:50092/ng/")
      sleep 3
        #@browser.alert.wait_until_present
    rescue Exception => e
      puts "Trapped Error, expecting modal dialog exception"
      puts e.backtrace
      puts "Continuing"
    end
    if @browser.alert.exists?
      @wsh = WIN32OLE.new('Wscript.Shell')
      @wsh.SendKeys('vboya') #-- Boya Updated
      @wsh.SendKeys('{TAB}')
      @wsh.SendKeys('Origin2019')
      @browser.alert.ok
    end
    # sleep 1
    # @Boundelement = fetchBoundBtnElement
    # @OnHoldelement = fetchOnHoldBtnElement
    # if (@Boundelement.exist? == false) || (@OnHoldelement.exist? == false) || (@browser.url.to_s !=  "http://d-rdc-core01:50092/ng/#/dashboard")
    #   print "\nI am Here. All Panels failed to Load and I am reloading the web page.\n"
    #   reloadwebpage()
    # end
    print "Loaded the GRS webpage using the link " + $GRSApplicationURL + ".\n"
  end

  def loadwebpageInNewTab()
    BrowserContainer.fetchenv
    @gRSApplicationURL = $GRSApplicationURL
    begin
      sleep 2
      @browser.execute_script('window.open("http://d-rdc-core01:50092/ng/")')
      # @browser.link(:text, 'A/B Testing').click(:command, :shift)
      #
      # @browser.windows.last.use
      # @browser.goto($GRSApplicationURL)
      sleep 3
        #@browser.alert.wait_until_present
    rescue Exception => e
      puts "Trapped Error, expecting modal dialog exception"
      puts e.backtrace
      puts "Continuing"
    end
    if @browser.alert.exists?
      @wsh = WIN32OLE.new('Wscript.Shell')
      @wsh.SendKeys('vboya') #-- Boya Updated
      @wsh.SendKeys('{TAB}')
      @wsh.SendKeys('Origin2019')
      @browser.alert.ok
    end
    # sleep 1
    # @Boundelement = fetchBoundBtnElement
    # @OnHoldelement = fetchOnHoldBtnElement
    # if (@Boundelement.exist? == false) || (@OnHoldelement.exist? == false) || (@browser.url.to_s !=  "http://d-rdc-core01:50092/ng/#/dashboard")
    #   print "\nI am Here. All Panels failed to Load and I am reloading the web page.\n"
    #   reloadwebpage()
    # end
    print "Loaded the GRS webpage using the link " + $GRSApplicationURL + ".\n"
  end

  def reloadwebpage()
    begin
      sleep 1
      @browser.goto($GRSApplicationURL)
      sleep 1
        #@browser.alert.wait_until_present
    rescue Exception => e
      puts "Trapped Error, expecting modal dialog exception"
      puts e.backtrace
      puts "Continuing"
    end
    if @browser.alert.exists?
      @wsh = WIN32OLE.new('Wscript.Shell')
      @wsh.SendKeys('sjanardanannair')
      @wsh.SendKeys('{TAB}')
      @wsh.SendKeys('Summer18')
      @browser.alert.ok
    end
    sleep 1
  end
  def reloadUserwebpage()
    BrowserContainer.fetchenv
    begin
      if user == "All Access"
        @browser.goto($GRSApplicationURL)
        print "Loaded the GRS webpage as a " + user + " user using the link " + $GRSApplicationURL + ".\n"
      elsif user == "UW"
        @browser.goto($GRSUWApplicationURL)
        print "Loaded the GRS webpage as a " + user + " user using the link " + $GRSUWApplicationURL + ".\n"
      elsif user == "NPTA"
        @browser.goto($GRSNPTAApplicationURL)
        print "Loaded the GRS webpage as a " + user + " user using the link " + $GRSNPTAApplicationURL + ".\n"
      elsif user.to_s == "PTA"
        @browser.goto($GRSPTAApplicationURL)
        print "Loaded the GRS webpage as a " + user + " user using the link " + $GRSPTAApplicationURL + ".\n"
      elsif user == "Actuary"
        @browser.goto($GRSActuaryApplicationURL)
        print "Loaded the GRS webpage as a " + user + " user using the link " + $GRSActuaryApplicationURL + ".\n"
      elsif user.to_s == "Actuary Manager"
        @browser.goto($GRSActuaryManagerApplicationURL)
        print "Loaded the GRS webpage as a " + user + " user using the link " + $GRSActuaryManagerApplicationURL + ".\n"
      elsif user.to_s == "UW Manager"
        @browser.goto($GRSUWManagerApplicationURL)
        print "Loaded the GRS webpage as a " + user + " user using the link " + $GRSUWManagerApplicationURL + ".\n"
      end

      sleep 3
        #@browser.alert.wait_until_present
    rescue Exception => e
      puts "Trapped Error, expecting modal dialog exception"
      puts e.backtrace
      puts "Continuing"
    end
    if @browser.alert.exists?
      @wsh = WIN32OLE.new('Wscript.Shell')
      @wsh.SendKeys('sjanardanannair')
      @wsh.SendKeys('{TAB}')
      @wsh.SendKeys('Summer18')
      @browser.alert.ok
    end
    sleep 1
  end


  def verifyGRSTitle()
    #@useridelement = fetchuseridelement
    #@useridelement.wait_until_present
    @title=@browser.title
    #print "\n"
    #print @title
    #print "\n"
    @url = @browser.url
    if @title=="GRS" && @url.include?('dashboard')
      print "PASSED - The GRS Home page" + @browser.url + " successfully loaded.\n"
      #@reporter.ReportAction("PASSED - The GRS Home page" + @browser.url + " successfully loaded.\n")
    else
      print "FAILED - The GRS Home Page failed to load.\n"
      fail "FAILED - The GRS Home Page failed to load.\n"
      #@reporter.ReportAction("FAILED - The GRS Home Page failed to load.")
    end
  end
  def verifyUserID
    @useridelement = fetchuseridelement
    # puts @useridelement
    if @useridelement.exist?
      print "PASSED - The GRS Home page has the USER ID displayed.\n"
      #@reporter.ReportAction("PASSED - The GRS Home page has the USER ID displayed.")
    else
      print "FAILED - The GRS Home Page does not have the USER ID.\n"
      fail "FAILED - The GRS Home Page does not have the USER ID.\n"
      #@reporter.ReportAction("FAILED - The GRS Home Page does not have the USER ID.")
    end
  end
  def verifyElementName(elementName,elementText)
    @EleName = elementName
    @EleText = elementText
    #print @EleName.text.include?(@EleText)
    #print "\n"
    if @EleName.text.include?(@EleText)
      print "PASSED - The GRS Home page has the button with the " + @EleText + " displayed.\n"
      #@reporter.ReportAction("PASSED - The GRS Home page has the button with the " + @EleText + " displayed.\n")
    else
      print "FAILED - The GRS Home Page does not have the button with the " + @EleText + " displayed.\n"
      fail "FAILED - The GRS Home Page does not have the button with the " + @EleText + " displayed.\n"
      #@reporter.ReportAction("FAILED - The GRS Home Page does not have the button with the " + @EleText + " displayed.\n")
    end
  end
  def sendQuery(sqlquerystring)
    @sqlquery=sqlquerystring
    @resultdata = BrowserContainer.ExecuteQuery(@sqlquery)
    if @resultdata.length.to_s != "0"
      print "PASSED - Query Executed Successfully and fetched " + @resultdata.length.to_s + " records.\n"
      #@reporter.ReportAction("PASSED - Query Executed Successfully and fetched " + @resultdata.length.to_s + " records.\n")
    else
      # print "\n"
      # print @sqlquery
      # print "\n"
      print "Query fetched no results.\n"
      #@reporter.ReportAction("FAILED - Query fetched no results.\n")
    end
    return @resultdata
  end
  def sendCountQuery(sqlquerystring)
    @sqlquery=sqlquerystring
    @resultdata = BrowserContainer.ExecuteCountQuery(@sqlquery)
    #print @resultdata.to_s
    if @resultdata.nil?
      print "FAILED - Query fetched no results.\n"
      fail "FAILED - Query fetched no results.\n"
      #@reporter.ReportAction("FAILED - Query fetched no results.\n")
    else
      print "PASSED - Query Executed Successfully and fetched the count as " + @resultdata.to_s + ".\n"
      #@reporter.ReportAction("PASSED - Query Executed Successfully and fetched the count as " + @resultdata.to_s + ".\n")
    end
    return @resultdata
  end
  def getpanelCount(panelelement)
    @PElement = panelelement
    @PText = Array.new
    @PText = panelelement.text.split("\n")
    @PText.delete(nil)
    @PcountTest = @PText[0]
    return @PcountTest
    #print "/n"
    #print @PElement.text
    #print "/n"
  end
  def verifyCount(pcount,querycount)
    @panelcount = pcount
    @dbcount = querycount
    #print "\n"
    #print @panelcount
    #print "\n"
    #print @dbcount
    #print "\n"

    if @panelcount==@dbcount
      print "PASSED - Counts Matched successfully. Panel Value is " + @panelcount + " and DB Value is " + @dbcount + ".\n"
      #@reporter.ReportAction("PASSED - Counts Matched successfully.\n")
    else
      print "FAILED - Counts failed to match. Panel Value is " + @panelcount + " and DB Value is " + @dbcount + ".\n"
      fail "FAILED - Counts failed to match. Panel Value is " + @panelcount + " and DB Value is " + @dbcount + ".\n"
      #@reporter.ReportAction("FAILED - Counts failed to match.\n")
    end
  end
  def checkDealGridAvailable
    #sleep 4
    @dealgridListelement = fetchDealGridListElement
    @dealgridlistavailable = @dealgridListelement.exist?
    #Watir::Wait.until_present(@dealgridListelement)
    #@dealgridListelement.wait_until_present

    #Watir::Wait.until { @dealgridListelement.visible? }
    if @dealgridlistavailable==true
      print "PASSED - Deal Grid Displayed successfully.\n"
      #@reporter.ReportAction("PASSED - Deal Grid Displayed successfully.\n")
    else
      print "FAILED - Deal Grid failed to get displayed.\n"
      fail "FAILED - Deal Grid failed to get displayed.\n"
      #@reporter.ReportAction("FAILED - Deal Grid failed to get displayed.\n")
    end
  end

  def VerifyNeededFieldsAvailable
    @DealNameFieldElement = fetchdealnamegridcolumnElement
    @ContractNumberFieldElement = fetchcontractnumbergridcolumnElement
    @InceptionFieldElement = fetchinceptiongridcolumnElement
    @TargetDateFieldElement = fetchtargetdategridcolumnElement
    @PriorityFieldElement = fetchprioritygridcolumnElement
    @SubmittedFieldElement = fetchsubmittedgridcolumnElement
    @StatusFieldElement = fetchstatusgridcolumnElement
    @DealNumberFieldElement = fetchdealnumbergridcolumnElement
    @GridCell = fetchGridCellElement
    @GridCell.click
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @UWNameFieldElement = fetchUWNamegridcolumnElement
    @UW2NameFieldElement = fetchUW2NamegridcolumnElement
    @TAFieldElement = fetchTAgridcolumnElement
    @ModelerFieldElement = fetchModelergridcolumnElement
    @ActuaryFieldElement = fetchActuarygridcolumnElement
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @DealNameFieldElementAvailable = @DealNameFieldElement.exist?
    @ContractNumberFieldElementAvailable = @ContractNumberFieldElement.exist?
    @InceptionFieldElementAvailable = @InceptionFieldElement.exist?
    @TargetDateFieldElementAvailable = @TargetDateFieldElement.exist?
    @PriorityFieldElementAvailable = @PriorityFieldElement.exist?
    @SubmittedFieldElementAvailable = @SubmittedFieldElement.exist?
    @StatusFieldElementAvailable = @StatusFieldElement.exist?
    @DealNumberFieldElementAvailable = @DealNumberFieldElement.exist?
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @UWNameFieldElementAvailable = @UWNameFieldElement.exist?
    @UW2NameFieldElementAvailable = @UW2NameFieldElement.exist?
    @TAFieldElementAvailable = @TAFieldElement.exist?
    @ModelerFieldElementAvailable = @ModelerFieldElement.exist?
    @ActuaryFieldElementAvailable = @ActuaryFieldElement.exist?
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    # print @DealNameFieldElementAvailable
    # print @ContractNumberFieldElementAvailable
    # print @InceptionFieldElementAvailable
    # print @TargetDateFieldElementAvailable
    # print @PriorityFieldElementAvailable
    # print @StatusFieldElementAvailable
    # print @StatusFieldElementAvailable
    # print @DealNumberFieldElementAvailable
    # print @UWNameFieldElementAvailable
    # print @UW2NameFieldElementAvailable
    # print @TAFieldElementAvailable
    # print @ModelerFieldElementAvailable
    # print @ActuaryFieldElementAvailable
    if @DealNameFieldElementAvailable==true && @ContractNumberFieldElementAvailable==true && @InceptionFieldElementAvailable==true && @TargetDateFieldElementAvailable==true && @PriorityFieldElementAvailable==true && @SubmittedFieldElementAvailable==true && @StatusFieldElementAvailable==true && @DealNumberFieldElementAvailable==true && @UWNameFieldElementAvailable==true && @UW2NameFieldElementAvailable==true && @TargetDateFieldElementAvailable==true && @ModelerFieldElementAvailable==true && @ActuaryFieldElementAvailable==true
      print "PASSED - The needed fields are available in GRS Deal Grid.\n"
      #@reporter.ReportAction("PASSED - The needed fields are available in GRS Deal Grid.\n")
    else
      print "FAILED - Deal Grid does not have the needed fields. \n"
      fail "FAILED - Deal Grid does not have the needed fields. \n"
      #@reporter.ReportAction("FAILED - Deal Grid does not have the needed fields. \n")
    end
  end

  def getGridValues(dealgrid,colCount)

    @dealgridEle = dealgrid
    @dealgriddata = Hash.new #Array.new(Array.new)
    @ccount = 0
    @rcount = 0
    @gridSize = colCount-1
    @dealgridEle.each do |div|
      #if @count == 130
      #  exit
      #else
      if @ccount > @gridSize
        @ccount = 0
        @rcount = @rcount+1
      end
      # print "\n"
      # print "rowcount,columncount =" + @rcount.to_s + "," + @ccount.to_s + ". The value is :"
      # print "\n"
      # if (div.text == nil || div.text == "" || div.text.to_s == "nil") && @rcount == 0 && @ccount == 9
      #   @gridSize = 8
      # else
      #   @gridSize = 12
      # end

      # if (div.text == "0" || div.text == nil || div.text == "" || div.text == 0) && @ccount == 4
      #   print "\nI am Here \n"
      #   @dealgriddata[[@rcount,@ccount]] = nil
      # end
      if div.text == ""
        @dealgriddata[[@rcount,@ccount]] = nil
      else
        # print "\n"
        # print div.text.to_s
        # print "\n"
        @dealgriddata[[@rcount,@ccount]] = div.text.to_s
        # end
       end
      @ccount = @ccount + 1
    end
    #print "\n"
    # @colcount=4
    # for @rowcount in 1..6
    #   if @dealgriddata[[@rowcount,@colcount]] == nil
    #     @dealgriddata[[@rowcount,@colcount]] = 0
    #   end
    # end
    print "\n"
    print @dealgriddata
    print "\n"
    # @dealgriddata = @dealgriddata.delete("")
    # print "\n"
    # print @dealgriddata
    # print "\n"
    return @dealgriddata
  end

  # def getGridValues(dealgrid)
  #
  #   @dealgridEle = dealgrid
  #   @dealgriddata = Hash.new #Array.new(Array.new)
  #   @ccount = 0
  #   @rcount = 0
  #   @dealgridEle.each do |div|
  #     #if @count == 130
  #     #  exit
  #     #else
  #     if @ccount > 12
  #       @ccount = 0
  #       @rcount = @rcount+1
  #     end
  #     print "\n"
  #     print "columncount =" + @ccount.to_s + " and rowcount =" + @rcount.to_s
  #     print "\n"
  #
  #     if div.text ==""
  #       @dealgriddata[[@ccount,@rcount]] = "nil"
  #     else
  #       print "\n"
  #       print div.text
  #       print "\n"
  #
  #       @dealgriddata[[@ccount,@rcount]] = div.text.to_s
  #     end
  #     #end
  #     @ccount = @ccount+1
  #   end
  #   print "\n"
  #   print @dealgriddata
  #   print "\n"
  #   @dealgriddata.delete("")
  #   return @dealgriddata
  # end

  def ModifyGridData(griddata)
    @dealgriddata = griddata
    #@actualcompmodifieddata = Array.new
    #@actualcompmodifieddata = "{\"dealName\"=>" + @dealgriddata[0][0]+", \"contractNumber\"=>" +@dealgriddata[0][1] + ", \"inceptionDate\"=>" + @dealgriddata[0][3] + ", \"submittedDate\"=>" + @dealgriddata[0][4]+", \"status\"=>" + @dealgriddata[0][5] + ",\"dealNumber\"=>" + @dealgriddata[0][6] + ", \"primaryUnderwriterName\"=>" + @dealgriddata[0][7] + ", \"technicalAssistantName\"=>" + @dealgriddata[0][8] + "},    {\"dealName\"=>" + @dealgriddata[1][0]+", \"contractNumber\"=>" +@dealgriddata[1][1] + ", \"inceptionDate\"=>" + @dealgriddata[1][3] + ", \"submittedDate\"=>" + @dealgriddata[1][4]+", \"status\"=>" + @dealgriddata[1][5] + ",\"dealNumber\"=>" + @dealgriddata[1][6] + ", \"primaryUnderwriterName\"=>" + @dealgriddata[1][7] + ", \"technicalAssistantName\"=>" + @dealgriddata[1][8] + "},{\"dealName\"=>" + @dealgriddata[2][0]+", \"contractNumber\"=>" +@dealgriddata[2][1] + ", \"inceptionDate\"=>" + @dealgriddata[2][3] + ", \"submittedDate\"=>" + @dealgriddata[2][4]+", \"status\"=>" + @dealgriddata[2][5] + ", \"dealNumber\"=>" + @dealgriddata[2][6] + ", \"primaryUnderwriterName\"=>" + @dealgriddata[2][7] + ", \"technicalAssistantName\"=>" + @dealgriddata[2][8] + "}, {\"dealName\"=>" + @dealgriddata[3][0]+", \"contractNumber\"=>" +@dealgriddata[3][1] + ", \"inceptionDate\"=>" + @dealgriddata[3][3] + ", \"submittedDate\"=>" + @dealgriddata[3][4]+", \"status\"=>" + @dealgriddata[3][5] + ",\"dealNumber\"=>" + @dealgriddata[3][6] + ", \"primaryUnderwriterName\"=>" + @dealgriddata[3][7] + ", \"technicalAssistantName\"=>" + @dealgriddata[3][8] + "}, {\"dealName\"=>" + @dealgriddata[4][0]+", \"contractNumber\"=>" +@dealgriddata[4][1] + ", \"inceptionDate\"=>" + @dealgriddata[4][3] + ", \"submittedDate\"=>" + @dealgriddata[4][4]+", \"status\"=>" + @dealgriddata[4][5] + ",\"dealNumber\"=>" + @dealgriddata[4][6] + ", \"primaryUnderwriterName\"=>" + @dealgriddata[4][7] + ", \"technicalAssistantName\"=>" + @dealgriddata[4][8] + "},{\"dealName\"=>" + @dealgriddata[5][0]+", \"contractNumber\"=>" +@dealgriddata[5][1] + ", \"inceptionDate\"=>"+ @dealgriddata[5][3] + ", \"submittedDate\"=>" + @dealgriddata[5][4]+", \"status\"=>" + @dealgriddata[5][5] + ", \"dealNumber\"=>" + @dealgriddata[5][6] + ", \"primaryUnderwriterName\"=>" + @dealgriddata[5][7] + ", \"technicalAssistantName\"=>" + @dealgriddata[5][8] + "}"
    #@actualcompmodifieddata = "{\"dealName\"=>" << @dealgriddata[0][0]<<", \"contractNumber\"=>" <<@dealgriddata[0][1] << ", \"inceptionDate\"=>" << @dealgriddata[0][3] << ", \"submittedDate\"=>" << @dealgriddata[0][4]<<", \"status\"=>" << @dealgriddata[0][5] << ",\"dealNumber\"=>" << @dealgriddata[0][6] << ", \"primaryUnderwriterName\"=>" << @dealgriddata[0][7] << ", \"technicalAssistantName\"=>" << @dealgriddata[0][8] << "},    {\"dealName\"=>" << @dealgriddata[1][0]<<", \"contractNumber\"=>" <<@dealgriddata[1][1] << ", \"inceptionDate\"=>" << @dealgriddata[1][3] << ", \"submittedDate\"=>" << @dealgriddata[1][4]<<", \"status\"=>" << @dealgriddata[1][5] << ",\"dealNumber\"=>" << @dealgriddata[1][6] << ", \"primaryUnderwriterName\"=>" << @dealgriddata[1][7] << ", \"technicalAssistantName\"=>" << @dealgriddata[1][8] << "},{\"dealName\"=>" << @dealgriddata[2][0]<<", \"contractNumber\"=>" <<@dealgriddata[2][1] << ", \"inceptionDate\"=>" << @dealgriddata[2][3] << ", \"submittedDate\"=>" << @dealgriddata[2][4]<<", \"status\"=>" << @dealgriddata[2][5] << ", \"dealNumber\"=>" << @dealgriddata[2][6] << ", \"primaryUnderwriterName\"=>" << @dealgriddata[2][7] << ", \"technicalAssistantName\"=>" << @dealgriddata[2][8] << "}, {\"dealName\"=>" << @dealgriddata[3][0]<<", \"contractNumber\"=>" <<@dealgriddata[3][1] << ", \"inceptionDate\"=>" << @dealgriddata[3][3] << ", \"submittedDate\"=>" << @dealgriddata[3][4]<<", \"status\"=>" << @dealgriddata[3][5] << ",\"dealNumber\"=>" << @dealgriddata[3][6] << ", \"primaryUnderwriterName\"=>" << @dealgriddata[3][7] << ", \"technicalAssistantName\"=>" << @dealgriddata[3][8] << "}, {\"dealName\"=>" << @dealgriddata[4][0]<<", \"contractNumber\"=>" <<@dealgriddata[4][1] << ", \"inceptionDate\"=>" << @dealgriddata[4][3] << ", \"submittedDate\"=>" << @dealgriddata[4][4]<<", \"status\"=>" << @dealgriddata[4][5] << ",\"dealNumber\"=>" << @dealgriddata[4][6] << ", \"primaryUnderwriterName\"=>" << @dealgriddata[4][7] << ", \"technicalAssistantName\"=>" << @dealgriddata[4][8] << "},{\"dealName\"=>" << @dealgriddata[5][0]<<", \"contractNumber\"=>" <<@dealgriddata[5][1] << ", \"inceptionDate\"=>"<< @dealgriddata[5][3] << ", \"submittedDate\"=>" << @dealgriddata[5][4]<<", \"status\"=>" << @dealgriddata[5][5] << ", \"dealNumber\"=>" << @dealgriddata[5][6] << ", \"primaryUnderwriterName\"=>" << @dealgriddata[5][7] << ", \"technicalAssistantName\"=>" << @dealgriddata[5][8] << "}"
    #@actualcompmodifieddata = "{\"dealName\"=>" + @dealgriddata[0][0].to_s + ", \"contractNumber\"=>" +@dealgriddata[0][1].to_s + ", \"inceptionDate\"=>" + @dealgriddata[0][3].to_s + ", \"submittedDate\"=>" + @dealgriddata[0][4].to_s + ", \"status\"=>" + @dealgriddata[0][5].to_s + ",\"dealNumber\"=>" + @dealgriddata[0][6].to_s + ", \"primaryUnderwriterName\"=>" + @dealgriddata[0][7].to_s + ", \"technicalAssistantName\"=>" + @dealgriddata[0][8].to_s + "},    {\"dealName\"=>" + @dealgriddata[1][0].to_s + ", \"contractNumber\"=>" +@dealgriddata[1][1].to_s + ", \"inceptionDate\"=>" + @dealgriddata[1][3].to_s + ", \"submittedDate\"=>" + @dealgriddata[1][4].to_s + ", \"status\"=>" + @dealgriddata[1][5].to_s + ",\"dealNumber\"=>" + @dealgriddata[1][6].to_s + ", \"primaryUnderwriterName\"=>" + @dealgriddata[1][7].to_s + ", \"technicalAssistantName\"=>" + @dealgriddata[1][8].to_s + "},{\"dealName\"=>" + @dealgriddata[2][0].to_s + ", \"contractNumber\"=>" +@dealgriddata[2][1].to_s + ", \"inceptionDate\"=>" + @dealgriddata[2][3].to_s + ", \"submittedDate\"=>" + @dealgriddata[2][4].to_s + ", \"status\"=>" + @dealgriddata[2][5].to_s + ", \"dealNumber\"=>" + @dealgriddata[2][6].to_s + ", \"primaryUnderwriterName\"=>" + @dealgriddata[2][7].to_s + ", \"technicalAssistantName\"=>" + @dealgriddata[2][8].to_s + "}, {\"dealName\"=>" + @dealgriddata[3][0].to_s + ", \"contractNumber\"=>" +@dealgriddata[3][1].to_s + ", \"inceptionDate\"=>" + @dealgriddata[3][3].to_s + ", \"submittedDate\"=>" + @dealgriddata[3][4].to_s + ", \"status\"=>" + @dealgriddata[3][5].to_s + ",\"dealNumber\"=>" + @dealgriddata[3][6].to_s + ", \"primaryUnderwriterName\"=>" + @dealgriddata[3][7].to_s + ", \"technicalAssistantName\"=>" + @dealgriddata[3][8].to_s + "}, {\"dealName\"=>" + @dealgriddata[4][0].to_s + ", \"contractNumber\"=>" +@dealgriddata[4][1].to_s + ", \"inceptionDate\"=>" + @dealgriddata[4][3].to_s + ", \"submittedDate\"=>" + @dealgriddata[4][4].to_s + ", \"status\"=>" + @dealgriddata[4][5].to_s + ",\"dealNumber\"=>" + @dealgriddata[4][6].to_s + ", \"primaryUnderwriterName\"=>" + @dealgriddata[4][7].to_s + ", \"technicalAssistantName\"=>" + @dealgriddata[4][8].to_s + "},{\"dealName\"=>" + @dealgriddata[5][0].to_s + ", \"contractNumber\"=>" +@dealgriddata[5][1].to_s + ", \"inceptionDate\"=>"+ @dealgriddata[5][3].to_s + ", \"submittedDate\"=>" + @dealgriddata[5][4].to_s + ", \"status\"=>" + @dealgriddata[5][5].to_s + ", \"dealNumber\"=>" + @dealgriddata[5][6].to_s + ", \"primaryUnderwriterName\"=>" + @dealgriddata[5][7].to_s + ", \"technicalAssistantName\"=>" + @dealgriddata[5][8].to_s + "}"
    #@actualcompmodifieddata = "[{\"dealName\"=>" + "\"" + @dealgriddata[0][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[0][1].to_s + "\""   + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[0][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[0][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[0][4].to_s + "\""   + ",\"dealNumber\"=>" + "\"" + @dealgriddata[0][5].to_s + "\""   + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[0][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[0][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[1][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[1][1].to_s + "\""   + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[1][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[1][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[1][4].to_s + "\""   + ",\"dealNumber\"=>" + "\"" + @dealgriddata[1][5].to_s + "\""   + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[1][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[1][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[2][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[2][1].to_s + "\""   + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[2][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[2][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[2][4].to_s + "\""   + ", \"dealNumber\"=>" + "\"" + @dealgriddata[2][5].to_s + "\""   + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[2][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[2][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[3][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[3][1].to_s + "\""   + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[3][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[3][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[3][4].to_s + "\""   + ",\"dealNumber\"=>" + "\"" + @dealgriddata[3][5].to_s + "\""   + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[3][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[3][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[4][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[4][1].to_s + "\""   + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[4][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[4][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[4][4].to_s + "\""   + ",\"dealNumber\"=>" + "\"" + @dealgriddata[4][5].to_s + "\""   + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[4][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[4][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[5][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[5][1].to_s + "\""   + ", \"inceptionDate\"=>"+ "\"" + @dealgriddata[5][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[5][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[5][4].to_s + "\""   + ", \"dealNumber\"=>" + "\"" + @dealgriddata[5][5].to_s + "\""   + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[5][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[5][7].to_s + "\""   + "}]"
    #@actualcompmodifieddata = "[{\"dealName\"=>" + "\"" + @dealgriddata[0][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[0][1].to_s + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[0][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[0][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[0][4].to_s + "\""   + ",\"dealNumber\"=>" + "\"" + @dealgriddata[0][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[0][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[0][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[1][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[1][1].to_s + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[1][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[1][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[1][4].to_s + "\""   + ",\"dealNumber\"=>" + "\"" + @dealgriddata[1][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[1][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[1][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[2][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[2][1].to_s + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[2][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[2][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[2][4].to_s + "\""   + ", \"dealNumber\"=>" + "\"" + @dealgriddata[2][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[2][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[2][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[3][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[3][1].to_s + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[3][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[3][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[3][4].to_s + "\""   + ",\"dealNumber\"=>" + "\"" + @dealgriddata[3][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[3][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[3][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[4][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[4][1].to_s + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[4][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[4][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[4][4].to_s + "\""   + ",\"dealNumber\"=>" + "\"" + @dealgriddata[4][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[4][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[4][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[5][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[5][1].to_s + ", \"inceptionDate\"=>"+ "\"" + @dealgriddata[5][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[5][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[5][4].to_s + "\""   + ", \"dealNumber\"=>" + "\"" + @dealgriddata[5][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[5][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[5][7].to_s + "\""   + "}]"
    #@actualcompmodifieddata = "[{\"dealName\"=>" + "\"" + @dealgriddata[0][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[0][1].to_s + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[0][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[0][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[0][4].to_s + "\""   + ", \"dealNumber\"=>" + @dealgriddata[0][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[0][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[0][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[1][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[1][1].to_s + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[1][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[1][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[1][4].to_s + "\""   + ", \"dealNumber\"=>" + @dealgriddata[1][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[1][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[1][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[2][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[2][1].to_s + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[2][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[2][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[2][4].to_s + "\""   + ", \"dealNumber\"=>" + @dealgriddata[2][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[2][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[2][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[3][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[3][1].to_s + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[3][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[3][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[3][4].to_s + "\""   + ", \"dealNumber\"=>" + @dealgriddata[3][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[3][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[3][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[4][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[4][1].to_s + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[4][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[4][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[4][4].to_s + "\""   + ", \"dealNumber\"=>" + @dealgriddata[4][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[4][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[4][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[5][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[5][1].to_s + ", \"inceptionDate\"=>"+ "\"" + @dealgriddata[5][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[5][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[5][4].to_s + "\""   + ", \"dealNumber\"=>" + @dealgriddata[5][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[5][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[5][7].to_s + "\""   + "}]"
    #@moddealgriddata = Array.new
    #@moddealgriddata = "{"dealNumber
    #return @dealgriddata
    #print "\n"
    #print @actualcompmodifieddata
    #print "\n"
    #
    #for i in 0..12
    #  for j in 0..5
    #    print "\n"
    #    print @dealgriddata[[i,j]]
    #    print "\n"
    #    #if @dealgriddata[[i,j]] == ""
    #      @dealgriddata[[i,j]] = nil
    #    end
    #  end
    #end

    #    print "\n"
    #    print cell
    #    print "\n"

        #    print "Value at [" + @rowcount.to_s + ","+ @colcount.to_s + "]: "+ cell.to_s+ "\n"#[j]
    #    @colcount = @colcount + 1
    #  end
    #  @rowcount = @rowcount + 1
    #  @colcount = 0
    #end
    #@dealgriddata.each do |row|
    #  row.each do |cell|
    #    print cell
    #    print "Value at [" + @rowcount.to_s + ","+ @colcount.to_s + "]: "+ cell.to_s+ "\n"#[j]
    #    @colcount = @colcount + 1
    #  end
    #  @rowcount = @rowcount + 1
    #  @colcount = 0
    #end
    # print "\n"
    # print @dealgriddata
    # print "\n"


    @actualcompareddata = Array.new
    @colcount=0
    @rowcount = 9
    for cellcount in 1..6
      @actualcompareddata[cellcount] = {"dealName"=>@dealgriddata[[@rowcount,@colcount]], "contractNumber"=>@dealgriddata[[@rowcount+1,@colcount]].to_i, "inceptionDate"=>@dealgriddata[[@rowcount+2,@colcount]], "targetDate"=>@dealgriddata[[@rowcount+3,@colcount]], "priority"=>@dealgriddata[[@rowcount+4,@colcount]], "submittedDate"=>@dealgriddata[[@rowcount+5,@colcount]], "status"=>@dealgriddata[[@rowcount+6,@colcount]], "dealNumber"=>@dealgriddata[[@rowcount+7,@colcount]].to_i, "primaryUnderwriterName"=>@dealgriddata[[@rowcount+8,@colcount]]}

      #@actualcompareddata[cellcount] = {"dealName" => @dealgriddata[@colcount].to_s, "contractNumber" => @dealgriddata[@colcount+1].to_i, "inceptionDate" => @dealgriddata[@colcount+2].to_s, "targetDate" => @dealgriddata[@colcount+3].to_s, "priority" => @dealgriddata[@colcount+4].to_s, "submittedDate" => @dealgriddata[@colcount+5].to_s, "status" => @dealgriddata[@colcount+6].to_s, "dealNumber" => @dealgriddata[@colcount+7].to_i, "primaryUnderwriterName" => @dealgriddata[@colcount+8].to_s}
      #@actualcompareddata[cellcount] = {"dealName" => @dealgriddata[[@rowcount,@colcount]], "contractNumber" => @dealgriddata[[@rowcount+1,@colcount]].to_i, "inceptionDate" => @dealgriddata[[@rowcount+2,@colcount]], "targetDate" => @dealgriddata[[@rowcount+3,@colcount]], "priority" => @dealgriddata[[@rowcount+4,@colcount]], "submittedDate" => @dealgriddata[[@rowcount+5,@colcount]], "status" => @dealgriddata[[@rowcount+6,@colcount]], "dealNumber" => @dealgriddata[[@rowcount+7,@colcount]].to_i, "primaryUnderwriterName" => @dealgriddata[[@rowcount+8,@colcount]]}#, "secondaryUnderwriterName" => @dealgriddata[[@rowcount+9,@colcount]], "technicalAssistantName" => @dealgriddata[[@rowcount+10,@colcount]], "modellerName" => @dealgriddata[[@rowcount+11,@colcount]], "actuaryName" => @dealgriddata[[@rowcount+12,@colcount]]}
      #""=>0, ""=>"2018-01-01", ""=>"2017-12-18", ""=>"On Hold", ""=>1367665, ""=>"Kirby Montgomery", ""=>"Kate Trent"}
      @rowcount = @rowcount + 9
    end

    @actualcompareddata.delete(nil)

    #print "\n"
    #print @actualcompmodifieddata
    #print "\n"
    #print @actualcompareddata#["null"]
    #print "\n"


    return @actualcompareddata
  end
  def ModifyGridCellData(griddata)
    @dealgriddata = griddata
    #@actualcompmodifieddata = Array.new
    #@actualcompmodifieddata = "{\"dealName\"=>" + @dealgriddata[0][0]+", \"contractNumber\"=>" +@dealgriddata[0][1] + ", \"inceptionDate\"=>" + @dealgriddata[0][3] + ", \"submittedDate\"=>" + @dealgriddata[0][4]+", \"status\"=>" + @dealgriddata[0][5] + ",\"dealNumber\"=>" + @dealgriddata[0][6] + ", \"primaryUnderwriterName\"=>" + @dealgriddata[0][7] + ", \"technicalAssistantName\"=>" + @dealgriddata[0][8] + "},    {\"dealName\"=>" + @dealgriddata[1][0]+", \"contractNumber\"=>" +@dealgriddata[1][1] + ", \"inceptionDate\"=>" + @dealgriddata[1][3] + ", \"submittedDate\"=>" + @dealgriddata[1][4]+", \"status\"=>" + @dealgriddata[1][5] + ",\"dealNumber\"=>" + @dealgriddata[1][6] + ", \"primaryUnderwriterName\"=>" + @dealgriddata[1][7] + ", \"technicalAssistantName\"=>" + @dealgriddata[1][8] + "},{\"dealName\"=>" + @dealgriddata[2][0]+", \"contractNumber\"=>" +@dealgriddata[2][1] + ", \"inceptionDate\"=>" + @dealgriddata[2][3] + ", \"submittedDate\"=>" + @dealgriddata[2][4]+", \"status\"=>" + @dealgriddata[2][5] + ", \"dealNumber\"=>" + @dealgriddata[2][6] + ", \"primaryUnderwriterName\"=>" + @dealgriddata[2][7] + ", \"technicalAssistantName\"=>" + @dealgriddata[2][8] + "}, {\"dealName\"=>" + @dealgriddata[3][0]+", \"contractNumber\"=>" +@dealgriddata[3][1] + ", \"inceptionDate\"=>" + @dealgriddata[3][3] + ", \"submittedDate\"=>" + @dealgriddata[3][4]+", \"status\"=>" + @dealgriddata[3][5] + ",\"dealNumber\"=>" + @dealgriddata[3][6] + ", \"primaryUnderwriterName\"=>" + @dealgriddata[3][7] + ", \"technicalAssistantName\"=>" + @dealgriddata[3][8] + "}, {\"dealName\"=>" + @dealgriddata[4][0]+", \"contractNumber\"=>" +@dealgriddata[4][1] + ", \"inceptionDate\"=>" + @dealgriddata[4][3] + ", \"submittedDate\"=>" + @dealgriddata[4][4]+", \"status\"=>" + @dealgriddata[4][5] + ",\"dealNumber\"=>" + @dealgriddata[4][6] + ", \"primaryUnderwriterName\"=>" + @dealgriddata[4][7] + ", \"technicalAssistantName\"=>" + @dealgriddata[4][8] + "},{\"dealName\"=>" + @dealgriddata[5][0]+", \"contractNumber\"=>" +@dealgriddata[5][1] + ", \"inceptionDate\"=>"+ @dealgriddata[5][3] + ", \"submittedDate\"=>" + @dealgriddata[5][4]+", \"status\"=>" + @dealgriddata[5][5] + ", \"dealNumber\"=>" + @dealgriddata[5][6] + ", \"primaryUnderwriterName\"=>" + @dealgriddata[5][7] + ", \"technicalAssistantName\"=>" + @dealgriddata[5][8] + "}"
    #@actualcompmodifieddata = "{\"dealName\"=>" << @dealgriddata[0][0]<<", \"contractNumber\"=>" <<@dealgriddata[0][1] << ", \"inceptionDate\"=>" << @dealgriddata[0][3] << ", \"submittedDate\"=>" << @dealgriddata[0][4]<<", \"status\"=>" << @dealgriddata[0][5] << ",\"dealNumber\"=>" << @dealgriddata[0][6] << ", \"primaryUnderwriterName\"=>" << @dealgriddata[0][7] << ", \"technicalAssistantName\"=>" << @dealgriddata[0][8] << "},    {\"dealName\"=>" << @dealgriddata[1][0]<<", \"contractNumber\"=>" <<@dealgriddata[1][1] << ", \"inceptionDate\"=>" << @dealgriddata[1][3] << ", \"submittedDate\"=>" << @dealgriddata[1][4]<<", \"status\"=>" << @dealgriddata[1][5] << ",\"dealNumber\"=>" << @dealgriddata[1][6] << ", \"primaryUnderwriterName\"=>" << @dealgriddata[1][7] << ", \"technicalAssistantName\"=>" << @dealgriddata[1][8] << "},{\"dealName\"=>" << @dealgriddata[2][0]<<", \"contractNumber\"=>" <<@dealgriddata[2][1] << ", \"inceptionDate\"=>" << @dealgriddata[2][3] << ", \"submittedDate\"=>" << @dealgriddata[2][4]<<", \"status\"=>" << @dealgriddata[2][5] << ", \"dealNumber\"=>" << @dealgriddata[2][6] << ", \"primaryUnderwriterName\"=>" << @dealgriddata[2][7] << ", \"technicalAssistantName\"=>" << @dealgriddata[2][8] << "}, {\"dealName\"=>" << @dealgriddata[3][0]<<", \"contractNumber\"=>" <<@dealgriddata[3][1] << ", \"inceptionDate\"=>" << @dealgriddata[3][3] << ", \"submittedDate\"=>" << @dealgriddata[3][4]<<", \"status\"=>" << @dealgriddata[3][5] << ",\"dealNumber\"=>" << @dealgriddata[3][6] << ", \"primaryUnderwriterName\"=>" << @dealgriddata[3][7] << ", \"technicalAssistantName\"=>" << @dealgriddata[3][8] << "}, {\"dealName\"=>" << @dealgriddata[4][0]<<", \"contractNumber\"=>" <<@dealgriddata[4][1] << ", \"inceptionDate\"=>" << @dealgriddata[4][3] << ", \"submittedDate\"=>" << @dealgriddata[4][4]<<", \"status\"=>" << @dealgriddata[4][5] << ",\"dealNumber\"=>" << @dealgriddata[4][6] << ", \"primaryUnderwriterName\"=>" << @dealgriddata[4][7] << ", \"technicalAssistantName\"=>" << @dealgriddata[4][8] << "},{\"dealName\"=>" << @dealgriddata[5][0]<<", \"contractNumber\"=>" <<@dealgriddata[5][1] << ", \"inceptionDate\"=>"<< @dealgriddata[5][3] << ", \"submittedDate\"=>" << @dealgriddata[5][4]<<", \"status\"=>" << @dealgriddata[5][5] << ", \"dealNumber\"=>" << @dealgriddata[5][6] << ", \"primaryUnderwriterName\"=>" << @dealgriddata[5][7] << ", \"technicalAssistantName\"=>" << @dealgriddata[5][8] << "}"
    #@actualcompmodifieddata = "{\"dealName\"=>" + @dealgriddata[0][0].to_s + ", \"contractNumber\"=>" +@dealgriddata[0][1].to_s + ", \"inceptionDate\"=>" + @dealgriddata[0][3].to_s + ", \"submittedDate\"=>" + @dealgriddata[0][4].to_s + ", \"status\"=>" + @dealgriddata[0][5].to_s + ",\"dealNumber\"=>" + @dealgriddata[0][6].to_s + ", \"primaryUnderwriterName\"=>" + @dealgriddata[0][7].to_s + ", \"technicalAssistantName\"=>" + @dealgriddata[0][8].to_s + "},    {\"dealName\"=>" + @dealgriddata[1][0].to_s + ", \"contractNumber\"=>" +@dealgriddata[1][1].to_s + ", \"inceptionDate\"=>" + @dealgriddata[1][3].to_s + ", \"submittedDate\"=>" + @dealgriddata[1][4].to_s + ", \"status\"=>" + @dealgriddata[1][5].to_s + ",\"dealNumber\"=>" + @dealgriddata[1][6].to_s + ", \"primaryUnderwriterName\"=>" + @dealgriddata[1][7].to_s + ", \"technicalAssistantName\"=>" + @dealgriddata[1][8].to_s + "},{\"dealName\"=>" + @dealgriddata[2][0].to_s + ", \"contractNumber\"=>" +@dealgriddata[2][1].to_s + ", \"inceptionDate\"=>" + @dealgriddata[2][3].to_s + ", \"submittedDate\"=>" + @dealgriddata[2][4].to_s + ", \"status\"=>" + @dealgriddata[2][5].to_s + ", \"dealNumber\"=>" + @dealgriddata[2][6].to_s + ", \"primaryUnderwriterName\"=>" + @dealgriddata[2][7].to_s + ", \"technicalAssistantName\"=>" + @dealgriddata[2][8].to_s + "}, {\"dealName\"=>" + @dealgriddata[3][0].to_s + ", \"contractNumber\"=>" +@dealgriddata[3][1].to_s + ", \"inceptionDate\"=>" + @dealgriddata[3][3].to_s + ", \"submittedDate\"=>" + @dealgriddata[3][4].to_s + ", \"status\"=>" + @dealgriddata[3][5].to_s + ",\"dealNumber\"=>" + @dealgriddata[3][6].to_s + ", \"primaryUnderwriterName\"=>" + @dealgriddata[3][7].to_s + ", \"technicalAssistantName\"=>" + @dealgriddata[3][8].to_s + "}, {\"dealName\"=>" + @dealgriddata[4][0].to_s + ", \"contractNumber\"=>" +@dealgriddata[4][1].to_s + ", \"inceptionDate\"=>" + @dealgriddata[4][3].to_s + ", \"submittedDate\"=>" + @dealgriddata[4][4].to_s + ", \"status\"=>" + @dealgriddata[4][5].to_s + ",\"dealNumber\"=>" + @dealgriddata[4][6].to_s + ", \"primaryUnderwriterName\"=>" + @dealgriddata[4][7].to_s + ", \"technicalAssistantName\"=>" + @dealgriddata[4][8].to_s + "},{\"dealName\"=>" + @dealgriddata[5][0].to_s + ", \"contractNumber\"=>" +@dealgriddata[5][1].to_s + ", \"inceptionDate\"=>"+ @dealgriddata[5][3].to_s + ", \"submittedDate\"=>" + @dealgriddata[5][4].to_s + ", \"status\"=>" + @dealgriddata[5][5].to_s + ", \"dealNumber\"=>" + @dealgriddata[5][6].to_s + ", \"primaryUnderwriterName\"=>" + @dealgriddata[5][7].to_s + ", \"technicalAssistantName\"=>" + @dealgriddata[5][8].to_s + "}"
    #@actualcompmodifieddata = "[{\"dealName\"=>" + "\"" + @dealgriddata[0][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[0][1].to_s + "\""   + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[0][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[0][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[0][4].to_s + "\""   + ",\"dealNumber\"=>" + "\"" + @dealgriddata[0][5].to_s + "\""   + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[0][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[0][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[1][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[1][1].to_s + "\""   + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[1][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[1][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[1][4].to_s + "\""   + ",\"dealNumber\"=>" + "\"" + @dealgriddata[1][5].to_s + "\""   + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[1][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[1][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[2][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[2][1].to_s + "\""   + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[2][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[2][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[2][4].to_s + "\""   + ", \"dealNumber\"=>" + "\"" + @dealgriddata[2][5].to_s + "\""   + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[2][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[2][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[3][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[3][1].to_s + "\""   + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[3][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[3][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[3][4].to_s + "\""   + ",\"dealNumber\"=>" + "\"" + @dealgriddata[3][5].to_s + "\""   + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[3][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[3][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[4][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[4][1].to_s + "\""   + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[4][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[4][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[4][4].to_s + "\""   + ",\"dealNumber\"=>" + "\"" + @dealgriddata[4][5].to_s + "\""   + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[4][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[4][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[5][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[5][1].to_s + "\""   + ", \"inceptionDate\"=>"+ "\"" + @dealgriddata[5][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[5][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[5][4].to_s + "\""   + ", \"dealNumber\"=>" + "\"" + @dealgriddata[5][5].to_s + "\""   + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[5][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[5][7].to_s + "\""   + "}]"
    #@actualcompmodifieddata = "[{\"dealName\"=>" + "\"" + @dealgriddata[0][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[0][1].to_s + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[0][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[0][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[0][4].to_s + "\""   + ",\"dealNumber\"=>" + "\"" + @dealgriddata[0][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[0][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[0][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[1][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[1][1].to_s + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[1][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[1][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[1][4].to_s + "\""   + ",\"dealNumber\"=>" + "\"" + @dealgriddata[1][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[1][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[1][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[2][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[2][1].to_s + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[2][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[2][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[2][4].to_s + "\""   + ", \"dealNumber\"=>" + "\"" + @dealgriddata[2][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[2][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[2][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[3][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[3][1].to_s + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[3][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[3][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[3][4].to_s + "\""   + ",\"dealNumber\"=>" + "\"" + @dealgriddata[3][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[3][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[3][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[4][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[4][1].to_s + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[4][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[4][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[4][4].to_s + "\""   + ",\"dealNumber\"=>" + "\"" + @dealgriddata[4][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[4][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[4][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[5][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[5][1].to_s + ", \"inceptionDate\"=>"+ "\"" + @dealgriddata[5][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[5][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[5][4].to_s + "\""   + ", \"dealNumber\"=>" + "\"" + @dealgriddata[5][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[5][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[5][7].to_s + "\""   + "}]"
    #@actualcompmodifieddata = "[{\"dealName\"=>" + "\"" + @dealgriddata[0][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[0][1].to_s + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[0][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[0][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[0][4].to_s + "\""   + ", \"dealNumber\"=>" + @dealgriddata[0][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[0][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[0][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[1][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[1][1].to_s + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[1][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[1][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[1][4].to_s + "\""   + ", \"dealNumber\"=>" + @dealgriddata[1][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[1][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[1][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[2][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[2][1].to_s + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[2][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[2][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[2][4].to_s + "\""   + ", \"dealNumber\"=>" + @dealgriddata[2][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[2][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[2][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[3][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[3][1].to_s + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[3][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[3][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[3][4].to_s + "\""   + ", \"dealNumber\"=>" + @dealgriddata[3][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[3][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[3][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[4][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[4][1].to_s + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[4][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[4][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[4][4].to_s + "\""   + ", \"dealNumber\"=>" + @dealgriddata[4][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[4][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[4][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[5][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[5][1].to_s + ", \"inceptionDate\"=>"+ "\"" + @dealgriddata[5][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[5][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[5][4].to_s + "\""   + ", \"dealNumber\"=>" + @dealgriddata[5][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[5][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[5][7].to_s + "\""   + "}]"
    #@moddealgriddata = Array.new
    #@moddealgriddata = "{"dealNumber
    #return @dealgriddata
    #print "\n"
    #print @actualcompmodifieddata
    #print "\n"
    #
    #for i in 0..12
    #  for j in 0..5
    #    print "\n"
    #    print @dealgriddata[[i,j]]
    #    print "\n"
    #    #if @dealgriddata[[i,j]] == ""
    #      @dealgriddata[[i,j]] = nil
    #    end
    #  end
    #end

    #    print "\n"
    #    print cell
    #    print "\n"

    #    print "Value at [" + @rowcount.to_s + ","+ @colcount.to_s + "]: "+ cell.to_s+ "\n"#[j]
    #    @colcount = @colcount + 1
    #  end
    #  @rowcount = @rowcount + 1
    #  @colcount = 0
    #end
    #@dealgriddata.each do |row|
    #  row.each do |cell|
    #    print cell
    #    print "Value at [" + @rowcount.to_s + ","+ @colcount.to_s + "]: "+ cell.to_s+ "\n"#[j]
    #    @colcount = @colcount + 1
    #  end
    #  @rowcount = @rowcount + 1
    #  @colcount = 0
    #end
    #print "\n"
    #print @dealgriddata
    #print "\n"
    @possibleRows = 6

    # if @dealgriddata[[9,0]].to_s != "Underwriter 2"
    #   @actualcompareddata = Array.new
    #   @colcount=0
    #   @rowcount = 9
    #   if @dealgriddata[[@rowcount,@colcount]].to_s == "nil"
    #     @possibleRows=0
    #   elsif @dealgriddata[[@rowcount,@colcount+9]].to_s == "nil"
    #     @possibleRows=1
    #   elsif @dealgriddata[[@rowcount,@colcount+18]].to_s == "nil"
    #     @possibleRows=2
    #   elsif @dealgriddata[[@rowcount,@colcount+27]].to_s == "nil"
    #     @possibleRows=3
    #   elsif @dealgriddata[[@rowcount,@colcount+36]].to_s == "nil"
    #     @possibleRows=4
    #   elsif @dealgriddata[[@rowcount,@colcount+45]].to_s == "nil"
    #     @possibleRows=5
    #   else
    #     @possibleRows=6
    #   end
    #   if @possibleRows != 0
    #     for cellcount in 1..@possibleRows
    #       #@actualcompareddata[cellcount] = {"dealName"=>@dealgriddata[[@rowcount,@colcount]], "contractNumber"=>@dealgriddata[[@rowcount+1,@colcount]].to_i, "inceptionDate"=>@dealgriddata[[@rowcount+2,@colcount]], "targetDate"=>@dealgriddata[[@rowcount+3,@colcount]], "priority"=>@dealgriddata[[@rowcount+4,@colcount]], "submittedDate"=>@dealgriddata[[@rowcount+5,@colcount]], "status"=>@dealgriddata[[@rowcount+6,@colcount]], "dealNumber"=>@dealgriddata[[@rowcount+7,@colcount]].to_i, "primaryUnderwriterName"=>@dealgriddata[[@rowcount+8,@colcount]]}
    #
    #       #@actualcompareddata[cellcount] = {"dealName" => @dealgriddata[@colcount].to_s, "contractNumber" => @dealgriddata[@colcount+1].to_i, "inceptionDate" => @dealgriddata[@colcount+2].to_s, "targetDate" => @dealgriddata[@colcount+3].to_s, "priority" => @dealgriddata[@colcount+4].to_s, "submittedDate" => @dealgriddata[@colcount+5].to_s, "status" => @dealgriddata[@colcount+6].to_s, "dealNumber" => @dealgriddata[@colcount+7].to_i, "primaryUnderwriterName" => @dealgriddata[@colcount+8].to_s}
    #       @actualcompareddata[cellcount] = {"dealName" => @dealgriddata[[@rowcount,@colcount]], "contractNumber" => @dealgriddata[[@rowcount+1,@colcount]].to_i, "inceptionDate" => @dealgriddata[[@rowcount+2,@colcount]], "targetDate" => @dealgriddata[[@rowcount+3,@colcount]], "priority" => @dealgriddata[[@rowcount+4,@colcount]], "submittedDate" => @dealgriddata[[@rowcount+5,@colcount]], "status" => @dealgriddata[[@rowcount+6,@colcount]], "dealNumber" => @dealgriddata[[@rowcount+7,@colcount]].to_i, "primaryUnderwriterName" => @dealgriddata[[@rowcount+8,@colcount]]}#, "secondaryUnderwriterName" => @dealgriddata[[@rowcount+9,@colcount]], "technicalAssistantName" => @dealgriddata[[@rowcount+10,@colcount]], "modellerName" => @dealgriddata[[@rowcount+11,@colcount]], "actuaryName" => @dealgriddata[[@rowcount+12,@colcount]]}
    #       #""=>0, ""=>"2018-01-01", ""=>"2017-12-18", ""=>"On Hold", ""=>1367665, ""=>"Kirby Montgomery", ""=>"Kate Trent"}
    #       @rowcount = @rowcount + 9
    #     end
    #   end
    # end
    # if @dealgriddata[[0,12]].to_s == "Actuary"
    #   @actualcompareddata = Array.new
    #   @colcount=0
    #   @rowcount = 13
    #   if @dealgriddata[[@rowcount,@colcount]].to_s == "nil"
    #     @possibleRows=0
    #   elsif @dealgriddata[[@rowcount,@colcount+13]].to_s == "nil"
    #     @possibleRows=1
    #   elsif @dealgriddata[[@rowcount,@colcount+26]].to_s == "nil"
    #     @possibleRows=2
    #   elsif @dealgriddata[[@rowcount,@colcount+39]].to_s == "nil"
    #     @possibleRows=3
    #   elsif @dealgriddata[[@rowcount,@colcount+52]].to_s == "nil"
    #     @possibleRows=4
    #   elsif @dealgriddata[[@rowcount,@colcount+65]].to_s == "nil"
    #     @possibleRows=5
    #   else
    #     @possibleRows=6
    #   end
    #   if @possibleRows != 0
    #     for cellcount in 1..@possibleRows
    #       #@actualcompareddata[cellcount] = {"dealName"=>@dealgriddata[[@rowcount,@colcount]], "contractNumber"=>@dealgriddata[[@rowcount+1,@colcount]].to_i, "inceptionDate"=>@dealgriddata[[@rowcount+2,@colcount]], "targetDate"=>@dealgriddata[[@rowcount+3,@colcount]], "priority"=>@dealgriddata[[@rowcount+4,@colcount]], "submittedDate"=>@dealgriddata[[@rowcount+5,@colcount]], "status"=>@dealgriddata[[@rowcount+6,@colcount]], "dealNumber"=>@dealgriddata[[@rowcount+7,@colcount]].to_i, "primaryUnderwriterName"=>@dealgriddata[[@rowcount+8,@colcount]]}
    #
    #       #@actualcompareddata[cellcount] = {"dealName" => @dealgriddata[@colcount].to_s, "contractNumber" => @dealgriddata[@colcount+1].to_i, "inceptionDate" => @dealgriddata[@colcount+2].to_s, "targetDate" => @dealgriddata[@colcount+3].to_s, "priority" => @dealgriddata[@colcount+4].to_s, "submittedDate" => @dealgriddata[@colcount+5].to_s, "status" => @dealgriddata[@colcount+6].to_s, "dealNumber" => @dealgriddata[@colcount+7].to_i, "primaryUnderwriterName" => @dealgriddata[@colcount+8].to_s}
    #       @actualcompareddata[cellcount] = {"dealName" => @dealgriddata[[@rowcount,@colcount]], "contractNumber" => @dealgriddata[[@rowcount+1,@colcount]].to_i, "inceptionDate" => @dealgriddata[[@rowcount+2,@colcount]], "targetDate" => @dealgriddata[[@rowcount+3,@colcount]], "priority" => @dealgriddata[[@rowcount+4,@colcount]], "submittedDate" => @dealgriddata[[@rowcount+5,@colcount]], "status" => @dealgriddata[[@rowcount+6,@colcount]], "dealNumber" => @dealgriddata[[@rowcount+7,@colcount]].to_i, "primaryUnderwriterName" => @dealgriddata[[@rowcount+8,@colcount]]}#, "secondaryUnderwriterName" => @dealgriddata[[@rowcount+9,@colcount]], "technicalAssistantName" => @dealgriddata[[@rowcount+10,@colcount]], "modellerName" => @dealgriddata[[@rowcount+11,@colcount]], "actuaryName" => @dealgriddata[[@rowcount+12,@colcount]]}
    #       #""=>0, ""=>"2018-01-01", ""=>"2017-12-18", ""=>"On Hold", ""=>1367665, ""=>"Kirby Montgomery", ""=>"Kate Trent"}
    #       @rowcount = @rowcount + 13
    #     end
    #   end
    # end
    # if @dealgriddata[[12,0]].to_s == "Actuary" && (@dealgriddata[[2,1]].to_s == "nil" || @dealgriddata[[2,1]].to_s == "")
    #   @actualcompareddata = Array.new
    #   # @colcount=1
    #   # @rowcount = 1
    #   # if @dealgriddata[[@rowcount,@colcount]].to_s == "nil"
    #   #   @possibleRows=0
    #   # elsif @dealgriddata[[@rowcount+13,@colcount]].to_s == "nil"
    #   #   @possibleRows=1
    #   # elsif @dealgriddata[[@rowcount+26,@colcount]].to_s == "nil"
    #   #   @possibleRows=2
    #   # elsif @dealgriddata[[@rowcount+39,@colcount]].to_s == "nil"
    #   #   @possibleRows=3
    #   # elsif @dealgriddata[[@rowcount+52,@colcount]].to_s == "nil"
    #   #   @possibleRows=4
    #   # elsif @dealgriddata[[@rowcount+65,@colcount]].to_s == "nil"
    #   #   @possibleRows=5
    #   # else
    #   #   @possibleRows=6
    #   # end
    #   @colcount=1
    #   @rowcount = 1
    #   if @dealgriddata[[@rowcount+78,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount+78,@colcount]].to_s == ""
    #     @possibleRows=6
    #   end
    #   if @dealgriddata[[@rowcount+65,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount+65,@colcount]].to_s == ""
    #     @possibleRows=5
    #   end
    #   if @dealgriddata[[@rowcount+52,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount+52,@colcount]].to_s == ""
    #     @possibleRows=4
    #   end
    #   if @dealgriddata[[@rowcount+39,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount+39,@colcount]].to_s == ""
    #     @possibleRows=3
    #   end
    #   if @dealgriddata[[@rowcount+26,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount+26,@colcount]].to_s == ""
    #     @possibleRows=2
    #   end
    #   if @dealgriddata[[@rowcount+13,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount+13,@colcount]].to_s == ""
    #     @possibleRows=1
    #   end
    #   if @dealgriddata[[@rowcount,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount,@colcount]].to_s == ""
    #     @possibleRows=0
    #   end
    #   if @possibleRows != 0
    #     for cellcount in 1..@possibleRows
    #       #@actualcompareddata[cellcount] = {"dealName"=>@dealgriddata[[@rowcount,@colcount]], "contractNumber"=>@dealgriddata[[@rowcount+1,@colcount]].to_i, "inceptionDate"=>@dealgriddata[[@rowcount+2,@colcount]], "targetDate"=>@dealgriddata[[@rowcount+3,@colcount]], "priority"=>@dealgriddata[[@rowcount+4,@colcount]], "submittedDate"=>@dealgriddata[[@rowcount+5,@colcount]], "status"=>@dealgriddata[[@rowcount+6,@colcount]], "dealNumber"=>@dealgriddata[[@rowcount+7,@colcount]].to_i, "primaryUnderwriterName"=>@dealgriddata[[@rowcount+8,@colcount]]}
    #
    #       #@actualcompareddata[cellcount] = {"dealName" => @dealgriddata[@colcount].to_s, "contractNumber" => @dealgriddata[@colcount+1].to_i, "inceptionDate" => @dealgriddata[@colcount+2].to_s, "targetDate" => @dealgriddata[@colcount+3].to_s, "priority" => @dealgriddata[@colcount+4].to_s, "submittedDate" => @dealgriddata[@colcount+5].to_s, "status" => @dealgriddata[@colcount+6].to_s, "dealNumber" => @dealgriddata[@colcount+7].to_i, "primaryUnderwriterName" => @dealgriddata[@colcount+8].to_s}
    #       @actualcompareddata[cellcount] = {"dealName" => @dealgriddata[[@rowcount,@colcount]], "contractNumber" => @dealgriddata[[@rowcount+1,@colcount]].to_i, "inceptionDate" => @dealgriddata[[@rowcount+2,@colcount]], "targetDate" => @dealgriddata[[@rowcount+3,@colcount]], "priority" => @dealgriddata[[@rowcount+4,@colcount]], "submittedDate" => @dealgriddata[[@rowcount+5,@colcount]], "status" => @dealgriddata[[@rowcount+6,@colcount]], "dealNumber" => @dealgriddata[[@rowcount+7,@colcount]].to_i, "primaryUnderwriterName" => @dealgriddata[[@rowcount+8,@colcount]]}#, "secondaryUnderwriterName" => @dealgriddata[[@rowcount+9,@colcount]], "technicalAssistantName" => @dealgriddata[[@rowcount+10,@colcount]], "modellerName" => @dealgriddata[[@rowcount+11,@colcount]], "actuaryName" => @dealgriddata[[@rowcount+12,@colcount]]}
    #       #""=>0, ""=>"2018-01-01", ""=>"2017-12-18", ""=>"On Hold", ""=>1367665, ""=>"Kirby Montgomery", ""=>"Kate Trent"}
    #       @rowcount = @rowcount + 13
    #     end
    #   end
    # end
    # if @dealgriddata[[12,0]].to_s == "Actuary" && (@dealgriddata[[3,3]].to_s != "nil" || @dealgriddata[[3,3]].to_s != "")
    #   @actualcompareddata = Array.new
    #   # @colcount=1
    #   # @rowcount = 1
    #   # if @dealgriddata[[@rowcount,@colcount]].to_s == "nil"
    #   #   @possibleRows=0
    #   # elsif @dealgriddata[[@rowcount+13,@colcount]].to_s == "nil"
    #   #   @possibleRows=1
    #   # elsif @dealgriddata[[@rowcount+26,@colcount]].to_s == "nil"
    #   #   @possibleRows=2
    #   # elsif @dealgriddata[[@rowcount+39,@colcount]].to_s == "nil"
    #   #   @possibleRows=3
    #   # elsif @dealgriddata[[@rowcount+52,@colcount]].to_s == "nil"
    #   #   @possibleRows=4
    #   # elsif @dealgriddata[[@rowcount+65,@colcount]].to_s == "nil"
    #   #   @possibleRows=5
    #   # else
    #   #   @possibleRows=6
    #   # end
    #   @colcount=1
    #   @rowcount = 1
    #   if @dealgriddata[[@rowcount+6,@colcount+6]].to_s == "nil" || @dealgriddata[[@rowcount+6,@colcount+5]].to_s == ""
    #     @possibleRows=6
    #   end
    #   if @dealgriddata[[@rowcount+5,@colcount+5]].to_s == "nil" || @dealgriddata[[@rowcount+5,@colcount+5]].to_s == ""
    #     @possibleRows=5
    #   end
    #   if @dealgriddata[[@rowcount+4,@colcount+4]].to_s == "nil" || @dealgriddata[[@rowcount+4,@colcount+4]].to_s == ""
    #     @possibleRows=4
    #   end
    #   if @dealgriddata[[@rowcount+3,@colcount+3]].to_s == "nil" || @dealgriddata[[@rowcount+3,@colcount+3]].to_s == ""
    #     @possibleRows=3
    #   end
    #   if @dealgriddata[[@rowcount+2,@colcount+2]].to_s == "nil" || @dealgriddata[[@rowcount+2,@colcount+2]].to_s == ""
    #     @possibleRows=2
    #   end
    #   if @dealgriddata[[@rowcount+1,@colcount+1]].to_s == "nil" || @dealgriddata[[@rowcount+1,@colcount+1]].to_s == ""
    #     @possibleRows=1
    #   end
    #   if @dealgriddata[[@rowcount,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount,@colcount]].to_s == ""
    #     @possibleRows=0
    #   end
    #   if @possibleRows != 0
    #     for cellcount in 1..@possibleRows
    #       #@actualcompareddata[cellcount] = {"dealName"=>@dealgriddata[[@rowcount,@colcount]], "contractNumber"=>@dealgriddata[[@rowcount+1,@colcount]].to_i, "inceptionDate"=>@dealgriddata[[@rowcount+2,@colcount]], "targetDate"=>@dealgriddata[[@rowcount+3,@colcount]], "priority"=>@dealgriddata[[@rowcount+4,@colcount]], "submittedDate"=>@dealgriddata[[@rowcount+5,@colcount]], "status"=>@dealgriddata[[@rowcount+6,@colcount]], "dealNumber"=>@dealgriddata[[@rowcount+7,@colcount]].to_i, "primaryUnderwriterName"=>@dealgriddata[[@rowcount+8,@colcount]]}
    #
    #       #@actualcompareddata[cellcount] = {"dealName" => @dealgriddata[@colcount].to_s, "contractNumber" => @dealgriddata[@colcount+1].to_i, "inceptionDate" => @dealgriddata[@colcount+2].to_s, "targetDate" => @dealgriddata[@colcount+3].to_s, "priority" => @dealgriddata[@colcount+4].to_s, "submittedDate" => @dealgriddata[@colcount+5].to_s, "status" => @dealgriddata[@colcount+6].to_s, "dealNumber" => @dealgriddata[@colcount+7].to_i, "primaryUnderwriterName" => @dealgriddata[@colcount+8].to_s}
    #       @actualcompareddata[cellcount] = {"dealName" => @dealgriddata[[@rowcount,@colcount]], "contractNumber" => @dealgriddata[[@rowcount+1,@colcount]].to_i, "inceptionDate" => @dealgriddata[[@rowcount+2,@colcount]], "targetDate" => @dealgriddata[[@rowcount+3,@colcount]], "priority" => @dealgriddata[[@rowcount+4,@colcount]], "submittedDate" => @dealgriddata[[@rowcount+5,@colcount]], "status" => @dealgriddata[[@rowcount+6,@colcount]], "dealNumber" => @dealgriddata[[@rowcount+7,@colcount]].to_i, "primaryUnderwriterName" => @dealgriddata[[@rowcount+8,@colcount]]}#, "secondaryUnderwriterName" => @dealgriddata[[@rowcount+9,@colcount]], "technicalAssistantName" => @dealgriddata[[@rowcount+10,@colcount]], "modellerName" => @dealgriddata[[@rowcount+11,@colcount]], "actuaryName" => @dealgriddata[[@rowcount+12,@colcount]]}
    #       #""=>0, ""=>"2018-01-01", ""=>"2017-12-18", ""=>"On Hold", ""=>1367665, ""=>"Kirby Montgomery", ""=>"Kate Trent"}
    #       @colcount = @colcount + 1
    #       @rowcount = @colcount
    #     end
    #   end

    #end
    if @dealgriddata[[0,12]].to_s == "Actuary" #&& (@dealgriddata[[1,0]].to_s != "nil" || @dealgriddata[[1,0]].to_s != "")
      @actualcompareddata = Array.new
      # @colcount=1
      # @rowcount = 1
      # if @dealgriddata[[@rowcount,@colcount]].to_s == "nil"
      #   @possibleRows=0
      # elsif @dealgriddata[[@rowcount+13,@colcount]].to_s == "nil"
      #   @possibleRows=1
      # elsif @dealgriddata[[@rowcount+26,@colcount]].to_s == "nil"
      #   @possibleRows=2
      # elsif @dealgriddata[[@rowcount+39,@colcount]].to_s == "nil"
      #   @possibleRows=3
      # elsif @dealgriddata[[@rowcount+52,@colcount]].to_s == "nil"
      #   @possibleRows=4
      # elsif @dealgriddata[[@rowcount+65,@colcount]].to_s == "nil"
      #   @possibleRows=5
      # else
      #   @possibleRows=6
      # end
      @colcount = 0
      @rowcount = 1
      if @dealgriddata[[@rowcount+6,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount+6,@colcount]].to_s == ""
        @possibleRows=6
      end
      if @dealgriddata[[@rowcount+5,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount+5,@colcount]].to_s == ""
        @possibleRows=5
      end
      if @dealgriddata[[@rowcount+4,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount+4,@colcount]].to_s == ""
        @possibleRows=4
      end
      if @dealgriddata[[@rowcount+3,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount+3,@colcount]].to_s == ""
        @possibleRows=3
      end
      if @dealgriddata[[@rowcount+2,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount+2,@colcount]].to_s == ""
        @possibleRows=2
      end
      if @dealgriddata[[@rowcount+1,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount+1,@colcount]].to_s == ""
        @possibleRows=1
      end
      if @dealgriddata[[@rowcount,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount,@colcount]].to_s == ""
        @possibleRows=0
      end
      print "Grid is displayed with " + @possibleRows.to_s + " deals.\n"
      if @possibleRows != 0
        for cellcount in 0..@possibleRows-1
          #@actualcompareddata[cellcount] = {"dealName"=>@dealgriddata[[@rowcount,@colcount]], "contractNumber"=>@dealgriddata[[@rowcount+1,@colcount]].to_i, "inceptionDate"=>@dealgriddata[[@rowcount+2,@colcount]], "targetDate"=>@dealgriddata[[@rowcount+3,@colcount]], "priority"=>@dealgriddata[[@rowcount+4,@colcount]], "submittedDate"=>@dealgriddata[[@rowcount+5,@colcount]], "status"=>@dealgriddata[[@rowcount+6,@colcount]], "dealNumber"=>@dealgriddata[[@rowcount+7,@colcount]].to_i, "primaryUnderwriterName"=>@dealgriddata[[@rowcount+8,@colcount]]}

          #@actualcompareddata[cellcount] = {"dealName" => @dealgriddata[@colcount].to_s, "contractNumber" => @dealgriddata[@colcount+1].to_i, "inceptionDate" => @dealgriddata[@colcount+2].to_s, "targetDate" => @dealgriddata[@colcount+3].to_s, "priority" => @dealgriddata[@colcount+4].to_s, "submittedDate" => @dealgriddata[@colcount+5].to_s, "status" => @dealgriddata[@colcount+6].to_s, "dealNumber" => @dealgriddata[@colcount+7].to_i, "primaryUnderwriterName" => @dealgriddata[@colcount+8].to_s}
          @actualcompareddata[cellcount] = {"dealName" => @dealgriddata[[@rowcount,@colcount]], "contractNumber" => @dealgriddata[[@rowcount,@colcount+1]].to_i, "inceptionDate" => @dealgriddata[[@rowcount,@colcount+2]], "targetDate" => @dealgriddata[[@rowcount,@colcount+3]], "priority" => @dealgriddata[[@rowcount,@colcount+4]], "submittedDate" => @dealgriddata[[@rowcount,@colcount+5]], "status" => @dealgriddata[[@rowcount+6,@colcount]], "dealNumber" => @dealgriddata[[@rowcount,@colcount+7]].to_i, "primaryUnderwriterName" => @dealgriddata[[@rowcount,@colcount+8]]}#, "secondaryUnderwriterName" => @dealgriddata[[@rowcount+9,@colcount]], "technicalAssistantName" => @dealgriddata[[@rowcount+10,@colcount]], "modellerName" => @dealgriddata[[@rowcount+11,@colcount]], "actuaryName" => @dealgriddata[[@rowcount+12,@colcount]]}
          #""=>0, ""=>"2018-01-01", ""=>"2017-12-18", ""=>"On Hold", ""=>1367665, ""=>"Kirby Montgomery", ""=>"Kate Trent"}
          #@colcount = @colcount + 1
          @rowcount = @rowcount + 1
        end
      end

    end


    @actualcompareddata.delete(nil)

    #print "\n"
    #print @actualcompmodifieddata
    #print "\n"
    #print @actualcompareddata#["null"]
    #print "\n"


    return @actualcompareddata
  end
  def ModifyAllGridCellData(griddata)
    @dealgriddata = griddata
    # print "\n"
    # print @dealgriddata
    # print "\n"
    #@actualcompmodifieddata = Array.new
    #@actualcompmodifieddata = "{\"dealName\"=>" + @dealgriddata[0][0]+", \"contractNumber\"=>" +@dealgriddata[0][1] + ", \"inceptionDate\"=>" + @dealgriddata[0][3] + ", \"submittedDate\"=>" + @dealgriddata[0][4]+", \"status\"=>" + @dealgriddata[0][5] + ",\"dealNumber\"=>" + @dealgriddata[0][6] + ", \"primaryUnderwriterName\"=>" + @dealgriddata[0][7] + ", \"technicalAssistantName\"=>" + @dealgriddata[0][8] + "},    {\"dealName\"=>" + @dealgriddata[1][0]+", \"contractNumber\"=>" +@dealgriddata[1][1] + ", \"inceptionDate\"=>" + @dealgriddata[1][3] + ", \"submittedDate\"=>" + @dealgriddata[1][4]+", \"status\"=>" + @dealgriddata[1][5] + ",\"dealNumber\"=>" + @dealgriddata[1][6] + ", \"primaryUnderwriterName\"=>" + @dealgriddata[1][7] + ", \"technicalAssistantName\"=>" + @dealgriddata[1][8] + "},{\"dealName\"=>" + @dealgriddata[2][0]+", \"contractNumber\"=>" +@dealgriddata[2][1] + ", \"inceptionDate\"=>" + @dealgriddata[2][3] + ", \"submittedDate\"=>" + @dealgriddata[2][4]+", \"status\"=>" + @dealgriddata[2][5] + ", \"dealNumber\"=>" + @dealgriddata[2][6] + ", \"primaryUnderwriterName\"=>" + @dealgriddata[2][7] + ", \"technicalAssistantName\"=>" + @dealgriddata[2][8] + "}, {\"dealName\"=>" + @dealgriddata[3][0]+", \"contractNumber\"=>" +@dealgriddata[3][1] + ", \"inceptionDate\"=>" + @dealgriddata[3][3] + ", \"submittedDate\"=>" + @dealgriddata[3][4]+", \"status\"=>" + @dealgriddata[3][5] + ",\"dealNumber\"=>" + @dealgriddata[3][6] + ", \"primaryUnderwriterName\"=>" + @dealgriddata[3][7] + ", \"technicalAssistantName\"=>" + @dealgriddata[3][8] + "}, {\"dealName\"=>" + @dealgriddata[4][0]+", \"contractNumber\"=>" +@dealgriddata[4][1] + ", \"inceptionDate\"=>" + @dealgriddata[4][3] + ", \"submittedDate\"=>" + @dealgriddata[4][4]+", \"status\"=>" + @dealgriddata[4][5] + ",\"dealNumber\"=>" + @dealgriddata[4][6] + ", \"primaryUnderwriterName\"=>" + @dealgriddata[4][7] + ", \"technicalAssistantName\"=>" + @dealgriddata[4][8] + "},{\"dealName\"=>" + @dealgriddata[5][0]+", \"contractNumber\"=>" +@dealgriddata[5][1] + ", \"inceptionDate\"=>"+ @dealgriddata[5][3] + ", \"submittedDate\"=>" + @dealgriddata[5][4]+", \"status\"=>" + @dealgriddata[5][5] + ", \"dealNumber\"=>" + @dealgriddata[5][6] + ", \"primaryUnderwriterName\"=>" + @dealgriddata[5][7] + ", \"technicalAssistantName\"=>" + @dealgriddata[5][8] + "}"
    #@actualcompmodifieddata = "{\"dealName\"=>" << @dealgriddata[0][0]<<", \"contractNumber\"=>" <<@dealgriddata[0][1] << ", \"inceptionDate\"=>" << @dealgriddata[0][3] << ", \"submittedDate\"=>" << @dealgriddata[0][4]<<", \"status\"=>" << @dealgriddata[0][5] << ",\"dealNumber\"=>" << @dealgriddata[0][6] << ", \"primaryUnderwriterName\"=>" << @dealgriddata[0][7] << ", \"technicalAssistantName\"=>" << @dealgriddata[0][8] << "},    {\"dealName\"=>" << @dealgriddata[1][0]<<", \"contractNumber\"=>" <<@dealgriddata[1][1] << ", \"inceptionDate\"=>" << @dealgriddata[1][3] << ", \"submittedDate\"=>" << @dealgriddata[1][4]<<", \"status\"=>" << @dealgriddata[1][5] << ",\"dealNumber\"=>" << @dealgriddata[1][6] << ", \"primaryUnderwriterName\"=>" << @dealgriddata[1][7] << ", \"technicalAssistantName\"=>" << @dealgriddata[1][8] << "},{\"dealName\"=>" << @dealgriddata[2][0]<<", \"contractNumber\"=>" <<@dealgriddata[2][1] << ", \"inceptionDate\"=>" << @dealgriddata[2][3] << ", \"submittedDate\"=>" << @dealgriddata[2][4]<<", \"status\"=>" << @dealgriddata[2][5] << ", \"dealNumber\"=>" << @dealgriddata[2][6] << ", \"primaryUnderwriterName\"=>" << @dealgriddata[2][7] << ", \"technicalAssistantName\"=>" << @dealgriddata[2][8] << "}, {\"dealName\"=>" << @dealgriddata[3][0]<<", \"contractNumber\"=>" <<@dealgriddata[3][1] << ", \"inceptionDate\"=>" << @dealgriddata[3][3] << ", \"submittedDate\"=>" << @dealgriddata[3][4]<<", \"status\"=>" << @dealgriddata[3][5] << ",\"dealNumber\"=>" << @dealgriddata[3][6] << ", \"primaryUnderwriterName\"=>" << @dealgriddata[3][7] << ", \"technicalAssistantName\"=>" << @dealgriddata[3][8] << "}, {\"dealName\"=>" << @dealgriddata[4][0]<<", \"contractNumber\"=>" <<@dealgriddata[4][1] << ", \"inceptionDate\"=>" << @dealgriddata[4][3] << ", \"submittedDate\"=>" << @dealgriddata[4][4]<<", \"status\"=>" << @dealgriddata[4][5] << ",\"dealNumber\"=>" << @dealgriddata[4][6] << ", \"primaryUnderwriterName\"=>" << @dealgriddata[4][7] << ", \"technicalAssistantName\"=>" << @dealgriddata[4][8] << "},{\"dealName\"=>" << @dealgriddata[5][0]<<", \"contractNumber\"=>" <<@dealgriddata[5][1] << ", \"inceptionDate\"=>"<< @dealgriddata[5][3] << ", \"submittedDate\"=>" << @dealgriddata[5][4]<<", \"status\"=>" << @dealgriddata[5][5] << ", \"dealNumber\"=>" << @dealgriddata[5][6] << ", \"primaryUnderwriterName\"=>" << @dealgriddata[5][7] << ", \"technicalAssistantName\"=>" << @dealgriddata[5][8] << "}"
    #@actualcompmodifieddata = "{\"dealName\"=>" + @dealgriddata[0][0].to_s + ", \"contractNumber\"=>" +@dealgriddata[0][1].to_s + ", \"inceptionDate\"=>" + @dealgriddata[0][3].to_s + ", \"submittedDate\"=>" + @dealgriddata[0][4].to_s + ", \"status\"=>" + @dealgriddata[0][5].to_s + ",\"dealNumber\"=>" + @dealgriddata[0][6].to_s + ", \"primaryUnderwriterName\"=>" + @dealgriddata[0][7].to_s + ", \"technicalAssistantName\"=>" + @dealgriddata[0][8].to_s + "},    {\"dealName\"=>" + @dealgriddata[1][0].to_s + ", \"contractNumber\"=>" +@dealgriddata[1][1].to_s + ", \"inceptionDate\"=>" + @dealgriddata[1][3].to_s + ", \"submittedDate\"=>" + @dealgriddata[1][4].to_s + ", \"status\"=>" + @dealgriddata[1][5].to_s + ",\"dealNumber\"=>" + @dealgriddata[1][6].to_s + ", \"primaryUnderwriterName\"=>" + @dealgriddata[1][7].to_s + ", \"technicalAssistantName\"=>" + @dealgriddata[1][8].to_s + "},{\"dealName\"=>" + @dealgriddata[2][0].to_s + ", \"contractNumber\"=>" +@dealgriddata[2][1].to_s + ", \"inceptionDate\"=>" + @dealgriddata[2][3].to_s + ", \"submittedDate\"=>" + @dealgriddata[2][4].to_s + ", \"status\"=>" + @dealgriddata[2][5].to_s + ", \"dealNumber\"=>" + @dealgriddata[2][6].to_s + ", \"primaryUnderwriterName\"=>" + @dealgriddata[2][7].to_s + ", \"technicalAssistantName\"=>" + @dealgriddata[2][8].to_s + "}, {\"dealName\"=>" + @dealgriddata[3][0].to_s + ", \"contractNumber\"=>" +@dealgriddata[3][1].to_s + ", \"inceptionDate\"=>" + @dealgriddata[3][3].to_s + ", \"submittedDate\"=>" + @dealgriddata[3][4].to_s + ", \"status\"=>" + @dealgriddata[3][5].to_s + ",\"dealNumber\"=>" + @dealgriddata[3][6].to_s + ", \"primaryUnderwriterName\"=>" + @dealgriddata[3][7].to_s + ", \"technicalAssistantName\"=>" + @dealgriddata[3][8].to_s + "}, {\"dealName\"=>" + @dealgriddata[4][0].to_s + ", \"contractNumber\"=>" +@dealgriddata[4][1].to_s + ", \"inceptionDate\"=>" + @dealgriddata[4][3].to_s + ", \"submittedDate\"=>" + @dealgriddata[4][4].to_s + ", \"status\"=>" + @dealgriddata[4][5].to_s + ",\"dealNumber\"=>" + @dealgriddata[4][6].to_s + ", \"primaryUnderwriterName\"=>" + @dealgriddata[4][7].to_s + ", \"technicalAssistantName\"=>" + @dealgriddata[4][8].to_s + "},{\"dealName\"=>" + @dealgriddata[5][0].to_s + ", \"contractNumber\"=>" +@dealgriddata[5][1].to_s + ", \"inceptionDate\"=>"+ @dealgriddata[5][3].to_s + ", \"submittedDate\"=>" + @dealgriddata[5][4].to_s + ", \"status\"=>" + @dealgriddata[5][5].to_s + ", \"dealNumber\"=>" + @dealgriddata[5][6].to_s + ", \"primaryUnderwriterName\"=>" + @dealgriddata[5][7].to_s + ", \"technicalAssistantName\"=>" + @dealgriddata[5][8].to_s + "}"
    #@actualcompmodifieddata = "[{\"dealName\"=>" + "\"" + @dealgriddata[0][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[0][1].to_s + "\""   + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[0][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[0][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[0][4].to_s + "\""   + ",\"dealNumber\"=>" + "\"" + @dealgriddata[0][5].to_s + "\""   + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[0][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[0][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[1][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[1][1].to_s + "\""   + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[1][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[1][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[1][4].to_s + "\""   + ",\"dealNumber\"=>" + "\"" + @dealgriddata[1][5].to_s + "\""   + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[1][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[1][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[2][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[2][1].to_s + "\""   + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[2][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[2][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[2][4].to_s + "\""   + ", \"dealNumber\"=>" + "\"" + @dealgriddata[2][5].to_s + "\""   + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[2][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[2][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[3][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[3][1].to_s + "\""   + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[3][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[3][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[3][4].to_s + "\""   + ",\"dealNumber\"=>" + "\"" + @dealgriddata[3][5].to_s + "\""   + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[3][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[3][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[4][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[4][1].to_s + "\""   + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[4][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[4][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[4][4].to_s + "\""   + ",\"dealNumber\"=>" + "\"" + @dealgriddata[4][5].to_s + "\""   + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[4][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[4][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[5][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[5][1].to_s + "\""   + ", \"inceptionDate\"=>"+ "\"" + @dealgriddata[5][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[5][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[5][4].to_s + "\""   + ", \"dealNumber\"=>" + "\"" + @dealgriddata[5][5].to_s + "\""   + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[5][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[5][7].to_s + "\""   + "}]"
    #@actualcompmodifieddata = "[{\"dealName\"=>" + "\"" + @dealgriddata[0][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[0][1].to_s + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[0][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[0][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[0][4].to_s + "\""   + ",\"dealNumber\"=>" + "\"" + @dealgriddata[0][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[0][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[0][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[1][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[1][1].to_s + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[1][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[1][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[1][4].to_s + "\""   + ",\"dealNumber\"=>" + "\"" + @dealgriddata[1][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[1][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[1][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[2][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[2][1].to_s + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[2][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[2][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[2][4].to_s + "\""   + ", \"dealNumber\"=>" + "\"" + @dealgriddata[2][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[2][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[2][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[3][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[3][1].to_s + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[3][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[3][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[3][4].to_s + "\""   + ",\"dealNumber\"=>" + "\"" + @dealgriddata[3][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[3][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[3][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[4][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[4][1].to_s + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[4][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[4][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[4][4].to_s + "\""   + ",\"dealNumber\"=>" + "\"" + @dealgriddata[4][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[4][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[4][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[5][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[5][1].to_s + ", \"inceptionDate\"=>"+ "\"" + @dealgriddata[5][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[5][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[5][4].to_s + "\""   + ", \"dealNumber\"=>" + "\"" + @dealgriddata[5][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[5][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[5][7].to_s + "\""   + "}]"
    #@actualcompmodifieddata = "[{\"dealName\"=>" + "\"" + @dealgriddata[0][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[0][1].to_s + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[0][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[0][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[0][4].to_s + "\""   + ", \"dealNumber\"=>" + @dealgriddata[0][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[0][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[0][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[1][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[1][1].to_s + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[1][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[1][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[1][4].to_s + "\""   + ", \"dealNumber\"=>" + @dealgriddata[1][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[1][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[1][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[2][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[2][1].to_s + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[2][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[2][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[2][4].to_s + "\""   + ", \"dealNumber\"=>" + @dealgriddata[2][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[2][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[2][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[3][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[3][1].to_s + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[3][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[3][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[3][4].to_s + "\""   + ", \"dealNumber\"=>" + @dealgriddata[3][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[3][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[3][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[4][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[4][1].to_s + ", \"inceptionDate\"=>" + "\"" + @dealgriddata[4][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[4][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[4][4].to_s + "\""   + ", \"dealNumber\"=>" + @dealgriddata[4][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[4][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[4][7].to_s + "\""   + "}, {\"dealName\"=>" + "\"" + @dealgriddata[5][0].to_s + "\""   + ", \"contractNumber\"=>" +@dealgriddata[5][1].to_s + ", \"inceptionDate\"=>"+ "\"" + @dealgriddata[5][2].to_s + "\""   + ", \"submittedDate\"=>" + "\"" + @dealgriddata[5][3].to_s + "\""   + ", \"status\"=>" + "\"" + @dealgriddata[5][4].to_s + "\""   + ", \"dealNumber\"=>" + @dealgriddata[5][5].to_s + ", \"primaryUnderwriterName\"=>" + "\"" + @dealgriddata[5][6].to_s + "\""   + ", \"technicalAssistantName\"=>" + "\"" + @dealgriddata[5][7].to_s + "\""   + "}]"
    #@moddealgriddata = Array.new
    #@moddealgriddata = "{"dealNumber
    #return @dealgriddata
    #print "\n"
    #print @actualcompmodifieddata
    #print "\n"
    #
    #for i in 0..12
    #  for j in 0..5
    #    print "\n"
    #    print @dealgriddata[[i,j]]
    #    print "\n"
    #    #if @dealgriddata[[i,j]] == ""
    #      @dealgriddata[[i,j]] = nil
    #    end
    #  end
    #end

    #    print "\n"
    #    print cell
    #    print "\n"

    #    print "Value at [" + @rowcount.to_s + ","+ @colcount.to_s + "]: "+ cell.to_s+ "\n"#[j]
    #    @colcount = @colcount + 1
    #  end
    #  @rowcount = @rowcount + 1
    #  @colcount = 0
    #end
    #@dealgriddata.each do |row|
    #  row.each do |cell|
    #    print cell
    #    print "Value at [" + @rowcount.to_s + ","+ @colcount.to_s + "]: "+ cell.to_s+ "\n"#[j]
    #    @colcount = @colcount + 1
    #  end
    #  @rowcount = @rowcount + 1
    #  @colcount = 0
    #end
    # print "\n"
    # print @dealgriddata
    # print "\n"
    @possibleRows = 6

    # if @dealgriddata[[9,0]].to_s != "Underwriter 2"
    #   @actualcompareddata = Array.new
    #   @colcount=0
    #   @rowcount = 9
    #   if @dealgriddata[[@rowcount,@colcount]].to_s == "nil"
    #     @possibleRows=0
    #   elsif @dealgriddata[[@rowcount,@colcount+9]].to_s == "nil"
    #     @possibleRows=1
    #   elsif @dealgriddata[[@rowcount,@colcount+18]].to_s == "nil"
    #     @possibleRows=2
    #   elsif @dealgriddata[[@rowcount,@colcount+27]].to_s == "nil"
    #     @possibleRows=3
    #   elsif @dealgriddata[[@rowcount,@colcount+36]].to_s == "nil"
    #     @possibleRows=4
    #   elsif @dealgriddata[[@rowcount,@colcount+45]].to_s == "nil"
    #     @possibleRows=5
    #   else
    #     @possibleRows=6
    #   end
    #   if @possibleRows != 0
    #     for cellcount in 1..@possibleRows
    #       #@actualcompareddata[cellcount] = {"dealName"=>@dealgriddata[[@rowcount,@colcount]], "contractNumber"=>@dealgriddata[[@rowcount+1,@colcount]].to_i, "inceptionDate"=>@dealgriddata[[@rowcount+2,@colcount]], "targetDate"=>@dealgriddata[[@rowcount+3,@colcount]], "priority"=>@dealgriddata[[@rowcount+4,@colcount]], "submittedDate"=>@dealgriddata[[@rowcount+5,@colcount]], "status"=>@dealgriddata[[@rowcount+6,@colcount]], "dealNumber"=>@dealgriddata[[@rowcount+7,@colcount]].to_i, "primaryUnderwriterName"=>@dealgriddata[[@rowcount+8,@colcount]]}
    #
    #       #@actualcompareddata[cellcount] = {"dealName" => @dealgriddata[@colcount].to_s, "contractNumber" => @dealgriddata[@colcount+1].to_i, "inceptionDate" => @dealgriddata[@colcount+2].to_s, "targetDate" => @dealgriddata[@colcount+3].to_s, "priority" => @dealgriddata[@colcount+4].to_s, "submittedDate" => @dealgriddata[@colcount+5].to_s, "status" => @dealgriddata[@colcount+6].to_s, "dealNumber" => @dealgriddata[@colcount+7].to_i, "primaryUnderwriterName" => @dealgriddata[@colcount+8].to_s}
    #       @actualcompareddata[cellcount] = {"dealName" => @dealgriddata[[@rowcount,@colcount]], "contractNumber" => @dealgriddata[[@rowcount+1,@colcount]].to_i, "inceptionDate" => @dealgriddata[[@rowcount+2,@colcount]], "targetDate" => @dealgriddata[[@rowcount+3,@colcount]], "priority" => @dealgriddata[[@rowcount+4,@colcount]].to_i, "submittedDate" => @dealgriddata[[@rowcount+5,@colcount]], "status" => @dealgriddata[[@rowcount+6,@colcount]], "dealNumber" => @dealgriddata[[@rowcount+7,@colcount]].to_i, "primaryUnderwriterName" => @dealgriddata[[@rowcount+8,@colcount]]}#, "secondaryUnderwriterName" => @dealgriddata[[@rowcount+9,@colcount]], "technicalAssistantName" => @dealgriddata[[@rowcount+10,@colcount]], "modellerName" => @dealgriddata[[@rowcount+11,@colcount]], "actuaryName" => @dealgriddata[[@rowcount+12,@colcount]]}
    #       #""=>0, ""=>"2018-01-01", ""=>"2017-12-18", ""=>"On Hold", ""=>1367665, ""=>"Kirby Montgomery", ""=>"Kate Trent"}
    #       @rowcount = @rowcount + 9
    #     end
    #   end
    # end
    # if @dealgriddata[[0,12]].to_s == "Actuary"
    #   @actualcompareddata = Array.new
    #   @colcount=0
    #   @rowcount = 13
    #   if @dealgriddata[[@rowcount,@colcount]].to_s == "nil"
    #     @possibleRows=0
    #   elsif @dealgriddata[[@rowcount,@colcount+13]].to_s == "nil"
    #     @possibleRows=1
    #   elsif @dealgriddata[[@rowcount,@colcount+26]].to_s == "nil"
    #     @possibleRows=2
    #   elsif @dealgriddata[[@rowcount,@colcount+39]].to_s == "nil"
    #     @possibleRows=3
    #   elsif @dealgriddata[[@rowcount,@colcount+52]].to_s == "nil"
    #     @possibleRows=4
    #   elsif @dealgriddata[[@rowcount,@colcount+65]].to_s == "nil"
    #     @possibleRows=5
    #   else
    #     @possibleRows=6
    #   end
    #   if @possibleRows != 0
    #     for cellcount in 1..@possibleRows
    #       #@actualcompareddata[cellcount] = {"dealName"=>@dealgriddata[[@rowcount,@colcount]], "contractNumber"=>@dealgriddata[[@rowcount+1,@colcount]].to_i, "inceptionDate"=>@dealgriddata[[@rowcount+2,@colcount]], "targetDate"=>@dealgriddata[[@rowcount+3,@colcount]], "priority"=>@dealgriddata[[@rowcount+4,@colcount]], "submittedDate"=>@dealgriddata[[@rowcount+5,@colcount]], "status"=>@dealgriddata[[@rowcount+6,@colcount]], "dealNumber"=>@dealgriddata[[@rowcount+7,@colcount]].to_i, "primaryUnderwriterName"=>@dealgriddata[[@rowcount+8,@colcount]]}
    #
    #       #@actualcompareddata[cellcount] = {"dealName" => @dealgriddata[@colcount].to_s, "contractNumber" => @dealgriddata[@colcount+1].to_i, "inceptionDate" => @dealgriddata[@colcount+2].to_s, "targetDate" => @dealgriddata[@colcount+3].to_s, "priority" => @dealgriddata[@colcount+4].to_s, "submittedDate" => @dealgriddata[@colcount+5].to_s, "status" => @dealgriddata[@colcount+6].to_s, "dealNumber" => @dealgriddata[@colcount+7].to_i, "primaryUnderwriterName" => @dealgriddata[@colcount+8].to_s}
    #       @actualcompareddata[cellcount] = {"dealName" => @dealgriddata[[@rowcount,@colcount]], "contractNumber" => @dealgriddata[[@rowcount+1,@colcount]].to_i, "inceptionDate" => @dealgriddata[[@rowcount+2,@colcount]], "targetDate" => @dealgriddata[[@rowcount+3,@colcount]], "priority" => @dealgriddata[[@rowcount+4,@colcount]].to_i, "submittedDate" => @dealgriddata[[@rowcount+5,@colcount]], "status" => @dealgriddata[[@rowcount+6,@colcount]], "dealNumber" => @dealgriddata[[@rowcount+7,@colcount]].to_i, "primaryUnderwriterName" => @dealgriddata[[@rowcount+8,@colcount]]}#, "secondaryUnderwriterName" => @dealgriddata[[@rowcount+9,@colcount]], "technicalAssistantName" => @dealgriddata[[@rowcount+10,@colcount]], "modellerName" => @dealgriddata[[@rowcount+11,@colcount]], "actuaryName" => @dealgriddata[[@rowcount+12,@colcount]]}
    #       #""=>0, ""=>"2018-01-01", ""=>"2017-12-18", ""=>"On Hold", ""=>1367665, ""=>"Kirby Montgomery", ""=>"Kate Trent"}
    #       @rowcount = @rowcount + 13
    #     end
    #   end
    # end
    # if @dealgriddata[[12,0]].to_s == "Actuary" && (@dealgriddata[[3,3]].to_s == "nil" || @dealgriddata[[3,3]].to_s == "")
    #   @actualcompareddata = Array.new
    #   @colcount=1
    #   @rowcount = 1
    #   if @dealgriddata[[@rowcount+78,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount+78,@colcount]].to_s == ""
    #   @possibleRows=6
    #   end
    #   if @dealgriddata[[@rowcount+65,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount+65,@colcount]].to_s == ""
    #     @possibleRows=5
    #   end
    #   if @dealgriddata[[@rowcount+52,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount+52,@colcount]].to_s == ""
    #     @possibleRows=4
    #   end
    #   if @dealgriddata[[@rowcount+39,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount+39,@colcount]].to_s == ""
    #     @possibleRows=3
    #   end
    #   if @dealgriddata[[@rowcount+26,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount+26,@colcount]].to_s == ""
    #     @possibleRows=2
    #   end
    #   if @dealgriddata[[@rowcount+13,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount+13,@colcount]].to_s == ""
    #     @possibleRows=1
    #   end
    #   if @dealgriddata[[@rowcount,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount,@colcount]].to_s == ""
    #     @possibleRows=0
    #   end
    #   #print "\n"
    #   print "Grid is displayed with " + @possibleRows.to_s + " deals.\n"
    #   #print "\n"
    #
    #   if @possibleRows != 0
    #     for cellcount in 0..@possibleRows-1
    #       #@actualcompareddata[cellcount] = {"dealName"=>@dealgriddata[[@rowcount,@colcount]], "contractNumber"=>@dealgriddata[[@rowcount+1,@colcount]].to_i, "inceptionDate"=>@dealgriddata[[@rowcount+2,@colcount]], "targetDate"=>@dealgriddata[[@rowcount+3,@colcount]], "priority"=>@dealgriddata[[@rowcount+4,@colcount]], "submittedDate"=>@dealgriddata[[@rowcount+5,@colcount]], "status"=>@dealgriddata[[@rowcount+6,@colcount]], "dealNumber"=>@dealgriddata[[@rowcount+7,@colcount]].to_i, "primaryUnderwriterName"=>@dealgriddata[[@rowcount+8,@colcount]]}
    #
    #       #@actualcompareddata[cellcount] = {"dealName" => @dealgriddata[@colcount].to_s, "contractNumber" => @dealgriddata[@colcount+1].to_i, "inceptionDate" => @dealgriddata[@colcount+2].to_s, "targetDate" => @dealgriddata[@colcount+3].to_s, "priority" => @dealgriddata[@colcount+4].to_s, "submittedDate" => @dealgriddata[@colcount+5].to_s, "status" => @dealgriddata[@colcount+6].to_s, "dealNumber" => @dealgriddata[@colcount+7].to_i, "primaryUnderwriterName" => @dealgriddata[@colcount+8].to_s}
    #       @actualcompareddata[cellcount] = {"dealName" => @dealgriddata[[@rowcount,@colcount]], "contractNumber" => @dealgriddata[[@rowcount+1,@colcount]].to_i, "inceptionDate" => @dealgriddata[[@rowcount+2,@colcount]], "targetDate" => @dealgriddata[[@rowcount+3,@colcount]], "priority" => @dealgriddata[[@rowcount+4,@colcount]].to_i, "submittedDate" => @dealgriddata[[@rowcount+5,@colcount]], "status" => @dealgriddata[[@rowcount+6,@colcount]], "dealNumber" => @dealgriddata[[@rowcount+7,@colcount]].to_i, "primaryUnderwriterName" => @dealgriddata[[@rowcount+8,@colcount]], "secondaryUnderwriterName" => @dealgriddata[[@rowcount+9,@colcount]], "technicalAssistantName" => @dealgriddata[[@rowcount+10,@colcount]], "modellerName" => @dealgriddata[[@rowcount+11,@colcount]], "actuaryName" => @dealgriddata[[@rowcount+12,@colcount]]}
    #       #""=>0, ""=>"2018-01-01", ""=>"2017-12-18", ""=>"On Hold", ""=>1367665, ""=>"Kirby Montgomery", ""=>"Kate Trent"}
    #       @rowcount = @rowcount + 13
    #     end
    #   elsif @possibleRows == 0
    #     print "No data available in the grid.\n"
    #   end
    # end

    if @dealgriddata[[0,12]].to_s == "Actuary" #&& (@dealgriddata[[3,3]].to_s == "nil" || @dealgriddata[[3,3]].to_s == "")
      @actualcompareddata = Array.new
      @colcount = 0
      @rowcount = 1
      if @dealgriddata[[@rowcount+6,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount+6,@colcount]].to_s == ""
        @possibleRows=6
      end
      if @dealgriddata[[@rowcount+5,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount+5,@colcount]].to_s == ""
        @possibleRows=5
      end
      if @dealgriddata[[@rowcount+4,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount+4,@colcount]].to_s == ""
        @possibleRows=4
      end
      if @dealgriddata[[@rowcount+3,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount+3,@colcount]].to_s == ""
        @possibleRows=3
      end
      if @dealgriddata[[@rowcount+2,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount+2,@colcount]].to_s == ""
        @possibleRows=2
      end
      if @dealgriddata[[@rowcount+1,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount+1,@colcount]].to_s == ""
        @possibleRows=1
      end
      if @dealgriddata[[@rowcount,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount,@colcount]].to_s == ""
        @possibleRows=0
      end
      #print "\n"
      print "Grid is displayed with " + @possibleRows.to_s + " deals.\n"
      #print "\n"

      if @possibleRows != 0
        for cellcount in 0..@possibleRows-1
          #@actualcompareddata[cellcount] = {"dealName"=>@dealgriddata[[@rowcount,@colcount]], "contractNumber"=>@dealgriddata[[@rowcount+1,@colcount]].to_i, "inceptionDate"=>@dealgriddata[[@rowcount+2,@colcount]], "targetDate"=>@dealgriddata[[@rowcount+3,@colcount]], "priority"=>@dealgriddata[[@rowcount+4,@colcount]], "submittedDate"=>@dealgriddata[[@rowcount+5,@colcount]], "status"=>@dealgriddata[[@rowcount+6,@colcount]], "dealNumber"=>@dealgriddata[[@rowcount+7,@colcount]].to_i, "primaryUnderwriterName"=>@dealgriddata[[@rowcount+8,@colcount]]}

          #@actualcompareddata[cellcount] = {"dealName" => @dealgriddata[@colcount].to_s, "contractNumber" => @dealgriddata[@colcount+1].to_i, "inceptionDate" => @dealgriddata[@colcount+2].to_s, "targetDate" => @dealgriddata[@colcount+3].to_s, "priority" => @dealgriddata[@colcount+4].to_s, "submittedDate" => @dealgriddata[@colcount+5].to_s, "status" => @dealgriddata[@colcount+6].to_s, "dealNumber" => @dealgriddata[@colcount+7].to_i, "primaryUnderwriterName" => @dealgriddata[@colcount+8].to_s}
          @actualcompareddata[cellcount] = {"dealName" => @dealgriddata[[@rowcount,@colcount]], "contractNumber" => @dealgriddata[[@rowcount,@colcount+1]].to_i, "inceptionDate" => @dealgriddata[[@rowcount,@colcount+2]], "targetDate" => @dealgriddata[[@rowcount,@colcount+3]], "priority" => @dealgriddata[[@rowcount,@colcount+4]].to_i, "submittedDate" => @dealgriddata[[@rowcount,@colcount+5]], "status" => @dealgriddata[[@rowcount,@colcount+6]], "dealNumber" => @dealgriddata[[@rowcount,@colcount+7]].to_i, "primaryUnderwriterName" => @dealgriddata[[@rowcount,@colcount+8]], "secondaryUnderwriterName" => @dealgriddata[[@rowcount,@colcount+9]], "technicalAssistantName" => @dealgriddata[[@rowcount,@colcount+10]], "modellerName" => @dealgriddata[[@rowcount,@colcount+11]], "actuaryName" => @dealgriddata[[@rowcount,@colcount+12]]}
          #""=>0, ""=>"2018-01-01", ""=>"2017-12-18", ""=>"On Hold", ""=>1367665, ""=>"Kirby Montgomery", ""=>"Kate Trent"}
          @rowcount = @rowcount + 1
        end
      elsif @possibleRows == 0
        print "No data available in the grid.\n"
      end
    end



    #@actualcompareddata.delete(nil)
    #@actualcompareddata.to_s.gsub(/  /," ")

    #print "\n"
    #print @actualcompmodifieddata
    #print "\n"
    #print @actualcompareddata#["null"]
    #print "\n"
    print "\n"
    print @actualcompareddata
    print "\n"

    return @actualcompareddata
  end




  def GetModifyGridCellData(griddata,gridColNames)
    @actualcompareddata = Array.new
    @dealgriddata = griddata
    @possibleRows = 6
    @output = ""
    @iterator = 0

    # for @row in 1..@possibleRows
    #   for @col in 0..gridColNames.length-1
    #    @output = @output.to_s +  '"' +@dealgriddata[[0, @col]].to_s+ '"' +" => "+'"'+@dealgriddata[[@row, @col]].to_s+ '" ,'
    #    puts @output
    #     if @col == gridColNames.length-1
    #       # puts @output.inspect
    #        @actualcompareddata[@iterator] = {@output.chop}
    #       # @output = ""
    #        @iterator = @iterator+1
    #     end
    #   end
    # end

    @output = Hash.new
    for @row in 1..@possibleRows
      for @col in 0..gridColNames.length-1
        @output[@dealgriddata[[0, @col]].to_s]  = @dealgriddata[[@row, @col]].to_s
        puts @output
        if @col == gridColNames.length-1
          # puts @output.inspect
          # @actualcompareddata[@iterator] = @output
          # # @output = ""
          # @iterator = @iterator+1
          @actualcompareddata << @output
        end
      end
    end


    print " findal result \n"
    print @actualcompareddata.to_s
    print "\n"

    return @actualcompareddata
  end

  def CompareResults(actualresult,queryresults)
    #@apiresultval = actualresult
    #@queryresultval = Array.new
    @queryresultval = queryresults.to_s
    #@actualresult = Array.new
    @actualresult = actualresult.to_s
    #print "\n"
    #print @apiresultval
    #print "\n"
    #print @queryresultval
    #print "\n"
    #print @responsebodyvalue
    #print "\n"
    #@@apihash = Hashie::Mash.new
    #@@apihash = @apiresultval
    # print "\n"
    # print @actualresult
    # print "\n"
    # print @queryresultval
    # print "\n"

    #print @@apihash
    #@queryresultval.extend Hashie::Extensions::DeepLocate
    #@queryresultval.extend Hashie::Extensions::DeepFind
    #@responsebodyvalue.extend Hashie::Extensions::DeepLocate
    #@responsebodyvalue.extend Hashie::Extensions::DeepFind
    #print @queryresultval.deep_locate -> (key,value,object) {key == "statusid" && value == 80}
    #print "\n"
    #print @responsebodyvalue.deep_locate -> (key,value,object) {key == "statusid" && value == 80}
    #print "\n"
    #print @actualresult.to_s.eql? @queryresultval.to_s


    # @datamatchresult = (@actualresult <=> @queryresultval)
    # print @datamatchresult.to_s
    # print "\n"
    #@diff = (@actualresult - @queryresultval) + (@queryresultval - @actualresult)

    if (@actualresult <=> @queryresultval)  == 0#.empty? == true #@diff.empty == true#(@actualresult.compare(@queryresultval)) == true #.empty? == true
      print "PASSED - The UI Data and DB Query results are matched successfully.\n"
      #@reporter.ReportAction("PASSED - The UI Data and DB Query results are matched successfully.\n")
      return 1
    else
      # print "\n"
      # print "UI DATA: " + @actualresult.to_s
      # print "\n"
      # print "DB DATA: " + @queryresultval.to_s
      # print "\n"
      print "FAILED - The UI Data failed to match with DB Query results.\n"
      fail "FAILED - The UI Data failed to match with DB Query results.\n"
      #@reporter.ReportAction("FAILED - The UI data failed to match with DB Query results.\n")
      return 0
    end
  end
  def verifyDescendingOrderDealNumber(gData)
    @GridData = gData
    #@dealgriddataarr = Array.new(Array.new)
    #@rowcount = 0
    #@colcount = 0
    #@GridData.each do |row|
    #  row.each do |cell|
    #    print cell
    #    #@dealgriddataarr[@rowcount][@colcount] = cell
    #    print "Value at [" + @rowcount.to_s + ","+ @colcount.to_s + "]: "+ cell.to_s+ "\n"#[j]
    #    @colcount = @colcount + 1
    #  end
    #  @rowcount = @rowcount + 1
    #  @colcount = 0
    #end
    #print "\n"
    #print (@GridData[0][5] <=> @GridData[1][5])
    #print "\n"
    #print (@GridData[1][5] <=> @GridData[2][5])
    #print "\n"
    #print (@GridData[2][5] <=> @GridData[3][5])
    #print "\n"
    #print (@GridData[3][5] <=> @GridData[4][5])
    #print "\n"
    #print (@GridData[4][5] <=> @GridData[5][5])
    #print "\n"
    # print "\n"
    # print @GridData
    # print "\n"

    #if (@GridData[[8,1]].to_s <=> @GridData[[21,1]].to_s) >= 0 && (@GridData[[21,1]].to_s <=> @GridData[[34,1]].to_s) >= 0 && (@GridData[[34,1]].to_s <=> @GridData[[47,1]].to_s) >= 0  && (@GridData[[47,1]].to_s <=> @GridData[[60,1]].to_s) >= 0 && (@GridData[[60,1]].to_s <=> @GridData[[73,1]].to_s) >= 0
    if (@GridData[[1,7]].to_s <=> @GridData[[2,7]].to_s) >= 0 && (@GridData[[2,7]].to_s <=> @GridData[[3,7]].to_s) >= 0 && (@GridData[[3,7]].to_s <=> @GridData[[4,7]].to_s) >= 0  && (@GridData[[4,7]].to_s <=> @GridData[[5,7]].to_s) >= 0 && (@GridData[[5,7]].to_s <=> @GridData[[6,7]].to_s) >= 0
      print "PASSED - The UI Data is sorted in descending order of Deal Number.\n"
      #@reporter.ReportAction("PASSED - The UI Data is sorted in descending order of Deal Number.\n")
    else
      print "FAILED - The UI Data is not sorted in descending order of Deal Number.\n"
      fail "FAILED - The UI Data is not sorted in descending order of Deal Number.\n"
      #@reporter.ReportAction("FAILED - The UI Data is not sorted in descending order of Deal Number.\n")
    end
  end
  def verifyDescendingOrderDealName(gData)
   # @GridData = Array.new[][]
    @GridData = gData
    #@dealgriddataarr = Array.new(Array.new)
    #@GridData.each do |row|
    #  row.each do |cell|
    #    #@dealgriddataarr[@rowcount][@colcount] = cell
    #    print cell #"Value at [" + @rowcount.to_s + ","+ @colcount.to_s + "]: "+ cell.to_s+ "\n"#[j]
    #    @colcount = @colcount + 1
    #  end
    #  @rowcount = @rowcount + 1
    #  @colcount = 0
    #end
    #print "\n"
    #print @GridData[0][0].to_s + "-" + @GridData[1][0].to_s + ":" + (@GridData[0][0] <=> @GridData[1][0]).to_s
    #print "\n"
    #print @GridData[1][0].to_s + "-" + @GridData[2][0].to_s + ":" + (@GridData[1][0] <=> @GridData[2][0]).to_s
    #print "\n"
    #print @GridData[2][0].to_s + "-" + @GridData[3][0].to_s + ":" + (@GridData[2][0] <=> @GridData[3][0]).to_s
    #print "\n"
    #print @GridData[3][0].to_s + "-" + @GridData[4][0].to_s + ":" + (@GridData[3][0] <=> @GridData[4][0]).to_s
    #print "\n"
    #print @GridData[4][0].to_s + "-" + @GridData[5][0].to_s + ":" + (@GridData[4][0] <=> @GridData[5][0]).to_s
    #print "\n"
    #print @GridData[[13,1]]
    #print "\n"
    #print @GridData[[26,1]]
    #print "\n"
    #print (@GridData[[13,1]] <=> @GridData[[26,1]]).to_s
    #print "\n"
    #print @GridData[[26,1]]
    #print "\n"
    #print @GridData[[39,1]]
    #print "\n"
    #print (@GridData[[26,1]] <=> @GridData[[39,1]]).to_s
    #print "\n"
    #print @GridData[[39,1]]
    #print "\n"
    #print @GridData[[52,1]]
    #print "\n"
    #print (@GridData[[39,1]] <=> @GridData[[52,1]]).to_s
    #print "\n"
    #print @GridData[[52,1]]
    #print "\n"
    #print @GridData[[65,1]]
    #print "\n"
    #print (@GridData[[52,1]] <=> @GridData[[65,1]]).to_s
    #print "\n"
    #print @GridData[[65,1]]
    #print "\n"
    #print @GridData[[78,1]]
    #print "\n"
    #print (@GridData[[65,1]] <=> @GridData[[78,1]]).to_s
    #print "\n"
    #if (@GridData[[1,1]] <=> @GridData[[14,1]])>=0 && (@GridData[[14,1]] <=> @GridData[[27,1]])>=0 && (@GridData[[27,1]] <=> @GridData[[40,1]])>=0 && (@GridData[[40,1]] <=> @GridData[[53,1]]) >=0 && (@GridData[[53,1]] <=> @GridData[[66,1]]) >=0
    if (@GridData[[1,0]] <=> @GridData[[2,0]])>=0 && (@GridData[[2,0]] <=> @GridData[[3,0]])>=0 && (@GridData[[3,0]] <=> @GridData[[4,0]])>=0 && (@GridData[[4,0]] <=> @GridData[[5,0]]) >=0 && (@GridData[[5,0]] <=> @GridData[[6,0]]) >=0
    #if (@GridData[13][1] <=> @GridData[26][1])>=0 && (@GridData[26][1] <=> @GridData[39][1])>=0 && (@GridData[39][1] <=> @GridData[52][1])>=0 && (@GridData[52][1] <=> @GridData[65][1])>=0 && (@GridData[65][1] <=> @GridData[78][1])>=0
      print "PASSED - The UI Data is sorted in descending order of Deal Name.\n"
      #@reporter.ReportAction("PASSED - The UI Data is sorted in descending order of Deal Name.\n")
    else
      print "FAILED - The UI Data is not sorted in descending order of Deal Name.\n"
      fail "FAILED - The UI Data is not sorted in descending order of Deal Name.\n"
      #@reporter.ReportAction("FAILED - The UI Data is not sorted in descending order of Deal Name.\n")
    end
  end
  def verifyAscendingOrderDealName(gData)
    #@GridData = Array.new(Array.new)
    @GridData = gData
    #@dealgriddataarr = Array.new(Array.new)
    #@dealgriddata = @GridData
    #@GridData.each do |row|
    #  row.each do |cell|
    #    @dealgriddataarr[@rowcount][@colcount] = cell.to_s
    #    print "Value at [" + @rowcount.to_s + ","+ @colcount.to_s + "]: "+ cell.to_s+ "\n"#[j]
    #    @colcount = @colcount + 1
    #  end
    #  @rowcount = @rowcount + 1
    #  @colcount = 0
    #end
    #print "\n"
    #print @GridData[[13,1]]
    #print "\n"
    #print @GridData[[26,1]]
    #print "\n"
    #print (@GridData[[13,1]] <=> @GridData[[26,1]]).to_s
    #print "\n"
    #print @GridData[[26,1]]
    #print "\n"
    #print @GridData[[39,1]]
    #print "\n"
    #print (@GridData[[26,1]] <=> @GridData[[39,1]]).to_s
    #print "\n"
    #print @GridData[[39,1]]
    #print "\n"
    #print @GridData[[52,1]]
    #print "\n"
    #print (@GridData[[39,1]] <=> @GridData[[52,1]]).to_s
    #print "\n"
    #print @GridData[[52,1]]
    #print "\n"
    #print @GridData[[65,1]]
    #print "\n"
    #print (@GridData[[52,1]] <=> @GridData[[65,1]]).to_s
    #print "\n"
    #print @GridData[[65,1]]
    #print "\n"
    #print @GridData[[78,1]]
    #print "\n"
    #print (@GridData[[65,1]] <=> @GridData[[78,1]]).to_s
    #print "\n"
    #print " Multi-Line 1999 <=> 1 800 Packrat LLC - 2018 : " + (" Multi-Line 1999" <=> "1 800 Packrat LLC - 2018").to_s
    #print "\n"
    #print @GridData[[13,1]]
    #print "\n"
    #print "\n"
    #print @dealgriddataarr[13][1]
    #print "\n"
    #print "\n"
    #print @dealgriddataarr[13,1]
    #print "\n"
    #(@GridData[[1,1]] <=> @GridData[[14,1]])>=0 && (@GridData[[14,1]] <=> @GridData[[27,1]])>=0 && (@GridData[[27,1]] <=> @GridData[[40,1]])>=0 && (@GridData[[40,1]] <=> @GridData[[53,1]]) >=0 && (@GridData[[53,1]] <=> @GridData[[66,1]]) >=0
    if (@GridData[[1,0]] <=> @GridData[[2,0]]).to_i <= 0 && (@GridData[[2,0]] <=> @GridData[[3,0]]).to_i <= 0 && (@GridData[[3,0]] <=> @GridData[[4,0]]).to_i <= 0 && (@GridData[[4,0]] <=> @GridData[[5,0]]).to_i <= 0 && (@GridData[[5,0]] <=> @GridData[[6,0]]).to_i <= 0
    #if (@GridData[13] <=> @GridData[26]) <= 0 && (@GridData[26] <=> @GridData[39]) <= 0 && (@GridData[39] <=> @GridData[52]) <= 0 && (@GridData[52] <=> @GridData[65]) <= 0 && (@GridData[65] <=> @GridData[78]) <= 0
    #if (@GridData[13][1] <=> @GridData[26][1]) <= 0 && (@GridData[26][1] <=> @GridData[39][1]) <= 0 && (@GridData[39][1] <=> @GridData[52][1]) <= 0 && (@GridData[52][1] <=> @GridData[65][1]) <= 0 && (@GridData[65][1] <=> @GridData[78][1]) <= 0
      print "PASSED - The UI Data is sorted in ascending order of Deal Name.\n"
      #@reporter.ReportAction("PASSED - The UI Data is sorted in ascending order of Deal Name.\n")
    else
      print "FAILED - The UI Data is not sorted in ascending order of Deal Name.\n"
      fail "FAILED - The UI Data is not sorted in ascending order of Deal Name.\n"
      #@reporter.ReportAction("FAILED - The UI Data is not sorted in ascending order of Deal Name.\n")
    end
  end
  def verifyLocation(elementmoved)
    @MovedElement = elementmoved
    sleep 1
    @Elementlocation = @MovedElement.wd.location
    return @Elementlocation
  end
  def verifyRightMove(beforeLocation,afterlocation)
    @previousLocation = beforeLocation
    @shiftedLocation = afterlocation
    #print @previousLocation
    #print "\n"
    #print @shiftedLocation
    if (@previousLocation[0] < @shiftedLocation[0])
      print "PASSED - The Column is successfully moved to the right.\n"
      #@reporter.ReportAction("PASSED - The Column is successfully moved to the right.\n")
    else
      print "FAILED - The Column failed to moved to the right.\n"
      fail "FAILED - The Column failed to moved to the right.\n"
      #@reporter.ReportAction("FAILED - The Column failed to moved to the right.\n")
    end
  end
  def verifyLeftMove(beforeLocation,afterlocation)
    @previousLocation = beforeLocation
    @shiftedLocation = afterlocation
    #print @previousLocation
    #print "\n"
    #print @shiftedLocation
    if (@previousLocation[0] > @shiftedLocation[0])
      print "PASSED - The Column is successfully moved to the left.\n"
      #@reporter.ReportAction("PASSED - The Column is successfully moved to the right.\n")
    else
      print "FAILED - The Column failed to moved to the left.\n"
      fail "FAILED - The Column failed to moved to the left.\n"
      #@reporter.ReportAction("FAILED - The Column failed to moved to the right.\n")
    end
  end
  def VerifyGridDataDealStatus(gridData,status)
    @GData = gridData
    @NeededStatus = status
    # print "\n"
    # print @GData[16]
    # print "\n"
    # print @GData[25]
    # print "\n"
    # print @GData[34]
    # print "\n"
    # print @GData[43]
    # print "\n"
    # print @GData[52]
    # print "\n"
    # print @GData[61]
    # print "\n"
    #@NeededData = Array.new
    #@NeededData = @GData
    #@rowcount = 0
    ##@colcount = 1
    #print @GData
    #@GData.each do |row|
    #  row.each do |cell|
    #    @NeededData.push(row)
    #    print "\n"
    #    print cell
    #    print "\n"
    #    print "Value at [" + @rowcount.to_s + ","+ @colcount.to_s + "]: "+ cell.to_s+ "\n"#[j]
    #    @colcount = @colcount + 1
    #  end
    #  @rowcount = @rowcount + 1
    #  @colcount = 0
    #end
    # print "\n"
    # print @GData
    # print "\n"
    # print @GData[[15,0]]
    # print "\n"
    # print @GData[[24,0]]
    # print "\n"
    # print @GData[[33,0]]
    # print "\n"
    # print @GData[[42,0]]
    # print "\n"
    # print @GData[[51,0]]
    # print "\n"
    # print @GData[[60,0]]
    # print "\n"
    # print @GData[[7,1]]
    # print "\n"
    # print @GData[[20,1]]
    # print "\n"
    # print @GData[[33,1]]
    # print "\n"
    # print @GData[[46,1]]
    # print "\n"
    # print @GData[[59,1]]
    # print "\n"
    # print @GData[[72,1]]
    # print "\n"
    # @entry1value = @GData[[7,1]].to_s
    # @entry2value = @GData[[20,1]].to_s
    # @entry3value = @GData[[33,1]].to_s
    # @entry4value = @GData[[46,1]].to_s
    # @entry5value = @GData[[59,1]].to_s
    # @entry6value = @GData[[72,1]].to_s
    # @entry1value = @GData[[15,0]].to_s
    # @entry2value = @GData[[24,0]].to_s
    # @entry3value = @GData[[33,0]].to_s
    # @entry4value = @GData[[42,0]].to_s
    # @entry5value = @GData[[51,0]].to_s
    # @entry6value = @GData[[60,0]].to_s
    @entry1value = @GData[[1,6]].to_s
    @entry2value = @GData[[2,6]].to_s
    @entry3value = @GData[[3,6]].to_s
    @entry4value = @GData[[4,6]].to_s
    @entry5value = @GData[[5,6]].to_s
    @entry6value = @GData[[6,6]].to_s

    @entry1verify = @NeededStatus.include? @entry1value#@GData[[15,0]].to_s
    @entry2verify = @NeededStatus.include? @entry2value #@GData[[24,0]].to_s
    @entry3verify = @NeededStatus.include? @entry3value #@GData[[33,0]].to_s
    @entry4verify = @NeededStatus.include? @entry4value #@GData[[42,0]].to_s
    @entry5verify = @NeededStatus.include? @entry5value #@GData[[51,0]].to_s
    @entry6verify = @NeededStatus.include? @entry6value

    if @NeededStatus == "In Progress"
      @NeededStatus = ["Under Review","Authorize", "Outstanding Quote", "To Be Declined", "Bound Pending Data Entry"]
      #@GData[[60,0]].to_s
      if @entry1verify.to_s == "true" && @entry2verify.to_s == "true" && @entry3verify.to_s == "true" && @entry4verify.to_s == "true" && @entry5verify.to_s == "true" && @entry6verify.to_s == "true"
        @Statustoprint = "In Progress"
        print "PASSED - The Deals in the grid are of " + @Statustoprint + " " + status + " substatus.\n"
        #  #@reporter.ReportAction("PASSED - The Column is successfully moved to the right.\n")
      else
        @Statustoprint = "In Progress"
        print "FAILED - The Deals in the grid are not of " + @Statustoprint + " " + status + " substatus.\n"
        fail "FAILED - The Deals in the grid are not of " + @Statustoprint + " " + status + " substatus.\n"
        #  #@reporter.ReportAction("FAILED - The Column failed to moved to the right.\n")
      end
    elsif @NeededStatus == "Renewable - 6 Months"
      @NeededStatus = ["Bound"]
      #@GData[[60,0]].to_s
      if @entry1verify.to_s == "true" && @entry2verify.to_s == "true" && @entry3verify.to_s == "true" && @entry4verify.to_s == "true" && @entry5verify.to_s == "true" && @entry6verify.to_s == "true"
        @Statustoprint = "Renewable - 6 Months"
        print "PASSED - The Deals in the grid are of " + @Statustoprint + " " + status + " substatus.\n"
        #  #@reporter.ReportAction("PASSED - The Column is successfully moved to the right.\n")
      else
        @Statustoprint = "Renewable - 6 Months"
        print "FAILED - The Deals in the grid are not of " + @Statustoprint + " " + status + " substatus.\n"
        fail "FAILED - The Deals in the grid are not of " + @Statustoprint + " " + status + " substatus.\n"
        #  #@reporter.ReportAction("FAILED - The Column failed to moved to the right.\n")
      end
    else
      for @ival in 0..11
        if @GData[[0,@ival]].to_s == "Status"
          @entry1value = @GData[[1,@ival]].to_s
          @entry2value = @GData[[2,@ival]].to_s
          @entry3value = @GData[[3,@ival]].to_s
          @entry4value = @GData[[4,@ival]].to_s
          @entry5value = @GData[[5,@ival]].to_s
          @entry6value = @GData[[6,@ival]].to_s
          if @entry1value.to_s == @NeededStatus && @entry2value.to_s == @NeededStatus && @entry3value.to_s == @NeededStatus && @entry4value.to_s == @NeededStatus && @entry5value.to_s == @NeededStatus && @entry6value.to_s == @NeededStatus
            print "PASSED - The Deals in the grid are of " + @NeededStatus + " status.\n"
            #  #@reporter.ReportAction("PASSED - The Column is successfully moved to the right.\n")
          else
            print "FAILED - The Deals in the grid are not of " + @NeededStatus + " status.\n"
            fail "FAILED - The Deals in the grid are not of " + @NeededStatus + " status.\n"
            #  #@reporter.ReportAction("FAILED - The Column failed to moved to the right.\n")
          end
          break # Breaking the loop
        end
      end


    end
  end
  def verifyElementExist(quickViewElement)
    @ElementToBeVerified = quickViewElement
    @ElementExist = @ElementToBeVerified.exist?
    if @ElementExist == true
      print "PASSED - The Element " + @ElementToBeVerified.text + " is visible in the screen.\n"
    else
      print "FAILED - The Element is not visible in the screen.\n"
      fail "FAILED - The Element is not visible in the screen.\n"
    end
  end
  def VerifyQlikView()
    #@nbrowser = newbrowser
    #@newbrowserurl = @nbrowser.url
    #print "\n"
    #print @browser.window(:index => 1).title #:url => /global re dashboard/).url
    #print "\n"
    # @browser.window(:index => 2).url.include? "global%20re%20dashboard")
    # @browser.windows(:title => "global re dashborad.qvw - Internet Explorer").use do
    #  @browser.window(1)
    # @browser.windows.last.use do
    #   sleep 2
    #   puts @brwser.url
    # end
    sleep 5 #Adding this wait as it not getting Page URL
    if (@browser.window(:index => 1).url.include? "global%20re%20dashboard")
      print "PASSED - The QlikView with the URL " + @browser.window(:index => 1).url + " is successfully opened in a new browser.\n"
    else
      print "FAILED - The QlikView failed to open.\n"
      fail "FAILED - The QlikView failed to open.\n"
      end
  # end
end
  #def VerifyERMS(newbrowser)
  #  @nbrowser = newbrowser
  #  @newbrowserurl = @nbrowser.url
  #  if @newbrowserurl.contains("erms:ERMSHome\ERMSHome.Shell.exe alias=ONEDEV") == true
  #    print "PASSED - The ERMS is successfully launched.\n"
  #  else
  #    print "FAILED - The QlikView failed to open.\n"
  #  end
  #end
  def ClickMenuBtn(celement)
    @clickedelement = celement
    begin
      sleep 1
      @clickedelement.click

      if @browser.alert.exists?
        #@browser.alert.ok
        @wsh = WIN32OLE.new('Wscript.Shell')
        @wsh.SendKeys('sjanardanannair')
        @wsh.SendKeys('{TAB}')
        @wsh.SendKeys('Summer18')
        @wsh.SendKeys('{TAB}')

        @wsh.SendKeys('{ENTER}')
        #@browser.alert.ok

      end
        #@browser.alert.wait_until_present
    rescue Exception => e
      puts "Trapped Error, expecting modal dialog exception"
      puts e.backtrace
      puts "Continuing"
    end
    #@newbrowserwindow = @browser.window(:url => /va1-tcorapp087 /)
    #print "\n"
    #print @browser.alert.exists?
    #print "\n"

  end

  def ClickDealEditScreenBtn(user, celement)
    @clickedelement = celement
    if user == "Read Only" && celement.enabled? == "false"
      print "The Submit button is in disabled status as it is a #{user} access user"
    else
      begin
        @clickedelement.click
      rescue Exception => e
        puts "Trapped Error, expecting modal dialog exception"
        puts e.backtrace
        puts "Continuing"
      end
    end
  end
  def handleQlikviewAuthPopup
    #@newbrowserwindow = @browser.window(:url => /va1-tcorapp087 /)
    #if @newbrowserwindow.alert.exists?
    #  @wsh = WIN32OLE.new('Wscript.Shell')
    #  @wsh.SendKeys('sjanardanannair')
    #  @wsh.SendKeys('{TAB}')
    #  @wsh.SendKeys('Summer18')
    #  @browser.alert.ok
    #end
  end

  def verifyToolMenuAvailability
    @homepage_ToolMenuBody = fetchToolMenuBodyelement
    @homepage_ToolMenuHeader = fetchToolMenuHeaderelement
    if @homepage_ToolMenuHeader.exist? == true && @homepage_ToolMenuBody.exist? == true
      print "PASSED - The Tool Menu is displayed successfully.\n"
    else
      print "FAILED - The Tool Menu is not displayed.\n"
      fail "FAILED - The Tool Menu is not displayed.\n"
    end
  end
  def ClickAgToolMenuColumnButton(toolMenuHeaderTabElement)
    @ToolMenuHeaderTabElement = toolMenuHeaderTabElement
    #@ToolMenuHeaderColumntabElement = @ToolMenuHeaderElement.span(:class => 'ag-tab ag-tab-selected')
    @ToolMenuHeaderTabElement.click
  end
  def verifyElementExistonPage(elementtobChecked,elementName)
    @ElementexisttobeChecked = elementtobChecked
    @ElementName = elementName
    if @ElementexisttobeChecked.exist? == true
      print "PASSED - The " + @ElementName + " is displayed successfully.\n"
    else
      print "FAILED - The " + @ElementName + " is not displayed.\n"
      fail "FAILED - The " + @ElementName + " is not displayed.\n"
    end
  end
  def verifyElementDoesNotExistonPage(elementtobChecked,elementName)
    @ElementexisttobeChecked = elementtobChecked
    @ElementName = elementName
    if @ElementexisttobeChecked.exist? == true
      print "FAILED - The " + @ElementName + " is still displayed.\n"
      fail "FAILED - The " + @ElementName + " is still displayed.\n"
    else
      print "PASSED - The " + @ElementName + " is not displayed.\n"
    end
  end
  def verifyElementIsReadOnly(elementtobChecked,elementName)
    @ElementexisttobeChecked = elementtobChecked
    @ElementName = elementName
    sleep 2
    if @ElementexisttobeChecked.exist? == true
      print "Passed " + @ElementName + " is Read only.\n"
      return true
    else
      print "PASSED - The " + @ElementName + " is not read only.\n"
      return false
    end
  end
  def inputTextField(element,inputText)
    @ElementexisttobeChecked = element
    @inPut = inputText
    sleep 2
    if @ElementexisttobeChecked.exist? == true
      @ElementexisttobeChecked.set(@inPut)
      print "Passed - Text  " + @inPut + " is placed in input field.\n"
    else
      print "Failed - Text : " + @inPut + "is not placed in input field.\n"
      fail "Failed - Text : " + @inPut + " is not placed in input field.\n"
    end
  end
  def VerifyElementText(elementtobChecked,wording)
    @EleTobeVerified = elementtobChecked
    @wordingexpected = wording
    #print @EleTobeVerified.text
    if @EleTobeVerified.text ==  @wordingexpected
      print "PASSED - The " + @wordingexpected + " element is available in the screen with the wording " + @wordingexpected + ".\n"
    else
      print "FAILED - The " + @wordingexpected + " element is not available in the screen with the wording " + @wordingexpected + ".\n"
      fail "FAILED - The " + @wordingexpected + " element is not available in the screen with the wording " + @wordingexpected + ".\n"
    end
  end


  def getpanelCountElement(substatusElement)
    @Elementtogetcount = substatusElement
    @CountElement = @Elementtogetcount.parent
    return @CountElement
  end
  def getsubstatusCount(panelelement)
    @PElement = panelelement
    @PText = Array.new
    @PText = panelelement.text.split("\n")
    @PcountTest = @PText[1]
    return @PcountTest
    #print "/n"
    #print @PElement.text
    #print "/n"
  end

  def verifyZerocountandDisabled(substatus,panelcount,panelelement)#,panelinputelement)
    @status = substatus
    @PCount = panelcount
    @PElement = panelelement
    if @PCount == 0 && @PElement.attribute_value('disabled') == true
      print "PASSED - The " + @PElement.text + " status panel is having count " + @PCount + " and is in disabled status.\n"
    elsif @PCount == 0 && @PElement.attribute_value('disabled') == false
      print "FAILED - The " + @PElement.text + " status panel is having count " + @PCount + " and is not in disabled status.\n"
      fail "FAILED - The " + @PElement.text + " status panel is having count " + @PCount + " and is not in disabled status.\n"
    else
      print "PASSED - The " + @PElement.text + " status panel is having count " + @PCount + " and is in enabled status.\n"
    end
    #@Pinputelement = panelinputelement
  end
  def verifyZerocountandUnSelected(substatus,panelcount,panelelement,panelinputelement)
    @status = substatus
    @PCount = panelcount
    @PElement = panelelement
    @Pinputelement = panelinputelement
    if @PCount == 0 && @Pinputelement.attribute_value('checked').to_s == "false" #@PElement.attribute_value('disabled') == true
      print "PASSED - The " + @PElement.text + " status panel is having count " + @PCount + " and is unselected.\n"
    elsif @PCount == 0 && @Pinputelement.attribute_value('checked').to_s == "true"
      print "FAILED - The " + @PElement.text + " status panel is having count " + @PCount + " and is not unselected.\n"
      fail "FAILED - The " + @PElement.text + " status panel is having count " + @PCount + " and is not unselected.\n"
    else
      print "PASSED - The " + @PElement.text + " status panel is having count " + @PCount + " and is selected.\n"
    end
  end
  def unselectallsubstatusCheckbox
    @underreview_CheckBox = fetchInprogress_UR_CHKBX_ElementInput
    @authorize_CheckBox = fetchInprogress_Authorize_CHKBX_ElementInput
    @outstandingquote_CheckBox = fetchInprogress_OQ_CHKBX_ElementInput
    @tobedeclined_CheckBox = fetchInprogress_TBD_CHKBX_ElementInput
    @boundpendingdataentry_CheckBox = fetchInprogress_BPDE_CHKBX_ElementInput

    @underreview_CheckBoxClick = fetchInprogress_UR_CHKBX_Element
    @authorize_CheckBoxClick = fetchInprogress_Authorize_CHKBX_Element
    @outstandingquote_CheckBoxClick = fetchInprogress_OQ_CHKBX_Element
    @tobedeclined_CheckBoxClick = fetchInprogress_TBD_CHKBX_Element
    @boundpendingdataentry_CheckBoxClick = fetchInprogress_BPDE_CHKBX_Element
    #print "\n"
    #print @underreview_CheckBox.attribute_value('checked')
    #print "\n"
    Watir::Wait.until { @underreview_CheckBox.visible? }
    if @underreview_CheckBox.attribute_value('checked').to_s == "true"
      sleep 3
      ClickBtn(@underreview_CheckBoxClick)
    end
    Watir::Wait.until { @authorize_CheckBox.visible? }
    if @authorize_CheckBox.attribute_value('checked').to_s == "true"
      sleep 3
      ClickBtn(@authorize_CheckBoxClick)
    end
    Watir::Wait.until { @outstandingquote_CheckBox.visible? }
    if @outstandingquote_CheckBox.attribute_value('checked').to_s == "true"
      sleep 3
      ClickBtn(@outstandingquote_CheckBoxClick)
    end
    Watir::Wait.until { @tobedeclined_CheckBox.visible? }
    if @tobedeclined_CheckBox.attribute_value('checked').to_s == "true"
      sleep 3
      ClickBtn(@tobedeclined_CheckBoxClick)
    end
    Watir::Wait.until { @boundpendingdataentry_CheckBox.visible? }
    if @boundpendingdataentry_CheckBox.attribute_value('checked').to_s == "true"
      sleep 3
      ClickBtn(@boundpendingdataentry_CheckBoxClick)
    end
  end


  def uncheckallsubstatusCheckbox(substatus,status)
    # print "\n"
    # print substatus
    # print "\n"
    # print status
    # print "\n"
    @neededstatusElement = fetchpanelElement(status)
    @StatusButtonDisabled = @neededstatusElement.attribute_value('className').include? "disabled"
    if @StatusButtonDisabled.to_s != "true" && substatus.to_s != "" && substatus.to_s != "nil"
      # print "\n"
      # print substatus
      @NeededSubstatus = substatus.to_s
      #@NeededSubstatusArr = Array.new
      @NeededSubstatusArr = @NeededSubstatus.split(",").map { |s| s.to_s}
      # print "\n"
      # print @NeededSubstatusArr[0]
      # print "\n"
      @NeededSubstatusArr.delete(nil)
      for @count in 0..@NeededSubstatusArr.length-1
        @NeededCheckboxInputElement = fetchpanelSubStatusElementInput(@NeededSubstatusArr[@count])
        #Watir::Wait.until { @NeededCheckboxInputElement.visible? }
        if @NeededCheckboxInputElement.attribute_value('checked').to_s == "true"
          ClickBtn(@NeededCheckboxInputElement)
        end
      end
    end

    # @underreview_CheckBox = fetchInprogress_UR_CHKBX_ElementInput
    # @authorize_CheckBox = fetchInprogress_Authorize_CHKBX_ElementInput
    # @outstandingquote_CheckBox = fetchInprogress_OQ_CHKBX_ElementInput
    # @tobedeclined_CheckBox = fetchInprogress_TBD_CHKBX_ElementInput
    # @boundpendingdataentry_CheckBox = fetchInprogress_BPDE_CHKBX_ElementInput
    #print "\n"
    #print @underreview_CheckBox.attribute_value('checked')
    #print "\n"
    # Watir::Wait.until { @underreview_CheckBox.visible? }
    # if @underreview_CheckBox.attribute_value('checked').to_s == "true"
    #   ClickBtn(@underreview_CheckBox)
    # end
  end
  def VerifyGridDataMultipleDealStatus(gridData,status,substatus1,substatus2)
    @GData = gridData
    @NeededStatus = status
    @NeededStatus1 = substatus1
    @NeededStatus2 = substatus2
    # print "\n"
    # print @GData
    # print "\n"
    if @NeededStatus1.to_s == "In Progress"
      @NeededStatus1 = ["Under Review","Authorize", "Outstanding Quote", "To Be Declined", "Bound Pending Data Entry"]
    end
    if @NeededStatus2.to_s == "In Progress"
      @NeededStatus2 = ["Under Review","Authorize", "Outstanding Quote", "To Be Declined", "Bound Pending Data Entry"]
    end
    if @NeededStatus1.to_s == "Renewable - 6 Months"
      @NeededStatus1 = ["Bound"]
    end
    if @NeededStatus2.to_s == "Renewable - 6 Months"
      @NeededStatus2 = ["Bound"]
    end


    # @entry1value = @GData[[7,1]].to_s
    # @entry2value = @GData[[20,1]].to_s
    # @entry3value = @GData[[33,1]].to_s
    # @entry4value = @GData[[46,1]].to_s
    # @entry5value = @GData[[59,1]].to_s
    # @entry6value = @GData[[72,1]].to_s
    # print "\n"
    # print @GData[[15,0]].to_s
    # print "\n"
    # print @GData[[24,0]].to_s
    # print "\n"
    # print @GData[[33,0]].to_s
    # print "\n"
    # print @GData[[42,0]].to_s
    # print "\n"
    # print @GData[[51,0]].to_s
    # print "\n"
    # print @GData[[60,0]].to_s
    # print "\n"
    # @entry1value = @GData[[15,0]].to_s
    # @entry2value = @GData[[24,0]].to_s
    # @entry3value = @GData[[33,0]].to_s
    # @entry4value = @GData[[42,0]].to_s
    # @entry5value = @GData[[51,0]].to_s
    # @entry6value = @GData[[60,0]].to_s
    @entry1value = @GData[[1,6]].to_s
    @entry2value = @GData[[2,6]].to_s
    @entry3value = @GData[[3,6]].to_s
    @entry4value = @GData[[4,6]].to_s
    @entry5value = @GData[[5,6]].to_s
    @entry6value = @GData[[6,6]].to_s
    @entry1valueNS1Available = @NeededStatus1.include? @entry1value
    @entry2valueNS1Available = @NeededStatus1.include? @entry2value
    @entry3valueNS1Available = @NeededStatus1.include? @entry3value
    @entry4valueNS1Available = @NeededStatus1.include? @entry4value
    @entry5valueNS1Available = @NeededStatus1.include? @entry5value
    @entry6valueNS1Available = @NeededStatus1.include? @entry6value
    @entry1valueNS2Available = @NeededStatus2.include? @entry1value
    @entry2valueNS2Available = @NeededStatus2.include? @entry2value
    @entry3valueNS2Available = @NeededStatus2.include? @entry3value
    @entry4valueNS2Available = @NeededStatus2.include? @entry4value
    @entry5valueNS2Available = @NeededStatus2.include? @entry5value
    @entry6valueNS2Available = @NeededStatus2.include? @entry6value

    # print "\n"
    # print @entry1valueNS1Available
    # print "\n"
    # print @entry1valueNS2Available
    # print "\n"
    # print @entry2valueNS1Available
    # print "\n"
    # print @entry2valueNS2Available
    # print "\n"
    # print @entry3valueNS1Available
    # print "\n"
    # print @entry3valueNS2Available
    # print "\n"
    # print @entry4valueNS1Available
    # print "\n"
    # print @entry4valueNS2Available
    # print "\n"
    # print @entry5valueNS1Available
    # print "\n"
    # print @entry5valueNS2Available
    # print "\n"
    # print @entry6valueNS1Available
    # print "\n"
    # print @entry6valueNS2Available
    # print "\n"


    if (@entry1valueNS1Available.to_s == "true" || @entry1valueNS2Available.to_s == "true")  && (@entry2valueNS1Available.to_s == "true" || @entry2valueNS2Available.to_s == "true") && (@entry3valueNS1Available.to_s == "true" || @entry3valueNS2Available.to_s == "true") && (@entry4valueNS1Available.to_s == "true" || @entry4valueNS2Available.to_s == "true") && (@entry5valueNS1Available.to_s == "true" || @entry5valueNS2Available.to_s == "true") && (@entry6valueNS1Available.to_s == "true" || @entry6valueNS2Available.to_s == "true")
      print "PASSED - The Deals in the grid displayed on selecting the " + status.to_s + " panel are of " + substatus1.to_s + " or " + substatus2.to_s + " status.\n"
      #  #@reporter.ReportAction("PASSED - The Column is successfully moved to the right.\n")
    else
      print "FAILED - The Deals in the grid displayed on selecting the " + status.to_s + " panel are not of " + substatus1.to_s + " or " + substatus2.to_s + " status.\n"
      fail "FAILED - The Deals in the grid displayed on selecting the " + status.to_s + " panel are not of " + substatus1.to_s + " or " + substatus2.to_s + " status.\n"
      #  #@reporter.ReportAction("FAILED - The Column failed to moved to the right.\n")
    end


    # if (@entry1value == @NeededStatus1 || @entry1value == @NeededStatus2)  && (@entry2value == @NeededStatus1 || @entry2value == @NeededStatus2) && (@entry3value == @NeededStatus1 || @entry3value == @NeededStatus2) && (@entry4value == @NeededStatus1 || @entry4value == @NeededStatus2) && (@entry5value == @NeededStatus1 || @entry5value == @NeededStatus2) && (@entry6value == @NeededStatus1 || @entry6value == @NeededStatus2)
    #   print "PASSED - The Deals in the grid displayed on selecting the " + @NeededStatus + " panel are of " + @NeededStatus1 + " or " + @NeededStatus2 + " status.\n"
    #   #  #@reporter.ReportAction("PASSED - The Column is successfully moved to the right.\n")
    # else
    #   print "FAILED - The Deals in the grid displayed on selecting the " + @NeededStatus + " panel are not of " + @NeededStatus1 + " or " + @NeededStatus2 + " status.\n"
    #   #  #@reporter.ReportAction("FAILED - The Column failed to moved to the right.\n")
    # end
  end
  def VerifyGridDataIsOfUser(gridData,user,status)
    @GData = gridData
    @NeededUser = user
    # print "\n"
    # print @GData
    # print "\n"
    @NeededUWUserFullName = "Mike McCarthy"
    @NeededNPTAUserFullName = "Rhonda Corbin"
    @NeededActuaryUserFullName = "Laurie Slader"
    @NeededActuaryManagerTeam = ["Todd Glassman","Bruce Stocker","Ernie Tistan","Kari Falcone","Laurie Slader","Richard Millilo","Steve Meyer"]
    @NeededUWManagerTeam = ["Andrew Barnard", "Crystal Doughty","James Welsby","John Duda", "Benedict Parfit","Marlon Williams"]
    case @NeededUser
      when "All Access"
        print "Verification Not Required as it is a " + user + " user.\n"
      when "UW"
        verifyGridDataisOfUWUser(@GData,@NeededUWUserFullName)
      when "NPTA"
        verifyGridDataisOfNPTAUser(@GData,@NeededNPTAUserFullName)
      when "PTA"
        @dbquery = DBQueries.new
        @sqlquery=@dbquery.getUserDBQuery(status,user)
        # print "\n"
        # print @sqlquery
        # print "\n"
        @dbresult = sendQuery(@sqlquery.to_s)
        @ModifiedDBResult = modifyDBResult(@dbresult)
        @GridDataModified = ModifyAllGridCellData(@GData)
        # print "\n"
        # print @GridDataModified
        # print "\n"
        # print @ModifiedDBResult
        # print "\n"
        @compareRes = CompareResults(@GridDataModified,@ModifiedDBResult)
        if @compareRes == 1
          print "PASSED - The UI and DB Data matched successfully and the data displayed is that of Property TA user.\n"
        else
          print "FAILED - The UI and DB Data failed to get matched and the data displayed is not that of Property TA user.\n"
        end
      when "Actuary"
        verifyGridDataisOfActuaryUser(@GData,@NeededActuaryUserFullName)
      when "UW Manager"
        #verifyGridDataisOfUWManagerTeam(@GData,@NeededUWManagerTeam)
        @dbquery = DBQueries.new
        @sqlquery=@dbquery.getUserDBQuery(status,user)
        # print "\n"
        # print @sqlquery
        # print "\n"
        @dbresult = sendQuery(@sqlquery.to_s)
        @ModifiedDBResult = modifyDBResult(@dbresult)
        @GridDataModified = ModifyAllGridCellData(@GData)
        # print "\n"
        # print @GridDataModified
        # print "\n"
        # print @ModifiedDBResult
        # print "\n"
        @compareRes = CompareResults(@GridDataModified,@ModifiedDBResult)
        if @compareRes == 1
          print "PASSED - The UI and DB Data matched successfully and the data displayed is that of UW Manager user.\n"
        else
          print "FAILED - The UI and DB Data failed to get matched and the data displayed is not that of UW Manager user.\n"
          fail "FAILED - The UI and DB Data failed to get matched and the data displayed is not that of UW Manager user.\n"
        end
      when "Actuary Manager"
        verifyGridDataisOfActuaryManagerTeam(@GData,@NeededActuaryManagerTeam)
    end
  end
  def verifyGridDataisOfUWUser(gData,user)
    @GridData = gData
    @NeededUWUser = user
    @colcount = 0
    @rowcount = 1
    @possibleRows = 6
    if @GridData[[@rowcount+6,@colcount]].to_s == "nil" || @GridData[[@rowcount+6,@colcount]].to_s == ""
      @possibleRows=6
    end
    if @GridData[[@rowcount+5,@colcount]].to_s == "nil" || @GridData[[@rowcount+5,@colcount]].to_s == ""
      @possibleRows=5
    end
    if @GridData[[@rowcount+4,@colcount]].to_s == "nil" || @GridData[[@rowcount+4,@colcount]].to_s == ""
      @possibleRows=4
    end
    if @GridData[[@rowcount+3,@colcount]].to_s == "nil" || @GridData[[@rowcount+3,@colcount]].to_s == ""
      @possibleRows=3
    end
    if @GridData[[@rowcount+2,@colcount]].to_s == "nil" || @GridData[[@rowcount+2,@colcount]].to_s == ""
      @possibleRows=2
    end
    if @GridData[[@rowcount+1,@colcount]].to_s == "nil" || @GridData[[@rowcount+1,@colcount]].to_s == ""
      @possibleRows=1
    end
    if @GridData[[@rowcount,@colcount]].to_s == "nil" || @GridData[[@rowcount,@colcount]].to_s == ""
      @possibleRows=0
    end
    print "Grid is displayed with " + @possibleRows.to_s + " deals.\n"
    # @entry1UW1value = @GridData[[9,1]].to_s
    # @entry1UW2value = @GridData[[10,1]].to_s
    # @entry2UW1value = @GridData[[22,1]].to_s
    # @entry2UW2value = @GridData[[23,1]].to_s
    # @entry3UW1value = @GridData[[35,1]].to_s
    # @entry3UW2value = @GridData[[36,1]].to_s
    # @entry4UW1value = @GridData[[48,1]].to_s
    # @entry4UW2value = @GridData[[49,1]].to_s
    # @entry5UW1value = @GridData[[61,1]].to_s
    # @entry5UW2value = @GridData[[62,1]].to_s
    # @entry6UW1value = @GridData[[87,1]].to_s
    # @entry6UW2value = @GridData[[88,1]].to_s
    @entry1UW1value = @GridData[[1,8]].to_s
    @entry1UW2value = @GridData[[1,9]].to_s
    @entry2UW1value = @GridData[[2,8]].to_s
    @entry2UW2value = @GridData[[2,9]].to_s
    @entry3UW1value = @GridData[[3,8]].to_s
    @entry3UW2value = @GridData[[3,9]].to_s
    @entry4UW1value = @GridData[[4,8]].to_s
    @entry4UW2value = @GridData[[4,9]].to_s
    @entry5UW1value = @GridData[[5,8]].to_s
    @entry5UW2value = @GridData[[5,9]].to_s
    @entry6UW1value = @GridData[[6,8]].to_s
    @entry6UW2value = @GridData[[6,9]].to_s

    # print "\n"
    # print @entry1UW1value
    # print "\n"
    # print @entry1UW2value
    # print "\n"
    # print @entry2UW1value
    # print "\n"
    # print @entry2UW2value
    # print "\n"
    # print @entry3UW1value
    # print "\n"
    # print @entry3UW2value
    # print "\n"
    # print @entry4UW1value
    # print "\n"
    # print @entry4UW2value
    # print "\n"
    # print @entry5UW1value
    # print "\n"
    # print @entry5UW2value
    # print "\n"
    # print @entry6UW1value
    # print "\n"
    # print @entry6UW2value
    # print "\n"



    if (@GridData[[1,1]].to_s == "nil" ||  @GridData[[1,1]].to_s == "") && (@GridData[[14,1]].to_s == "nil" ||  @GridData[[14,1]].to_s == "") && @possibleRows == 0
      print "The grid does not have any data. So skipping the field validation.\n"
    else
      if ((@entry1UW1value.to_s != "nil" || @entry1UW2value.to_s != "nil") || (@entry1UW1value.to_s != "" || @entry1UW2value.to_s != "")) && (@possibleRows >= 1)
        if @entry1UW1value.to_s == @NeededUWUser || @entry1UW2value.to_s == @NeededUWUser
          print "PASSED - The 1st Deal Entry in the Grid is having the name " + @NeededUWUser + " in the UnderWriter1 or UnderWriter2 name field.\n"
        else
          print "FAILED - The 1st Deal Entry in the Grid is not having the name " + @NeededUWUser + " in the UnderWriter1 or UnderWriter2 name field.\n"
          fail "FAILED - The 1st Deal Entry in the Grid is not having the name " + @NeededUWUser + " in the UnderWriter1 or UnderWriter2 name field.\n"
        end
      end
      if ((@entry2UW1value.to_s != "nil" || @entry2UW2value.to_s != "nil") || (@entry2UW1value.to_s != "" || @entry2UW2value.to_s != "")) && (@possibleRows >= 2)
        if @entry2UW1value.to_s == @NeededUWUser || @entry2UW2value.to_s == @NeededUWUser
          print "PASSED - The 2nd Deal Entry in the Grid is having the name " + @NeededUWUser + " in the UnderWriter1 or UnderWriter2 name field.\n"
        else
          print "FAILED - The 2nd Deal Entry in the Grid is not having the name " + @NeededUWUser + " in the UnderWriter1 or UnderWriter2 name field.\n"
          fail "FAILED - The 2nd Deal Entry in the Grid is not having the name " + @NeededUWUser + " in the UnderWriter1 or UnderWriter2 name field.\n"
        end
      end
      if ((@entry3UW1value.to_s != "nil" || @entry3UW2value.to_s != "nil") || (@entry3UW1value.to_s != "" || @entry3UW2value.to_s != "")) && (@possibleRows >= 3)
        if @entry3UW1value.to_s == @NeededUWUser || @entry3UW2value.to_s == @NeededUWUser
          print "PASSED - The 3rd Deal Entry in the Grid is having the name " + @NeededUWUser + " in the UnderWriter1 or UnderWriter2 name field.\n"
        else
          print "FAILED - The 3rd Deal Entry in the Grid is not having the name " + @NeededUWUser + " in the UnderWriter1 or UnderWriter2 name field.\n"
          fail "FAILED - The 3rd Deal Entry in the Grid is not having the name " + @NeededUWUser + " in the UnderWriter1 or UnderWriter2 name field.\n"
        end
      end
      if ((@entry4UW1value.to_s != "nil" || @entry4UW2value.to_s != "nil") || (@entry4UW1value.to_s != "" || @entry4UW2value.to_s != "")) && (@possibleRows >= 4)
        if @entry4UW1value.to_s == @NeededUWUser || @entry4UW2value.to_s == @NeededUWUser
          print "PASSED - The 4th Deal Entry in the Grid is having the name " + @NeededUWUser + " in the UnderWriter1 or UnderWriter2 name field.\n"
        else
          print "FAILED - The 4th Deal Entry in the Grid is not having the name " + @NeededUWUser + " in the UnderWriter1 or UnderWriter2 name field.\n"
          fail "FAILED - The 4th Deal Entry in the Grid is not having the name " + @NeededUWUser + " in the UnderWriter1 or UnderWriter2 name field.\n"
        end
      end
      if ((@entry5UW1value.to_s != "nil" || @entry5UW2value.to_s != "nil") || (@entry5UW1value.to_s != "" || @entry5UW2value.to_s != "")) && (@possibleRows >= 5)
        if @entry5UW1value.to_s == @NeededUWUser || @entry5UW2value.to_s == @NeededUWUser
          print "PASSED - The 5th Deal Entry in the Grid is having the name " + @NeededUWUser + " in the UnderWriter1 or UnderWriter2 name field.\n"
        else
          print "FAILED - The 5th Deal Entry in the Grid is not having the name " + @NeededUWUser + " in the UnderWriter1 or UnderWriter2 name field.\n"
          fail "FAILED - The 5th Deal Entry in the Grid is not having the name " + @NeededUWUser + " in the UnderWriter1 or UnderWriter2 name field.\n"
        end
      end
      if ((@entry6UW1value.to_s != "nil" || @entry6UW2value.to_s != "nil") || (@entry6UW1value.to_s != "" || @entry6UW2value.to_s != "")) && (@possibleRows >= 6)
        if @entry6UW1value.to_s == @NeededUWUser || @entry6UW2value.to_s == @NeededUWUser
          print "PASSED - The 6th Deal Entry in the Grid is having the name " + @NeededUWUser + " in the UnderWriter1 or UnderWriter2 name field.\n"
        else
          print "FAILED - The 6th Deal Entry in the Grid is not having the name " + @NeededUWUser + " in the UnderWriter1 or UnderWriter2 name field.\n"
          fail "FAILED - The 6th Deal Entry in the Grid is not having the name " + @NeededUWUser + " in the UnderWriter1 or UnderWriter2 name field.\n"
        end
      end
    end
  end
  def verifyGridDataisOfNPTAUser(gData,user)
    @GridData = gData
    # print "\n"
    # print @GridData
    # print "\n"
    @NeededUWUser = user
    @possibleRows = 6
    # @colcount = 1
    # @rowcount = 1
    # if @GridData[[@rowcount+78,@colcount]].to_s == "nil" || @GridData[[@rowcount+78,@colcount]].to_s == ""
    #   @possibleRows=6
    # end
    # if @GridData[[@rowcount+65,@colcount]].to_s == "nil" || @GridData[[@rowcount+65,@colcount]].to_s == ""
    #   @possibleRows=5
    # end
    # if @GridData[[@rowcount+52,@colcount]].to_s == "nil" || @GridData[[@rowcount+52,@colcount]].to_s == ""
    #   @possibleRows=4
    # end
    # if @GridData[[@rowcount+39,@colcount]].to_s == "nil" || @GridData[[@rowcount+39,@colcount]].to_s == ""
    #   @possibleRows=3
    # end
    # if @GridData[[@rowcount+26,@colcount]].to_s == "nil" || @GridData[[@rowcount+26,@colcount]].to_s == ""
    #   @possibleRows=2
    # end
    # if @GridData[[@rowcount+13,@colcount]].to_s == "nil" || @GridData[[@rowcount+13,@colcount]].to_s == ""
    #   @possibleRows=1
    # end
    # if @GridData[[@rowcount,@colcount]].to_s == "nil" || @GridData[[@rowcount,@colcount]].to_s == ""
    #   @possibleRows=0
    # end
    @colcount = 0
    @rowcount = 1
    if @GridData[[@rowcount+6,@colcount]].to_s == "nil" || @GridData[[@rowcount+6,@colcount]].to_s == ""
      @possibleRows=6
    end
    if @GridData[[@rowcount+5,@colcount]].to_s == "nil" || @GridData[[@rowcount+5,@colcount]].to_s == ""
      @possibleRows=5
    end
    if @GridData[[@rowcount+4,@colcount]].to_s == "nil" || @GridData[[@rowcount+4,@colcount]].to_s == ""
      @possibleRows=4
    end
    if @GridData[[@rowcount+3,@colcount]].to_s == "nil" || @GridData[[@rowcount+3,@colcount]].to_s == ""
      @possibleRows=3
    end
    if @GridData[[@rowcount+2,@colcount]].to_s == "nil" || @GridData[[@rowcount+2,@colcount]].to_s == ""
      @possibleRows=2
    end
    if @GridData[[@rowcount+1,@colcount]].to_s == "nil" || @GridData[[@rowcount+1,@colcount]].to_s == ""
      @possibleRows=1
    end
    if @GridData[[@rowcount,@colcount]].to_s == "nil" || @GridData[[@rowcount,@colcount]].to_s == ""
      @possibleRows=0
    end
    print "Grid is displayed with " + @possibleRows.to_s + " deals.\n"
    @entry1TAvalue = @GridData[[1,10]].to_s
    @entry2TAvalue = @GridData[[2,10]].to_s
    @entry3TAvalue = @GridData[[3,10]].to_s
    @entry4TAvalue = @GridData[[4,10]].to_s
    @entry5TAvalue = @GridData[[5,10]].to_s
    @entry6TAvalue = @GridData[[6,10]].to_s


    if (@GridData[[1,1]].to_s == "nil" ||  @GridData[[1,1]].to_s == "") && (@GridData[[14,1]].to_s == "nil" ||  @GridData[[14,1]].to_s == "") && (@possibleRows == 0)
      print "The grid does not have any data. So skipping the field validation.\n"
    else
      if (@entry1TAvalue.to_s != "nil" || @entry1TAvalue.to_s != "") && (@possibleRows >= 1)
        if @entry1TAvalue.to_s == @NeededUWUser
          print "PASSED - The 1st Deal Entry in the Grid is having the name " + @NeededUWUser + " in the TA name field.\n"
        else
          print "FAILED - The 1st Deal Entry in the Grid is not having the name " + @NeededUWUser + " in the TA name field.\n"
          fail "FAILED - The 1st Deal Entry in the Grid is not having the name " + @NeededUWUser + " in the TA name field.\n"
        end
      end
      if (@entry2TAvalue.to_s != "nil" || @entry2TAvalue.to_s != "") && (@possibleRows >= 2)
        if @entry2TAvalue.to_s == @NeededUWUser
          print "PASSED - The 2nd Deal Entry in the Grid is having the name " + @NeededUWUser + " in the TA name field.\n"
        else
          print "FAILED - The 2nd Deal Entry in the Grid is not having the name " + @NeededUWUser + " in the TA name field.\n"
          fail "FAILED - The 2nd Deal Entry in the Grid is not having the name " + @NeededUWUser + " in the TA name field.\n"
        end
      end
      if (@entry3TAvalue.to_s != "nil" || @entry3TAvalue.to_s != "") && (@possibleRows >= 3)
        if @entry3TAvalue.to_s == @NeededUWUser
          print "PASSED - The 3rd Deal Entry in the Grid is having the name " + @NeededUWUser + " in the TA name field.\n"
        else
          print "FAILED - The 3rd Deal Entry in the Grid is not having the name " + @NeededUWUser + " in the TA name field.\n"
          fail "FAILED - The 3rd Deal Entry in the Grid is not having the name " + @NeededUWUser + " in the TA name field.\n"
        end
      end
      if (@entry4TAvalue.to_s != "nil" || @entry4TAvalue.to_s != "") && (@possibleRows >= 4)
        if @entry4TAvalue.to_s == @NeededUWUser
          print "PASSED - The 4th Deal Entry in the Grid is having the name " + @NeededUWUser + " in the TA name field.\n"
        else
          print "FAILED - The 4th Deal Entry in the Grid is not having the name " + @NeededUWUser + " in the TA name field.\n"
          fail "FAILED - The 4th Deal Entry in the Grid is not having the name " + @NeededUWUser + " in the TA name field.\n"
        end
      end
      if (@entry5TAvalue.to_s != "nil" || @entry5TAvalue.to_s != "") && (@possibleRows >= 5)
        if @entry5TAvalue.to_s == @NeededUWUser
          print "PASSED - The 5th Deal Entry in the Grid is having the name " + @NeededUWUser + " in the TA name field.\n"
        else
          print "FAILED - The 5th Deal Entry in the Grid is not having the name " + @NeededUWUser + " in the TA name field.\n"
          fail "FAILED - The 5th Deal Entry in the Grid is not having the name " + @NeededUWUser + " in the TA name field.\n"
        end
      end
      if (@entry6TAvalue.to_s != "nil" || @entry6TAvalue.to_s != "") && (@possibleRows >= 6)
        if @entry6TAvalue.to_s == @NeededUWUser
          print "PASSED - The 6th Deal Entry in the Grid is having the name " + @NeededUWUser + " in the TA name field.\n"
        else
          print "FAILED - The 6th Deal Entry in the Grid is not having the name " + @NeededUWUser + " in the TA name field.\n"
          fail "FAILED - The 6th Deal Entry in the Grid is not having the name " + @NeededUWUser + " in the TA name field.\n"
        end
      end
    end

  end
  def verifyGridDataisOfActuaryUser(gData,user)
    @GridData = gData
    # print "\n"
    # print @GridData
    # print "\n"
    @NeededUser = user
    @possibleRows = 6
    @colcount = 0
    @rowcount = 1
    if @GridData[[@rowcount+6,@colcount]].to_s == "nil" || @GridData[[@rowcount+6,@colcount]].to_s == ""
      @possibleRows=6
    end
    if @GridData[[@rowcount+5,@colcount]].to_s == "nil" || @GridData[[@rowcount+5,@colcount]].to_s == ""
      @possibleRows=5
    end
    if @GridData[[@rowcount+4,@colcount]].to_s == "nil" || @GridData[[@rowcount+4,@colcount]].to_s == ""
      @possibleRows=4
    end
    if @GridData[[@rowcount+3,@colcount]].to_s == "nil" || @GridData[[@rowcount+3,@colcount]].to_s == ""
      @possibleRows=3
    end
    if @GridData[[@rowcount+2,@colcount]].to_s == "nil" || @GridData[[@rowcount+2,@colcount]].to_s == ""
      @possibleRows=2
    end
    if @GridData[[@rowcount+1,@colcount]].to_s == "nil" || @GridData[[@rowcount+1,@colcount]].to_s == ""
      @possibleRows=1
    end
    if @GridData[[@rowcount,@colcount]].to_s == "nil" || @GridData[[@rowcount,@colcount]].to_s == ""
      @possibleRows=0
    end
    print "Grid is displayed with " + @possibleRows.to_s + " deals.\n"
    @entry1Actuaryvalue = @GridData[[1,12]].to_s
    @entry2Actuaryvalue = @GridData[[2,12]].to_s
    @entry3Actuaryvalue = @GridData[[3,12]].to_s
    @entry4Actuaryvalue = @GridData[[4,12]].to_s
    @entry5Actuaryvalue = @GridData[[5,12]].to_s
    @entry6Actuaryvalue = @GridData[[6,12]].to_s
    # print "\n"
    # print @possibleRows
    # print "\n"


    if (@GridData[[1,1]].to_s == "nil" ||  @GridData[[1,1]].to_s == "") && (@GridData[[14,1]].to_s == "nil" ||  @GridData[[14,1]].to_s == "") && (@possibleRows == 0)
      print "The grid does not have any data. So skipping the field validation.\n"
    else
      if (@entry1Actuaryvalue.to_s != "nil" || @entry1Actuaryvalue.to_s != "") && (@possibleRows >= 1)
        if @entry1Actuaryvalue.to_s == @NeededUser
          print "PASSED - The 1st Deal Entry in the Grid is having the name " + @NeededUser + " in the Actuary name field.\n"
        else
          print "FAILED - The 1st Deal Entry in the Grid is not having the name " + @NeededUser + " in the Actuary name field.\n"
          fail "FAILED - The 1st Deal Entry in the Grid is not having the name " + @NeededUser + " in the Actuary name field.\n"
        end
      end
      if (@entry2Actuaryvalue.to_s != "nil" || @entry2Actuaryvalue.to_s != "") && (@possibleRows >= 2)
        if @entry2Actuaryvalue.to_s == @NeededUser
          print "PASSED - The 2nd Deal Entry in the Grid is having the name " + @NeededUser + " in the Actuary name field.\n"
        else
          print "FAILED - The 2nd Deal Entry in the Grid is not having the name " + @NeededUser + " in the Actuary name field.\n"
          fail "FAILED - The 2nd Deal Entry in the Grid is not having the name " + @NeededUser + " in the Actuary name field.\n"
        end
      end
      if (@entry3Actuaryvalue.to_s != "nil" || @entry3Actuaryvalue.to_s != "") && (@possibleRows >= 3)
        if @entry3Actuaryvalue.to_s == @NeededUser
          print "PASSED - The 3rd Deal Entry in the Grid is having the name " + @NeededUser + " in the Actuary name field.\n"
        else
          print "FAILED - The 3rd Deal Entry in the Grid is not having the name " + @NeededUser + " in the Actuary name field.\n"
          fail "FAILED - The 3rd Deal Entry in the Grid is not having the name " + @NeededUser + " in the Actuary name field.\n"
        end
      end
      if (@entry4Actuaryvalue.to_s != "nil" || @entry4Actuaryvalue.to_s != "") && (@possibleRows >= 4)
        if @entry4Actuaryvalue.to_s == @NeededUser
          print "PASSED - The 4th Deal Entry in the Grid is having the name " + @NeededUser + " in the Actuary name field.\n"
        else
          print "FAILED - The 4th Deal Entry in the Grid is not having the name " + @NeededUser + " in the Actuary name field.\n"
          fail "FAILED - The 4th Deal Entry in the Grid is not having the name " + @NeededUser + " in the Actuary name field.\n"
        end
      end
      if (@entry5Actuaryvalue.to_s != "nil" || @entry5Actuaryvalue.to_s != "") && (@possibleRows >= 5)
        if @entry5Actuaryvalue.to_s == @NeededUser
          print "PASSED - The 5th Deal Entry in the Grid is having the name " + @NeededUser + " in the Actuary name field.\n"
        else
          print "FAILED - The 5th Deal Entry in the Grid is not having the name " + @NeededUser + " in the Actuary name field.\n"
          fail "FAILED - The 5th Deal Entry in the Grid is not having the name " + @NeededUser + " in the Actuary name field.\n"
        end
      end
      if (@entry6Actuaryvalue.to_s != "nil" || @entry6Actuaryvalue.to_s != "") && (@possibleRows >= 6)
        if @entry6Actuaryvalue.to_s == @NeededUser
          print "PASSED - The 6th Deal Entry in the Grid is having the name " + @NeededUser + " in the Actuary name field.\n"
        else
          print "FAILED - The 6th Deal Entry in the Grid is not having the name " + @NeededUser + " in the Actuary name field.\n"
          fail "FAILED - The 6th Deal Entry in the Grid is not having the name " + @NeededUser + " in the Actuary name field.\n"
        end
      end
    end


  end
  def verifyGridDataisOfActuaryManagerTeam(gData,user)
    @GridData = gData
    # print "\n"
    # print @GridData
    # print "\n"
    @NeededUser = user
    @colcount = 0
    @rowcount = 1
    @possibleRows = 6
    if @GridData[[@rowcount+6,@colcount]].to_s == "nil" || @GridData[[@rowcount+6,@colcount]].to_s == ""
      @possibleRows=6
    end
    if @GridData[[@rowcount+5,@colcount]].to_s == "nil" || @GridData[[@rowcount+5,@colcount]].to_s == ""
      @possibleRows=5
    end
    if @GridData[[@rowcount+4,@colcount]].to_s == "nil" || @GridData[[@rowcount+4,@colcount]].to_s == ""
      @possibleRows=4
    end
    if @GridData[[@rowcount+3,@colcount]].to_s == "nil" || @GridData[[@rowcount+3,@colcount]].to_s == ""
      @possibleRows=3
    end
    if @GridData[[@rowcount+2,@colcount]].to_s == "nil" || @GridData[[@rowcount+2,@colcount]].to_s == ""
      @possibleRows=2
    end
    if @GridData[[@rowcount+1,@colcount]].to_s == "nil" || @GridData[[@rowcount+1,@colcount]].to_s == ""
      @possibleRows=1
    end
    if @GridData[[@rowcount,@colcount]].to_s == "nil" || @GridData[[@rowcount,@colcount]].to_s == ""
      @possibleRows=0
    end
    print "Grid is displayed with " + @possibleRows.to_s + " deals.\n"
    @entry1Actuaryvalue = @GridData[[1,12]].to_s
    @entry2Actuaryvalue = @GridData[[2,12]].to_s
    @entry3Actuaryvalue = @GridData[[3,12]].to_s
    @entry4Actuaryvalue = @GridData[[4,12]].to_s
    @entry5Actuaryvalue = @GridData[[5,12]].to_s
    @entry6Actuaryvalue = @GridData[[6,12]].to_s

    if (@GridData[[1,1]].to_s == "nil" ||  @GridData[[1,1]].to_s == "") && (@GridData[[14,1]].to_s == "nil" ||  @GridData[[14,1]].to_s == "") && (@possibleRows == 0)
      print "The grid does not have any data. So skipping the field validation.\n"
    else
      if (@entry1Actuaryvalue.to_s != "nil" || @entry1Actuaryvalue.to_s != "") && (@possibleRows >= 1)
        if @NeededUser.include? @entry1Actuaryvalue.to_s
          print "PASSED - The 1st Deal Entry in the Grid is having the name " + @entry1Actuaryvalue + " in the Actuary name field.\n"
        else
          print "FAILED - The 1st Deal Entry in the Grid is not having the name " + @entry1Actuaryvalue + " in the Actuary name field.\n"
          fail "FAILED - The 1st Deal Entry in the Grid is not having the name " + @entry1Actuaryvalue + " in the Actuary name field.\n"
        end
      end
      if (@entry2Actuaryvalue.to_s != "nil" || @entry2Actuaryvalue.to_s != "") && (@possibleRows >= 2)
        if @NeededUser.include? @entry2Actuaryvalue.to_s
          print "PASSED - The 2nd Deal Entry in the Grid is having the name " + @entry2Actuaryvalue + " in the Actuary name field.\n"
        else
          print "FAILED - The 2nd Deal Entry in the Grid is not having the name " + @entry2Actuaryvalue + " in the Actuary name field.\n"
          fail "FAILED - The 2nd Deal Entry in the Grid is not having the name " + @entry2Actuaryvalue + " in the Actuary name field.\n"
        end
      end
      if (@entry3Actuaryvalue.to_s != "nil" || @entry3Actuaryvalue.to_s != "") && (@possibleRows >= 3)
        if @NeededUser.include? @entry3Actuaryvalue.to_s
          print "PASSED - The 3rd Deal Entry in the Grid is having the name " + @entry3Actuaryvalue + " in the Actuary name field.\n"
        else
          print "FAILED - The 3rd Deal Entry in the Grid is not having the name " + @entry3Actuaryvalue + " in the Actuary name field.\n"
          fail "FAILED - The 3rd Deal Entry in the Grid is not having the name " + @entry3Actuaryvalue + " in the Actuary name field.\n"
        end
      end
      if (@entry4Actuaryvalue.to_s != "nil" || @entry4Actuaryvalue.to_s != "") && (@possibleRows >= 4)
        if @NeededUser.include? @entry4Actuaryvalue.to_s
          print "PASSED - The 4th Deal Entry in the Grid is having the name " + @entry4Actuaryvalue + " in the Actuary name field.\n"
        else
          print "FAILED - The 4th Deal Entry in the Grid is not having the name " + @entry4Actuaryvalue + " in the Actuary name field.\n"
          fail "FAILED - The 4th Deal Entry in the Grid is not having the name " + @entry4Actuaryvalue + " in the Actuary name field.\n"
        end
      end
      if (@entry5Actuaryvalue.to_s != "nil" || @entry5Actuaryvalue.to_s != "") && (@possibleRows >= 5)
        if @NeededUser.include? @entry5Actuaryvalue.to_s
          print "PASSED - The 5th Deal Entry in the Grid is having the name " + @entry5Actuaryvalue + " in the Actuary name field.\n"
        else
          print "FAILED - The 5th Deal Entry in the Grid is not having the name " + @entry5Actuaryvalue + " in the Actuary name field.\n"
          fail "FAILED - The 5th Deal Entry in the Grid is not having the name " + @entry5Actuaryvalue + " in the Actuary name field.\n"
        end
      end
      if (@entry6Actuaryvalue.to_s != "nil" || @entry6Actuaryvalue.to_s != "") && (@possibleRows >= 6)
        if @NeededUser.include? @entry6Actuaryvalue.to_s
          print "PASSED - The 6th Deal Entry in the Grid is having the name " + @entry6Actuaryvalue + " in the Actuary name field.\n"
        else
          print "FAILED - The 6th Deal Entry in the Grid is not having the name " + @entry6Actuaryvalue + " in the Actuary name field.\n"
          fail "FAILED - The 6th Deal Entry in the Grid is not having the name " + @entry6Actuaryvalue + " in the Actuary name field.\n"
        end
      end
    end
  end
  def verifyGridDataisOfUWManagerTeam(gData,user)
    @GridData = gData
    @NeededUWUser = user
    @colcount = 0
    @rowcount = 1
    @possibleRows = 6
    if @GridData[[@rowcount+6,@colcount]].to_s == "nil" || @GridData[[@rowcount+6,@colcount]].to_s == ""
      @possibleRows=6
    end
    if @GridData[[@rowcount+5,@colcount]].to_s == "nil" || @GridData[[@rowcount+5,@colcount]].to_s == ""
      @possibleRows=5
    end
    if @GridData[[@rowcount+4,@colcount]].to_s == "nil" || @GridData[[@rowcount+4,@colcount]].to_s == ""
      @possibleRows=4
    end
    if @GridData[[@rowcount+3,@colcount]].to_s == "nil" || @GridData[[@rowcount+3,@colcount]].to_s == ""
      @possibleRows=3
    end
    if @GridData[[@rowcount+2,@colcount]].to_s == "nil" || @GridData[[@rowcount+2,@colcount]].to_s == ""
      @possibleRows=2
    end
    if @GridData[[@rowcount+1,@colcount]].to_s == "nil" || @GridData[[@rowcount+1,@colcount]].to_s == ""
      @possibleRows=1
    end
    if @GridData[[@rowcount,@colcount]].to_s == "nil" || @GridData[[@rowcount,@colcount]].to_s == ""
      @possibleRows=0
    end
    print "Grid is displayed with " + @possibleRows.to_s + " deals.\n"
    # @entry1UW1value = @GridData[[9,1]].to_s
    # @entry1UW2value = @GridData[[10,1]].to_s
    # @entry2UW1value = @GridData[[22,1]].to_s
    # @entry2UW2value = @GridData[[23,1]].to_s
    # @entry3UW1value = @GridData[[35,1]].to_s
    # @entry3UW2value = @GridData[[36,1]].to_s
    # @entry4UW1value = @GridData[[48,1]].to_s
    # @entry4UW2value = @GridData[[49,1]].to_s
    # @entry5UW1value = @GridData[[61,1]].to_s
    # @entry5UW2value = @GridData[[62,1]].to_s
    # @entry6UW1value = @GridData[[87,1]].to_s
    # @entry6UW2value = @GridData[[88,1]].to_s
    @entry1UW1value = @GridData[[1,8]].to_s
    @entry1UW2value = @GridData[[1,9]].to_s
    @entry2UW1value = @GridData[[2,8]].to_s
    @entry2UW2value = @GridData[[2,9]].to_s
    @entry3UW1value = @GridData[[3,8]].to_s
    @entry3UW2value = @GridData[[3,9]].to_s
    @entry4UW1value = @GridData[[4,8]].to_s
    @entry4UW2value = @GridData[[4,9]].to_s
    @entry5UW1value = @GridData[[5,8]].to_s
    @entry5UW2value = @GridData[[5,9]].to_s
    @entry6UW1value = @GridData[[6,8]].to_s
    @entry6UW2value = @GridData[[6,9]].to_s

    if (@GridData[[1,1]].to_s == "nil" ||  @GridData[[1,1]].to_s == "") && (@GridData[[14,1]].to_s == "nil" ||  @GridData[[14,1]].to_s == "") && (@possibleRows == 0)
      print "The grid does not have any data. So skipping the field validation.\n"
    else
      if ((@entry1UW1value.to_s != "nil" || @entry1UW2value.to_s != "nil") || (@entry1UW1value.to_s != "" || @entry1UW2value.to_s != "")) && (@possibleRows >= 1)
        if @NeededUWUser.include?(@entry1UW1value.to_s) || @NeededUWUser.include?(@entry1UW2value.to_s)
          print "PASSED - The 1st Deal Entry in the Grid is having the name " + @entry1UW1value + " in the UnderWriter1 and " + @entry1UW2value + " in the UnderWriter2 name field.\n"
        else
          print "FAILED - The 1st Deal Entry in the Grid is having the name " + @entry1UW1value + " in the UnderWriter1 and " + @entry1UW2value + " in the UnderWriter2 name field.\n"
          fail "FAILED - The 1st Deal Entry in the Grid is having the name " + @entry1UW1value + " in the UnderWriter1 and " + @entry1UW2value + " in the UnderWriter2 name field.\n"
        end
      end
      if ((@entry2UW1value.to_s != "nil" || @entry2UW2value.to_s != "nil") || (@entry2UW1value.to_s != "" || @entry2UW2value.to_s != "")) && (@possibleRows >= 2)
        if @NeededUWUser.include?(@entry2UW1value.to_s) || @NeededUWUser.include?(@entry2UW2value.to_s)
          print "PASSED - The 2nd Deal Entry in the Grid is having the name " + @entry2UW1value + " in the UnderWriter1 and " + @entry2UW2value + " in the UnderWriter2 name field.\n"
        else
          print "FAILED - The 2nd Deal Entry in the Grid is having the name " + @entry2UW1value + " in the UnderWriter1 and " + @entry2UW2value + " in the UnderWriter2 name field.\n"
          fail "FAILED - The 2nd Deal Entry in the Grid is having the name " + @entry2UW1value + " in the UnderWriter1 and " + @entry2UW2value + " in the UnderWriter2 name field.\n"
        end
      end
      if ((@entry3UW1value.to_s != "nil" || @entry3UW2value.to_s != "nil") || (@entry3UW1value.to_s != "" || @entry3UW2value.to_s != "")) && (@possibleRows >= 3)
        if @NeededUWUser.include?(@entry3UW1value.to_s) || @NeededUWUser.include?(@entry3UW2value.to_s)
          print "PASSED - The 3rd Deal Entry in the Grid is having the name " + @entry3UW1value + " in the UnderWriter1 and " + @entry3UW2value + " in the UnderWriter2 name field.\n"
        else
          print "FAILED - The 3rd Deal Entry in the Grid is having the name " + @entry3UW1value + " in the UnderWriter1 and " + @entry3UW2value + " in the UnderWriter2 name field.\n"
          fail "FAILED - The 3rd Deal Entry in the Grid is having the name " + @entry3UW1value + " in the UnderWriter1 and " + @entry3UW2value + " in the UnderWriter2 name field.\n"
        end
      end
      if ((@entry4UW1value.to_s != "nil" || @entry4UW2value.to_s != "nil") || (@entry4UW1value.to_s != "" || @entry4UW2value.to_s != "")) && (@possibleRows >= 4)
        if @NeededUWUser.include?(@entry4UW1value.to_s) || @NeededUWUser.include?(@entry4UW2value.to_s)
          print "PASSED - The 4th Deal Entry in the Grid is having the name " + @entry4UW1value + " in the UnderWriter1 and " + @entry4UW2value + " in the UnderWriter2 name field.\n"
        else
          print "FAILED - The 4th Deal Entry in the Grid is having the name " + @entry4UW1value + " in the UnderWriter1 and " + @entry4UW2value + " in the UnderWriter2 name field.\n"
          fail "FAILED - The 4th Deal Entry in the Grid is having the name " + @entry4UW1value + " in the UnderWriter1 and " + @entry4UW2value + " in the UnderWriter2 name field.\n"
        end
      end
      if ((@entry5UW1value.to_s != "nil" || @entry5UW2value.to_s != "nil") || (@entry5UW1value.to_s != "" || @entry5UW2value.to_s != "")) && (@possibleRows >= 5)
        if @NeededUWUser.include?(@entry5UW1value.to_s) || @NeededUWUser.include?(@entry5UW2value.to_s)
          print "PASSED - The 5th Deal Entry in the Grid is having the name " + @entry5UW1value + " in the UnderWriter1 and " + @entry5UW2value + " in the UnderWriter2 name field.\n"
        else
          print "FAILED - The 5th Deal Entry in the Grid is having the name " + @entry5UW1value + " in the UnderWriter1 and " + @entry5UW2value + " in the UnderWriter2 name field.\n"
          fail "FAILED - The 5th Deal Entry in the Grid is having the name " + @entry5UW1value + " in the UnderWriter1 and " + @entry5UW2value + " in the UnderWriter2 name field.\n"
        end
      end
      if ((@entry6UW1value.to_s != "nil" || @entry6UW2value.to_s != "nil") || (@entry6UW1value.to_s != "" || @entry6UW2value.to_s != "")) && (@possibleRows >= 6)
        if @NeededUWUser.include?(@entry6UW1value.to_s) || @NeededUWUser.include?(@entry6UW2value.to_s)
          print "PASSED - The 6th Deal Entry in the Grid is having the name " + @entry6UW1value + " in the UnderWriter1 and " + @entry6UW2value + " in the UnderWriter2 name field.\n"
        else
          print "FAILED - The 6th Deal Entry in the Grid is having the name " + @entry6UW1value + " in the UnderWriter1 and " + @entry6UW2value + " in the UnderWriter2 name field.\n"
          fail "FAILED - The 6th Deal Entry in the Grid is having the name " + @entry6UW1value + " in the UnderWriter1 and " + @entry6UW2value + " in the UnderWriter2 name field.\n"
        end
      end
    end
  end

  def VerifySubstatusIsChecked(substatus)
    @NeededSubStatusArr = substatus
    for count in 0..@NeededSubStatusArr.length-1
      #print "\nPerforming the execution for substatus panel " + @NeededSubStatusArr[count] + ".\n"
      @ElementtobeChecked = fetchpanelSubStatusElement(@NeededSubStatusArr[count])
      @ElementInputtobChecked = fetchpanelSubStatusElementInput(@NeededSubStatusArr[count])
      @ElementName = @NeededSubStatusArr[count]
      verifyElementExistonPage(@ElementtobeChecked,@ElementName)
      @panelcountelement = getpanelCountElement(@ElementtobeChecked)
      @panelcount = getsubstatusCount(@panelcountelement)
      VerifyElementText(@ElementtobeChecked,@NeededSubStatusArr[count])
      verifyZerocountandDisabled(@NeededSubStatusArr[count],@panelcount,@ElementtobeChecked)
      verifyZerocountandUnSelected(@NeededSubStatusArr[count],@panelcount,@ElementtobeChecked,@ElementInputtobChecked)
    end
  end
  def VerifySubstatusIsdisplayed(substatus)
    @NeededSubStatusArr = substatus
    for count in 0..@NeededSubStatusArr.length-1
      #print "\nPerforming the execution for substatus panel " + @NeededSubStatusArr[count] + ".\n"
      @ElementtobeChecked = fetchpanelSubStatusElement(@NeededSubStatusArr[count])
      @ElementName = @NeededSubStatusArr[count]
      verifyElementExistonPage(@ElementtobeChecked,@ElementName)
      VerifyElementText(@ElementtobeChecked,@NeededSubStatusArr[count])
    end
  end
  def verifyGridFieldscheckedInToolsColMenu
    @fieldElement = fetchdealnamegridcolumnElement
    @ElementToBEClicked = fetchMenuFieldElement(@fieldElement)
    ClickGridCol(@ElementToBEClicked)
    @ToolMenuHeaderTabElement = fetchToolMenuHeaderTabelement
    ClickAgToolMenuColumnButton(@ToolMenuHeaderTabElement)
    @DealNameToolMenuFieldElement = fetchdealnameToolMenuFieldElement
    @ContractNumberToolMenuFieldElement = fetchcontractnumberToolMenuFieldElement
    @InceptionToolMenuFieldElement = fetchinceptionToolMenuFieldElement
    @TargetDateToolMenuFieldElement = fetchtargetdateToolMenuFieldElement
    @PriorityToolMenuFieldElement = fetchpriorityToolMenuFieldElement
    @SubmittedToolMenuFieldElement = fetchsubmittedToolMenuFieldElement
    @StatusToolMenuFieldElement = fetchstatusToolMenuFieldElement
    @DealNumberToolMenuFieldElement = fetchdealnumberToolMenuFieldElement
    @UWNameToolMenuFieldElement = fetchUWNameToolMenuFieldElement
    @UW2NameToolMenuFieldElement = fetchUW2NameToolMenuFieldElement
    @TAToolMenuFieldElement = fetchTAToolMenuFieldElement
    @ModelerToolMenuFieldElement = fetchModelerToolMenuFieldElement
    @ActuaryToolMenuFieldElement = fetchActuaryToolMenuFieldElement
    @ExpirationToolMenuFieldElement = fetchExpirationToolMenuFieldElement
    @BrokerNameToolMenuFieldElement = fetchbrokernameToolMenuFieldElement
    @BrokerContactToolMenuFieldElement = fetchbrokercontactToolMenuFieldElement
    sleep 6
    @DealNameToolMenuFieldElementIsNotChecked = @DealNameToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    @ContractNumberToolMenuFieldElementIsNotChecked = @ContractNumberToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    @InceptionToolMenuFieldElementIsNotChecked = @InceptionToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    @TargetDateToolMenuFieldElementIsNotChecked = @TargetDateToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    @PriorityToolMenuFieldElementIsNotChecked = @PriorityToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    @SubmittedToolMenuFieldElementIsNotChecked = @SubmittedToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    @StatusToolMenuFieldElementIsNotChecked = @StatusToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    @DealNumberToolMenuFieldElementIsNotChecked = @DealNumberToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    @UWNameToolMenuFieldElementIsNotChecked = @UWNameToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    @UW2NameToolMenuFieldElementIsNotChecked = @UW2NameToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    @TAToolMenuFieldElementIsNotChecked = @TAToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    @ModelerToolMenuFieldElementIsNotChecked = @ModelerToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    @ActuaryToolMenuFieldElementIsNotChecked = @ActuaryToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    @ExpirationToolMenuFieldElementIsNotChecked = @ExpirationToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    @BrokerNameToolMenuFieldElementIsNotChecked = @BrokerNameToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    @BrokerContactToolMenuFieldElementIsNotChecked = @BrokerContactToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    sleep 6
    @DealNameFieldElement = fetchdealnamegridcolumnElement
    @ContractNumberFieldElement = fetchcontractnumbergridcolumnElement
    @InceptionFieldElement = fetchinceptiongridcolumnElement
    @TargetDateFieldElement = fetchtargetdategridcolumnElement
    @PriorityFieldElement = fetchprioritygridcolumnElement
    @SubmittedFieldElement = fetchsubmittedgridcolumnElement
    @StatusFieldElement = fetchstatusgridcolumnElement
    @DealNumberFieldElement = fetchdealnumbergridcolumnElement
    @UWNameFieldElement = fetchUWNamegridcolumnElement
    @UW2NameFieldElement = fetchUW2NamegridcolumnElement
    #@GridCell = fetchGridCellElement
    #@GridCell.click
    #@browser.send_keys :control,:arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    sleep 1
    @TAFieldElement = fetchTAgridcolumnElement
    @ModelerFieldElement = fetchModelergridcolumnElement
    @ActuaryFieldElement = fetchActuarygridcolumnElement
    @ExpirationFieldElement = fetchExpirationgridcolumnElement
    @BrokerNameFieldElement = fetchbrokernamegridcolumnElement
    @BrokerContactFieldElement = fetchbrokercontactgridcolumnElement
    #@browser.send_keys :control,:arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @DealNameFieldElementAvailable = @DealNameFieldElement.exist?
    @ContractNumberFieldElementAvailable = @ContractNumberFieldElement.exist?
    @InceptionFieldElementAvailable = @InceptionFieldElement.exist?
    @TargetDateFieldElementAvailable = @TargetDateFieldElement.exist?
    @PriorityFieldElementAvailable = @PriorityFieldElement.exist?
    @SubmittedFieldElementAvailable = @SubmittedFieldElement.exist?
    @StatusFieldElementAvailable = @StatusFieldElement.exist?
    @DealNumberFieldElementAvailable = @DealNumberFieldElement.exist?
    @UWNameFieldElementAvailable = @UWNameFieldElement.exist?
    @UW2NameFieldElementAvailable = @UW2NameFieldElement.exist?
    @GridCell = fetchGridCellElement
    @GridCell.click
    #@browser.send_keys :control,:arrow_left
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    sleep 1
    @TAFieldElementAvailable = @TAFieldElement.exist?
    @ModelerFieldElementAvailable = @ModelerFieldElement.exist?
    @ActuaryFieldElementAvailable = @ActuaryFieldElement.exist?
    @ExpirationFieldElementAvailable = @ExpirationFieldElement.exist?
    @BrokerNameFieldElementAvailable = @BrokerNameFieldElement.exist?
    @BrokerContactFieldElementAvailable = @BrokerContactFieldElement.exist?
    #@browser.send_keys :control,:arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    sleep 2
    # print "################################################################################\n"
    # print "\n"
    # print @DealNameFieldElementAvailable
    # print"\n"
    # print @ContractNumberFieldElementAvailable
    # print"\n"
    # print @InceptionFieldElementAvailable
    # print"\n"
    # print @TargetDateFieldElementAvailable
    # print"\n"
    # print @PriorityFieldElementAvailable
    # print"\n"
    # print @SubmittedFieldElementAvailable
    # print"\n"
    # print @StatusFieldElementAvailable
    # print"\n"
    # print @DealNumberFieldElementAvailable
    # print"\n"
    # print @UWNameFieldElementAvailable
    # print"\n"
    # print @UW2NameFieldElementAvailable
    # print"\n"
    # print @TAFieldElementAvailable
    # print"\n"
    # print @ModelerFieldElementAvailable
    # print"\n"
    # print @ActuaryFieldElementAvailable
    # print"\n"
    # print @ExpirationFieldElementAvailable
    # print"\n"
    # print @BrokerNameFieldElementAvailable
    # print"\n"
    # print @BrokerContactFieldElementAvailable
    # print"\n"
    # print "################################################################################\n"
    # print "\n"
    # print @DealNameToolMenuFieldElementIsNotChecked
    # print"\n"
    # print @ContractNumberToolMenuFieldElementIsNotChecked
    # print"\n"
    # print @InceptionToolMenuFieldElementIsNotChecked
    # print"\n"
    # print @TargetDateToolMenuFieldElementIsNotChecked
    # print"\n"
    # print @PriorityToolMenuFieldElementIsNotChecked
    # print"\n"
    # print @SubmittedToolMenuFieldElementIsNotChecked
    # print"\n"
    # print @StatusToolMenuFieldElementIsNotChecked
    # print"\n"
    # print @DealNumberToolMenuFieldElementIsNotChecked
    # print"\n"
    # print @UWNameToolMenuFieldElementIsNotChecked
    # print"\n"
    # print @UW2NameToolMenuFieldElementIsNotChecked
    # print"\n"
    # print @TAToolMenuFieldElementIsNotChecked
    # print"\n"
    # print @ModelerToolMenuFieldElementIsNotChecked
    # print"\n"
    # print @ActuaryToolMenuFieldElementIsNotChecked
    # print"\n"
    # print @ExpirationToolMenuFieldElementIsNotChecked
    # print"\n"
    # print @BrokerNameToolMenuFieldElementIsNotChecked
    # print"\n"
    # print @BrokerContactToolMenuFieldElementIsNotChecked
    # print"\n"
    # print "################################################################################\n"

    if @DealNameFieldElementAvailable!=@DealNameToolMenuFieldElementIsNotChecked && @ContractNumberFieldElementAvailable!=@ContractNumberToolMenuFieldElementIsNotChecked && @InceptionFieldElementAvailable!=@InceptionToolMenuFieldElementIsNotChecked && @TargetDateFieldElementAvailable!=@TargetDateToolMenuFieldElementIsNotChecked && @PriorityFieldElementAvailable!=@PriorityToolMenuFieldElementIsNotChecked && @SubmittedFieldElementAvailable!=@SubmittedToolMenuFieldElementIsNotChecked && @StatusFieldElementAvailable!=@StatusToolMenuFieldElementIsNotChecked && @DealNumberFieldElementAvailable!=@DealNumberToolMenuFieldElementIsNotChecked && @UWNameFieldElementAvailable!=@UWNameToolMenuFieldElementIsNotChecked && @UW2NameFieldElementAvailable!=@UW2NameToolMenuFieldElementIsNotChecked && @TargetDateFieldElementAvailable!=@TAToolMenuFieldElementIsNotChecked && @ModelerFieldElementAvailable!=@ModelerToolMenuFieldElementIsNotChecked && @ActuaryFieldElementAvailable!=@ActuaryToolMenuFieldElementIsNotChecked && @ExpirationFieldElementAvailable!=@ExpirationToolMenuFieldElementIsNotChecked && @BrokerNameFieldElementAvailable!=@BrokerNameToolMenuFieldElementIsNotChecked && @BrokerContactFieldElementAvailable!=@BrokerContactToolMenuFieldElementIsNotChecked
      print "PASSED - The fields available in GRS Deal Grid are checked in the GRID Tool Menu.\n"
      #@reporter.ReportAction("PASSED - The needed fields are available in GRS Deal Grid.\n")
    else
      print "FAILED - The fields available in GRS Deal Grid are not checked in the GRID Tool Menu.\n"
      fail "FAILED - The fields available in GRS Deal Grid are not checked in the GRID Tool Menu.\n"
      #@reporter.ReportAction("FAILED - Deal Grid does not have the needed fields. \n")
    end
  end
  def verifyGridFieldsNotAvailableUncheckedInToolsColMenu
    @fieldElement = fetchdealnamegridcolumnElement
    @ElementToBEClicked = fetchMenuFieldElement(@fieldElement)
    ClickGridCol(@ElementToBEClicked)
    @ToolMenuHeaderTabElement = fetchToolMenuHeaderTabelement
    ClickAgToolMenuColumnButton(@ToolMenuHeaderTabElement)
    @DealNameToolMenuFieldElement = fetchdealnameToolMenuFieldElement
    @ContractNumberToolMenuFieldElement = fetchcontractnumberToolMenuFieldElement
    @InceptionToolMenuFieldElement = fetchinceptionToolMenuFieldElement
    @TargetDateToolMenuFieldElement = fetchtargetdateToolMenuFieldElement
    @PriorityToolMenuFieldElement = fetchpriorityToolMenuFieldElement
    @SubmittedToolMenuFieldElement = fetchsubmittedToolMenuFieldElement
    @StatusToolMenuFieldElement = fetchstatusToolMenuFieldElement
    @DealNumberToolMenuFieldElement = fetchdealnumberToolMenuFieldElement
    @UWNameToolMenuFieldElement = fetchUWNameToolMenuFieldElement
    @UW2NameToolMenuFieldElement = fetchUW2NameToolMenuFieldElement
    @TAToolMenuFieldElement = fetchTAToolMenuFieldElement
    @ModelerToolMenuFieldElement = fetchModelerToolMenuFieldElement
    @ActuaryToolMenuFieldElement = fetchActuaryToolMenuFieldElement
    @ExpirationToolMenuFieldElement = fetchExpirationToolMenuFieldElement
    @BrokerNameToolMenuFieldElement = fetchbrokernameToolMenuFieldElement
    @BrokerContactToolMenuFieldElement = fetchbrokercontactToolMenuFieldElement
    @DealNameToolMenuFieldElementIsNotChecked = @DealNameToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    @ContractNumberToolMenuFieldElementIsNotChecked = @ContractNumberToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    @InceptionToolMenuFieldElementIsNotChecked = @InceptionToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    @TargetDateToolMenuFieldElementIsNotChecked = @TargetDateToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    @PriorityToolMenuFieldElementIsNotChecked = @PriorityToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    @SubmittedToolMenuFieldElementIsNotChecked = @SubmittedToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    @StatusToolMenuFieldElementIsNotChecked = @StatusToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    @DealNumberToolMenuFieldElementIsNotChecked = @DealNumberToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    @UWNameToolMenuFieldElementIsNotChecked = @UWNameToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    @UW2NameToolMenuFieldElementIsNotChecked = @UW2NameToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    @TAToolMenuFieldElementIsNotChecked = @TAToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    @ModelerToolMenuFieldElementIsNotChecked = @ModelerToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    @ActuaryToolMenuFieldElementIsNotChecked = @ActuaryToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    @ExpirationToolMenuFieldElementIsNotChecked = @ExpirationToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    @BrokerNameToolMenuFieldElementIsNotChecked = @BrokerNameToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    @BrokerContactToolMenuFieldElementIsNotChecked = @BrokerContactToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
     sleep 6
    @DealNameFieldElement = fetchdealnamegridcolumnElement
    @ContractNumberFieldElement = fetchcontractnumbergridcolumnElement
    @InceptionFieldElement = fetchinceptiongridcolumnElement
    @TargetDateFieldElement = fetchtargetdategridcolumnElement
    @PriorityFieldElement = fetchprioritygridcolumnElement
    @SubmittedFieldElement = fetchsubmittedgridcolumnElement
    @StatusFieldElement = fetchstatusgridcolumnElement
    @DealNumberFieldElement = fetchdealnumbergridcolumnElement
    @UWNameFieldElement = fetchUWNamegridcolumnElement
    @UW2NameFieldElement = fetchUW2NamegridcolumnElement
    @GridCell = fetchGridCellElement
    @GridCell.click
    #@browser.send_keys :control,:arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    sleep 1
    @TAFieldElement = fetchTAgridcolumnElement
    @ModelerFieldElement = fetchModelergridcolumnElement
    @ActuaryFieldElement = fetchActuarygridcolumnElement
    @ExpirationFieldElement = fetchExpirationgridcolumnElement
    @BrokerNameFieldElement = fetchbrokernamegridcolumnElement
    @BrokerContactFieldElement = fetchbrokercontactgridcolumnElement
    #@browser.send_keys :control, :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @DealNameFieldElementAvailable = @DealNameFieldElement.exist?
    @ContractNumberFieldElementAvailable = @ContractNumberFieldElement.exist?
    @InceptionFieldElementAvailable = @InceptionFieldElement.exist?
    @TargetDateFieldElementAvailable = @TargetDateFieldElement.exist?
    @PriorityFieldElementAvailable = @PriorityFieldElement.exist?
    @SubmittedFieldElementAvailable = @SubmittedFieldElement.exist?
    @StatusFieldElementAvailable = @StatusFieldElement.exist?
    @DealNumberFieldElementAvailable = @DealNumberFieldElement.exist?
    @UWNameFieldElementAvailable = @UWNameFieldElement.exist?
    @UW2NameFieldElementAvailable = @UW2NameFieldElement.exist?
    @GridCell = fetchGridCellElement
    @GridCell.click
    #@browser.send_keys :control,:arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    @browser.send_keys :arrow_right
    sleep 1
    @TAFieldElementAvailable = @TAFieldElement.exist?
    @ModelerFieldElementAvailable = @ModelerFieldElement.exist?
    @ActuaryFieldElementAvailable = @ActuaryFieldElement.exist?
    @ExpirationFieldElementAvailable = @ExpirationFieldElement.exist?
    @BrokerNameFieldElementAvailable = @BrokerNameFieldElement.exist?
    @BrokerContactFieldElementAvailable = @BrokerContactFieldElement.exist?
    #@browser.send_keys :control,:arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    @browser.send_keys :arrow_left
    sleep 2
    # print "################################################################################\n"
    # print "\n"
    # print @DealNameFieldElementAvailable
    # print"\n"
    # print @ContractNumberFieldElementAvailable
    # print"\n"
    # print @InceptionFieldElementAvailable
    # print"\n"
    # print @TargetDateFieldElementAvailable
    # print"\n"
    # print @PriorityFieldElementAvailable
    # print"\n"
    # print @SubmittedFieldElementAvailable
    # print"\n"
    # print @StatusFieldElementAvailable
    # print"\n"
    # print @DealNumberFieldElementAvailable
    # print"\n"
    # print @UWNameFieldElementAvailable
    # print"\n"
    # print @UW2NameFieldElementAvailable
    # print"\n"
    # print @TAFieldElementAvailable
    # print"\n"
    # print @ModelerFieldElementAvailable
    # print"\n"
    # print @ActuaryFieldElementAvailable
    # print"\n"
    # print @ExpirationFieldElementAvailable
    # print"\n"
    # print @BrokerNameFieldElementAvailable
    # print"\n"
    # print @BrokerContactFieldElementAvailable
    # print"\n"
    # print "################################################################################\n"
    # print "\n"
    # print @DealNameToolMenuFieldElementIsNotChecked
    # print"\n"
    # print @ContractNumberToolMenuFieldElementIsNotChecked
    # print"\n"
    # print @InceptionToolMenuFieldElementIsNotChecked
    # print"\n"
    # print @TargetDateToolMenuFieldElementIsNotChecked
    # print"\n"
    # print @PriorityToolMenuFieldElementIsNotChecked
    # print"\n"
    # print @SubmittedToolMenuFieldElementIsNotChecked
    # print"\n"
    # print @StatusToolMenuFieldElementIsNotChecked
    # print"\n"
    # print @DealNumberToolMenuFieldElementIsNotChecked
    # print"\n"
    # print @UWNameToolMenuFieldElementIsNotChecked
    # print"\n"
    # print @UW2NameToolMenuFieldElementIsNotChecked
    # print"\n"
    # print @TAToolMenuFieldElementIsNotChecked
    # print"\n"
    # print @ModelerToolMenuFieldElementIsNotChecked
    # print"\n"
    # print @ActuaryToolMenuFieldElementIsNotChecked
    # print"\n"
    # print @ExpirationToolMenuFieldElementIsNotChecked
    # print"\n"
    # print @BrokerNameToolMenuFieldElementIsNotChecked
    # print"\n"
    # print @BrokerContactToolMenuFieldElementIsNotChecked
    # print"\n"
    # print "################################################################################\n"
    if @DealNameFieldElementAvailable!=@DealNameToolMenuFieldElementIsNotChecked && @ContractNumberFieldElementAvailable!=@ContractNumberToolMenuFieldElementIsNotChecked && @InceptionFieldElementAvailable!=@InceptionToolMenuFieldElementIsNotChecked && @TargetDateFieldElementAvailable!=@TargetDateToolMenuFieldElementIsNotChecked && @PriorityFieldElementAvailable!=@PriorityToolMenuFieldElementIsNotChecked && @SubmittedFieldElementAvailable!=@SubmittedToolMenuFieldElementIsNotChecked && @StatusFieldElementAvailable!=@StatusToolMenuFieldElementIsNotChecked && @DealNumberFieldElementAvailable!=@DealNumberToolMenuFieldElementIsNotChecked && @UWNameFieldElementAvailable!=@UWNameToolMenuFieldElementIsNotChecked && @UW2NameFieldElementAvailable!=@UW2NameToolMenuFieldElementIsNotChecked && @TargetDateFieldElementAvailable!=@TAToolMenuFieldElementIsNotChecked && @ModelerFieldElementAvailable!=@ModelerToolMenuFieldElementIsNotChecked && @ActuaryFieldElementAvailable!=@ActuaryToolMenuFieldElementIsNotChecked && @ExpirationFieldElementAvailable!=@ExpirationToolMenuFieldElementIsNotChecked && @BrokerNameFieldElementAvailable!=@BrokerNameToolMenuFieldElementIsNotChecked && @BrokerContactFieldElementAvailable!=@BrokerContactToolMenuFieldElementIsNotChecked
      print "PASSED - The fields not available in GRS Deal Grid are unchecked in the GRID Tool Menu.\n"
      #@reporter.ReportAction("PASSED - The needed fields are available in GRS Deal Grid.\n")
    else
      print "FAILED - The fields not available in GRS Deal Grid are not unchecked in the GRID Tool Menu.\n"
      fail "FAILED - The fields not available in GRS Deal Grid are not unchecked in the GRID Tool Menu.\n"
      #@reporter.ReportAction("FAILED - Deal Grid does not have the needed fields. \n")
    end
  end
  def unselectToolMenuFieldElement(toolMenuFieldElement)
    @ToolMenuFElement = toolMenuFieldElement
    @ToolMenuFElementCheckBox = @ToolMenuFElement.span(:class => 'ag-column-select-checkbox').span(:index => 0)
    @aghiddenclasstextavailable = @ToolMenuFElementCheckBox.attribute_value('className').include? "ag-hidden".to_s
    print "\n"
    print @ToolMenuFElementCheckBox.attribute_value('className').to_s
    print "\n"
    if @aghiddenclasstextavailable == "true"
      print "Tool Menu Field is already unselected.\n"
    else
      @ToolMenuFElement.click
    end
  end
  def unselectToolMenuField(toolMenuFieldElement)
    @ToolMenuFElement = toolMenuFieldElement

    require 'timeout'
    # @status = Timeout::timeout(10) {
      begin
        @ToolMenuFElement.click
      rescue Watir::Exception::UnknownObjectException
        print "Tool Menu Field is already unselected.\n"
      end

    # }

  end
  def selectToolMenuFieldElement(toolMenuFieldElement)
    @ToolMenuFElement = toolMenuFieldElement
    @ToolMenuFElementCheckBox = @ToolMenuFElement.span(:class => 'ag-column-select-checkbox').span(:index => 0)
    @aghiddenclasstextavailable = @ToolMenuFElementCheckBox.attribute_value('className').include? "ag-hidden".to_s
    if @aghiddenclasstextavailable == "false"
      @ToolMenuFElement.click
    else
      print "Tool Menu Field is already selected.\n"
    end
  end
  def verifyGridFieldUncheckedInToolsColMenuisNotAvailableinGRID(toolMenuFieldEle,elementName)
    @ToolMenuFieldElement = toolMenuFieldEle
    @GridElement = fetchGridFieldElement(elementName)
    @ToolMenuFieldElementAvailable = @GridElement.exist?

    @ToolMenuFieldElementCheckboxisUnChecked = @ToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    # print @ToolMenuFieldElementAvailable
    # print @ToolMenuFieldElementCheckboxisUnChecked
    if @ToolMenuFieldElementAvailable != @ToolMenuFieldElementCheckboxisUnChecked
      print "PASSED - The " + elementName + " field is not available in GRS Deal Grid and is also unchecked in the GRID Tool Menu.\n"
      #@reporter.ReportAction("PASSED - The needed fields are available in GRS Deal Grid.\n")
    else
      print "FAILED - The " + elementName + " field is available in GRS Deal Grid and is also unchecked in the GRID Tool Menu.\n"
      fail "FAILED - The " + elementName + " field is available in GRS Deal Grid and is also unchecked in the GRID Tool Menu.\n"
      #@reporter.ReportAction("FAILED - Deal Grid does not have the needed fields. \n")
    end
  end

  def verifyGridFieldUncheckedInGRIDColMenuisNotAvailableinGRID(toolMenuFieldEle,elementName)
    @ToolMenuFieldElement = toolMenuFieldEle
    @GridElement = fetchGRIDFieldElementFromGrid(elementName)
    @ToolMenuFieldElementAvailable = @GridElement.exist?

    # @ToolMenuFieldElementCheckboxisUnChecked = @ToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    # print @ToolMenuFieldElementAvailable
    # print @ToolMenuFieldElementCheckboxisUnChecked
    if @ToolMenuFieldElementAvailable == false
      print "PASSED - The " + elementName + " field is not available in GRS Deal Grid and is also unchecked in the GRID Tool Menu.\n"
      #@reporter.ReportAction("PASSED - The needed fields are available in GRS Deal Grid.\n")
    else
      print "FAILED - The " + elementName + " field is available in GRS Deal Grid and is also unchecked in the GRID Tool Menu.\n"
      fail "FAILED - The " + elementName + " field is available in GRS Deal Grid and is also unchecked in the GRID Tool Menu.\n"
      #@reporter.ReportAction("FAILED - Deal Grid does not have the needed fields. \n")
    end
  end

  def verifyGridFieldNotAvailableinGRID(gridFieldEle,elementName)
    @gridFieldElement = gridFieldEle

    @gridFieldElementAvailable = @gridFieldElement.exist?
    #print @gridFieldElementAvailable
    #print "\n"
    #print @gridFieldElementAvailable
    #print "\n"
    #@ToolMenuFieldElementCheckboxisUnChecked = @ToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    if @gridFieldElementAvailable.to_s == "false"
      print "PASSED - The " + elementName + " field is not available in GRS Deal Grid.\n"
      #@reporter.ReportAction("PASSED - The needed fields are available in GRS Deal Grid.\n")
    else
      print "FAILED - The " + elementName + " field is available in GRS Deal Grid.\n"
      fail "FAILED - The " + elementName + " field is available in GRS Deal Grid.\n"
      #@reporter.ReportAction("FAILED - Deal Grid does not have the needed fields. \n")
    end
  end
  def verifyFieldAvailableinGRID(gridFieldEle,elementName)
    @gridFieldElement = gridFieldEle
    @gridFieldElementAvailable = @gridFieldElement.exist?
    print "\n"
    print @gridFieldElementAvailable

    print "\n"
    #print "\n"
    #print @gridFieldElementAvailable
    #print "\n"
    #@ToolMenuFieldElementCheckboxisUnChecked = @ToolMenuFieldElement.span(:class => 'ag-column-select-checkbox').span(:index => 0).attribute_value('className').include? "ag-hidden"
    if @gridFieldElementAvailable.to_s == "true"
      print "PASSED - The " + elementName + " field is available in GRS Deal Grid.\n"
      #@reporter.ReportAction("PASSED - The needed fields are available in GRS Deal Grid.\n")
    else
      print "FAILED - The " + elementName + " field is not available in GRS Deal Grid.\n"
      fail "FAILED - The " + elementName + " field is not available in GRS Deal Grid.\n"
      #@reporter.ReportAction("FAILED - Deal Grid does not have the needed fields. \n")
    end
  end
  def get_process_info()
    procs = WIN32OLE.connect("winmgmts:\\\\.")
    procs.InstancesOf("win32_process").each do |p|
      return p.name.to_s
    end
  end
  def VerifyERMSLaunch

    # @browser.window(:index => 1).use
    #   if @browser.alert.exists?
    #   print "\n"
    #   print @browser.window(:index=>0).url
    #   print @browser.alert.text
    #   print "\n"
    #   @wsh = WIN32OLE.new('Wscript.Shell')
    #   # @wsh.SendKeys('{TAB}')
    #   # @wsh.SendKeys('{TAB}')
    #   # @wsh.SendKeys('{TAB}')
    #   # @wsh.SendKeys('{TAB}')
    #   # @wsh.SendKeys('{ENTER}')
    #   @browser.alert.accept
    #   end
    #@processlist = get_process_info.to_s
    #@processlist =
    #@processlist = system("tasklist").to_s
    @outFile = File.join(File.absolute_path('../', File.dirname(__FILE__)),"Output_Files","outfile.out")
    @processlist = %x("tasklist").to_s #> @outFile
    print "\n"
    print @processlist
    print "\n"

    if (@processlist.to_s.include?("ERMSLaunch") ||  @processlist.to_s.include?("wfica32"))
      print "PASSED - The ERMS application is launched successfully.\n"
    else
      print "FAILED - THE ERMS application failed to get launched.\n"
      fail "FAILED - THE ERMS application failed to get launched.\n"
    end


  end
  def VerifyGridDataMultipleSelectionDealStatus(gridData,status1,status2)
    @GData = gridData
    @NeededStatus1 = status1
    @NeededStatus2 = status2
    # print "\n"
    # print @GData[16]
    # print "\n"
    # print @GData[25]
    # print "\n"
    # print @GData[34]
    # print "\n"
    # print @GData[43]
    # print "\n"
    # print @GData[52]
    # print "\n"
    # print @GData[61]
    # print "\n"
    #@NeededData = Array.new
    #@NeededData = @GData
    #@rowcount = 0
    ##@colcount = 1
    #print @GData
    #@GData.each do |row|
    #  row.each do |cell|
    #    @NeededData.push(row)
    #    print "\n"
    #    print cell
    #    print "\n"
    #    print "Value at [" + @rowcount.to_s + ","+ @colcount.to_s + "]: "+ cell.to_s+ "\n"#[j]
    #    @colcount = @colcount + 1
    #  end
    #  @rowcount = @rowcount + 1
    #  @colcount = 0
    #end
    # print "\n"
    # print @GData
    # print "\n"
    # print @GData[[15,0]]
    # print "\n"
    # print @GData[[24,0]]
    # print "\n"
    # print @GData[[33,0]]
    # print "\n"
    # print @GData[[42,0]]
    # print "\n"
    # print @GData[[51,0]]
    # print "\n"
    # print @GData[[60,0]]
    # print "\n"
    # print @GData[[7,1]]
    # print "\n"
    # print @GData[[20,1]]
    # print "\n"
    # print @GData[[33,1]]
    # print "\n"
    # print @GData[[46,1]]
    # print "\n"
    # print @GData[[59,1]]
    # print "\n"
    # print @GData[[72,1]]
    # print "\n"
    # @entry1value = @GData[[7,1]].to_s
    # @entry2value = @GData[[20,1]].to_s
    # @entry3value = @GData[[33,1]].to_s
    # @entry4value = @GData[[46,1]].to_s
    # @entry5value = @GData[[59,1]].to_s
    # @entry6value = @GData[[72,1]].to_s
    # @entry1value = @GData[[15,0]].to_s
    # @entry2value = @GData[[24,0]].to_s
    # @entry3value = @GData[[33,0]].to_s
    # @entry4value = @GData[[42,0]].to_s
    # @entry5value = @GData[[51,0]].to_s
    # @entry6value = @GData[[60,0]].to_s
    @entry1value = @GData[[1,6]].to_s
    @entry2value = @GData[[2,6]].to_s
    @entry3value = @GData[[3,6]].to_s
    @entry4value = @GData[[4,6]].to_s
    @entry5value = @GData[[5,6]].to_s
    @entry6value = @GData[[6,6]].to_s
    if @NeededStatus1 == "In Progress"
      @NeededStatus1 = ["Under Review","Authorize", "Outstanding Quote", "To Be Declined", "Bound Pending Data Entry"]
    end
    if @NeededStatus2 == "In Progress"
      @NeededStatus2 = ["Under Review","Authorize", "Outstanding Quote", "To Be Declined", "Bound Pending Data Entry"]
    end
    if @NeededStatus1 == "Renewable - 6 Months"
      @NeededStatus1 = ["Bound"]
    end
    if @NeededStatus2 == "Renewable - 6 Months"
      @NeededStatus2 = ["Bound"]
    end

    @entry1NS1verify = @NeededStatus1.include? @entry1value#@GData[[15,0]].to_s
    @entry2NS1verify = @NeededStatus1.include? @entry2value#@GData[[15,0]].to_s
    @entry3NS1verify = @NeededStatus1.include? @entry3value#@GData[[15,0]].to_s
    @entry4NS1verify = @NeededStatus1.include? @entry4value#@GData[[15,0]].to_s
    @entry5NS1verify = @NeededStatus1.include? @entry5value#@GData[[15,0]].to_s
    @entry6NS1verify = @NeededStatus1.include? @entry6value#@GData[[15,0]].to_s
    @entry1NS2verify = @NeededStatus2.include? @entry1value#@GData[[15,0]].to_s
    @entry2NS2verify = @NeededStatus2.include? @entry2value#@GData[[15,0]].to_s
    @entry3NS2verify = @NeededStatus2.include? @entry3value#@GData[[15,0]].to_s
    @entry4NS2verify = @NeededStatus2.include? @entry4value#@GData[[15,0]].to_s
    @entry5NS2verify = @NeededStatus2.include? @entry5value#@GData[[15,0]].to_s
    @entry6NS2verify = @NeededStatus2.include? @entry6value#@GData[[15,0]].to_s

    @InProgressStatustoprint = "In Progress"
    if @entry1NS1verify.to_s == "true" || @entry1NS2verify.to_s == "true"
      print "PASSED - The first deal entry in the grid is of " + @InProgressStatustoprint + " substatus.\n"
    elsif @entry1value.to_s == @NeededStatus1 ||  @entry1value.to_s == @NeededStatus2
      print "PASSED - The first deal in the grid is of " + status1 + " or " + status2 + " status.\n"
    else
      print "FAILED - The first deal in the grid is not of " + status1 + " or " + status2 + " status.\n"
      fail "FAILED - The first deal in the grid is not of " + status1 + " or " + status2 + " status.\n"
    end
    if @entry2NS1verify.to_s == "true" || @entry2NS2verify.to_s == "true"
      print "PASSED - The first deal entry in the grid is of " + @InProgressStatustoprint + " substatus.\n"
    elsif @entry2value.to_s == @NeededStatus1 ||  @entry2value.to_s == @NeededStatus2
      print "PASSED - The first deal in the grid is of " + status1 + " or " + status2 + " status.\n"
    else
      print "FAILED - The first deal in the grid is not of " + status1 + " and " + status2 + " status.\n"
      fail "FAILED - The first deal in the grid is not of " + status1 + " and " + status2 + " status.\n"
    end
    if @entry3NS1verify.to_s == "true" || @entry3NS2verify.to_s == "true"
      print "PASSED - The first deal entry in the grid is of " + @InProgressStatustoprint + " substatus.\n"
    elsif @entry3value.to_s == @NeededStatus1 ||  @entry3value.to_s == @NeededStatus2
      print "PASSED - The first deal in the grid is of " + status1 + " or " + status2 + " status.\n"
    else
      print "FAILED - The first deal in the grid is not of " + status1 + " and " + status2 + " status.\n"
      fail "FAILED - The first deal in the grid is not of " + status1 + " and " + status2 + " status.\n"
    end
    if @entry4NS1verify.to_s == "true" || @entry4NS2verify.to_s == "true"
      print "PASSED - The first deal entry in the grid is of " + @InProgressStatustoprint + " substatus.\n"
    elsif @entry4value.to_s == @NeededStatus1 ||  @entry4value.to_s == @NeededStatus2
      print "PASSED - The first deal in the grid is of " + status1 + " or " + status2 + " status.\n"
    else
      print "FAILED - The first deal in the grid is not of " + status1 + " and " + status2 + " status.\n"
      fail "FAILED - The first deal in the grid is not of " + status1 + " and " + status2 + " status.\n"
    end
    if @entry5NS1verify.to_s == "true" || @entry5NS2verify.to_s == "true"
      print "PASSED - The first deal entry in the grid is of " + @InProgressStatustoprint + " substatus.\n"
    elsif @entry5value.to_s == @NeededStatus1 ||  @entry5value.to_s == @NeededStatus2
      print "PASSED - The first deal in the grid is of " + status1 + " or " + status2 + " status.\n"
    else
      print "FAILED - The first deal in the grid is not of " + status1 + " and " + status2 + " status.\n"
      fail "FAILED - The first deal in the grid is not of " + status1 + " and " + status2 + " status.\n"
    end
    if @entry6NS1verify.to_s == "true" || @entry6NS2verify.to_s == "true"
      print "PASSED - The first deal entry in the grid is of " + @InProgressStatustoprint + " substatus.\n"
    elsif @entry6value.to_s == @NeededStatus1 ||  @entry6value.to_s == @NeededStatus2
      print "PASSED - The first deal in the grid is of " + status1 + " or " + status2 + " status.\n"
    else
      print "FAILED - The first deal in the grid is not of " + status1 + " and " + status2 + " status.\n"
      fail "FAILED - The first deal in the grid is not of " + status1 + " and " + status2 + " status.\n"
    end
  end

  def findSubstatushavingnonZeroCount(status,substatus)
    @NeededSubStatusArr = substatus
    @nonZeroCountSubstatusArr = Array.new
    @nonZeroCountSubstatusArrCount = 0
    @neededstatusElement = fetchpanelElement(status)
    @StatusButtonDisabled = @neededstatusElement.attribute_value('className').include? "disabled"
    if @StatusButtonDisabled.to_s != "true"
      if @NeededSubStatusArr.length==0
        print "No Substatus available for the status " + status + ".\n"
      else
        for count in 0..@NeededSubStatusArr.length-1
          #print "\nPerforming the execution for substatus panel " + @NeededSubStatusArr[count] + ".\n"
          @ElementtobeChecked = fetchpanelSubStatusElement(@NeededSubStatusArr[count])
          @Elementexists = @ElementtobeChecked.visible?
          #if @Elementexists.to_s == "true"
          @ElementInputtobChecked = fetchpanelSubStatusElementInput(@NeededSubStatusArr[count])
          @ElementName = @NeededSubStatusArr[count]
          verifyElementExistonPage(@ElementtobeChecked,@ElementName)
          @panelcountelement = getpanelCountElement(@ElementtobeChecked)
          @panelcount = getsubstatusCount(@panelcountelement)
          if @panelcount.to_s == "0"
            print "The Substatus " +  @NeededSubStatusArr[count].to_s + " is having a count displayed as " + @panelcount + ".\n"
          elsif @panelcount.to_s != "0"
            print "The Substatus " +  @NeededSubStatusArr[count].to_s + " is having a count displayed as " + @panelcount + ".\n"
            @nonZeroCountSubstatusArr[@nonZeroCountSubstatusArrCount]=@NeededSubStatusArr[count].to_s
            @nonZeroCountSubstatusArrCount = @nonZeroCountSubstatusArrCount + 1
          end
          #end
          #VerifyElementText(@ElementtobeChecked,@NeededSubStatusArr[count])
          #verifyZerocountandDisabled(@NeededSubStatusArr[count],@panelcount,@ElementtobeChecked)
          #verifyZerocountandUnSelected(@NeededSubStatusArr[count],@panelcount,@ElementtobeChecked,@ElementInputtobChecked)
        end
      end
    else
      print "The " + status + "status panel is already disabled.\n"
    end

    return @nonZeroCountSubstatusArr
  end
  def verifyPanelDisabled(status,neededEle,pcount,nonZeroSubstatuselements)
    @NeededElement = neededEle
    @PanelCount = pcount
    @NonZeroSubStatusEle = nonZeroSubstatuselements
    @StatusButtonDisabled = @NeededElement.attribute_value('className').include? "disabled"
    # print "\n"
    # print @PanelCount
    # print "\n"
    # print @NonZeroSubStatusEle.length.to_s
    # print "\n"
    # print @StatusButtonDisabled
    # print "\n"

    if @PanelCount.to_s == "0" && @NonZeroSubStatusEle.length.to_s == "0" && @StatusButtonDisabled.to_s == "true"
      print "PASSED - The " + status + " status panel is having a count " + @PanelCount + " and the substatus if any available are having the count as zero and the " + status + " panel is in disabled status.\n"
    elsif @PanelCount.to_s == "0" && @NonZeroSubStatusEle.length.to_s != "0" && @StatusButtonDisabled.to_s == "false"
      print "PASSED - The " + status + " status panel is having a count " + @PanelCount + " and the substatus if any available are having the count as non zero and the " + status + " panel is not in disabled status.\n"
    elsif @PanelCount.to_s == "0" && @NonZeroSubStatusEle.length.to_s != "0" && @StatusButtonDisabled.to_s == "true"
      print "FAILED - The " + status + " status panel is having a count " + @PanelCount + " and the substatus if any available are having the count as zero and the " + status + " panel is in disabled status.\n"
      fail "FAILED - The " + status + " status panel is having a count " + @PanelCount + " and the substatus if any available are having the count as zero and the " + status + " panel is in disabled status.\n"
    elsif @PanelCount.to_s != "0" && @NonZeroSubStatusEle.length.to_s != "0" && @StatusButtonDisabled.to_s == "false"
      print "PASSED - The " + status + " status panel is having a count " + @PanelCount + " and the substatus if any available are having the count as non zero and the " + status + " panel is not in disabled status.\n"
    elsif @PanelCount.to_s != "0" && @NonZeroSubStatusEle.length.to_s == "0" && @StatusButtonDisabled.to_s == "false"
      print "PASSED - The " + status + " status panel is having a count " + @PanelCount + " and the substatus if any available are having the count as non zero and the " + status + " panel is not in disabled status.\n"
    elsif @PanelCount.to_s == "0" && @NonZeroSubStatusEle.length.to_s == "0" && @StatusButtonDisabled.to_s == "false"
      print "FAILED - The " + status + " status panel is having a count " + @PanelCount + " and the substatus if any available are having the count as zero and the " + status + " panel is not in disabled status.\n"
      fail "FAILED - The " + status + " status panel is having a count " + @PanelCount + " and the substatus if any available are having the count as zero and the " + status + " panel is not in disabled status.\n"
    elsif @PanelCount.to_s != "0" && @NonZeroSubStatusEle.length.to_s == "0" && @StatusButtonDisabled.to_s == "true"
      print "FAILED - The " + status + " status panel is having a count " + @PanelCount + " and the substatus if any available are having the count as zero and the " + status + " panel is in disabled status.\n"
      fail "FAILED - The " + status + " status panel is having a count " + @PanelCount + " and the substatus if any available are having the count as zero and the " + status + " panel is in disabled status.\n"
    elsif @PanelCount.to_s != "0" && @NonZeroSubStatusEle.length.to_s != "0" && @StatusButtonDisabled.to_s == "true"
      print "FAILED - The " + status + " status panel is having a count " + @PanelCount + " and the substatus if any available are having the count as non zero and the " + status + " panel is in disabled status.\n"
      fail "FAILED - The " + status + " status panel is having a count " + @PanelCount + " and the substatus if any available are having the count as non zero and the " + status + " panel is in disabled status.\n"
    end
  end
  def checkpanelnotDisabled(panelelement,status)
    @PElement = panelelement
    #@panelavailable = @PElement.exist?
    #@@PanelTextValue = @panelavailable.text.to_s
    #@@Panellabelval = @@PanelTextValue.split("\n")
    @panelDisabled = @PElement.attribute_value('className').include? 'disabled'
    if @panelDisabled.to_s == "false"
      print "PASSED - " + status +" panel is not in disabled status.\n"
      #@reporter.ReportAction("PASSED - " + @Gelement.attribute_value('id') +" grid is visible.")
    else
      print "Failed - " + status +" panel is in disabled status.\n"
      fail "Failed - " + status +" panel is in disabled status.\n"
      #@reporter.ReportAction("Failed - Grid is not visible.")
    end
  end
  def verifyPanelButtonUnChecked(statusElement,status)
    @NeededStatusElement = statusElement
    # sleep 2
    @NeededStatusElementclassName = @NeededStatusElement.attribute_value('className').to_s
    @NeededStatusElementSelected = @NeededStatusElementclassName.include? "mat-button-toggle-checked"
    if @NeededStatusElementSelected.to_s == "false"
      print "PASSED - The " + status + " status panel is not selected.\n"
    else
      print "FAILED - The " + status + " status panel is still selected.\n"
      fail "FAILED - The " + status + " status panel is still selected.\n"
    end
  end
  def verifyPanelButtonChecked(statusElement,status)
    @NeededStatusElement = statusElement
    # sleep 2
    @NeededStatusElementclassName = @NeededStatusElement.attribute_value('className').to_s
    # print "\n"
    # print @NeededStatusElementclassName
    # print "\n"
    @NeededStatusElementSelected = @NeededStatusElementclassName.include? "mat-button-toggle-checked"
    if @NeededStatusElementSelected.to_s == "true"
      print "PASSED - The " + status + " status panel is selected.\n"
    else
      print "FAILED - The " + status + " status panel is not selected.\n"
      fail "FAILED - The " + status + " status panel is not selected.\n"
    end
  end
  def verifypanelCountsUpToDate
    @Status = "In Progress,On Hold,Bound - Pending Actions,Renewable - 6 Months"
    @StatusArr = @Status.split(",")
    @dbquery = DBQueries.new
    @passedCounter = 0
    #print @StatusArr
    for @count in 0..@StatusArr.length-1
      #print "\n"
      print "Executing for the " + @StatusArr[@count].to_s + " status panel.\n"
      print "\n"
      @neededElement = fetchpanelElement(@StatusArr[@count].to_s)
      @sqlquery=@dbquery.getDBCountQuery(@StatusArr[@count].to_s).to_s
      @dbcount = sendCountQuery(@sqlquery)
      @panelcount = getpanelCount(@neededElement)
      if @panelcount==@dbcount
        print "PASSED - Counts Matched successfully. Panel Value is " + @panelcount + " and DB Value is " + @dbcount + ".\n"
        #@reporter.ReportAction("PASSED - Counts Matched successfully.\n")
        @passedCounter = @passedCounter + 1
      else
        print "FAILED - Counts failed to match. Panel Value is " + @panelcount + " and DB Value is " + @dbcount + ".\n"
        fail "FAILED - Counts failed to match. Panel Value is " + @panelcount + " and DB Value is " + @dbcount + ".\n"
        #@reporter.ReportAction("FAILED - Counts failed to match.\n")
      end
      # print "\n"
      # print @passedCounter.to_s
      # print "\n"
      # print (@StatusArr.length-1).to_s
      # print "\n"
    end
    if @passedCounter.to_s == @StatusArr.length.to_s
      print "PASSED - All the Panel counts are up to date and are matching with the counts fetched from DB.\n"
    else
      print "FAILED - All the Panel counts are not up to date and are not matching with the counts fetched from DB.\n"
      fail "FAILED - All the Panel counts are not up to date and are not matching with the counts fetched from DB.\n"
    end
  end

  def fetchTeamfield
    @teamfield = @browser.element(@@TeamButtonlocator)
    #print @teamfield
    return @teamfield
  end

  def verifyTeamfield(teamfield)
    @teamField = teamfield
    if @teamField.exist?
      print "\n Passed - Team field exists on the GRS Home page"
    else
      print "\nFailed - Team field is not appearing in the GRS Home page"
      fail "\nFailed - Team field is not appearing in the GRS Home page"
    end
  end
  def getTeamandOptionElement(team)
    case team
      when "Casualty"
        @elementlookedFor = fetchSubDivisionCasualtyelement
        return @elementlookedFor
      when "Cas Fac"
        @elementlookedFor =  fetchSubDivisionCasFacelement
        return @elementlookedFor
      when "Casualty Treaty"
        @elementlookedFor =  fetchSubDivisionCasualtyTreatyelement
        return @elementlookedFor
      when "Property"
        @elementlookedFor =  fetchSubDivisionPropertyelement
        return @elementlookedFor
      when "Intl Property"
        #print "I am here\n"
        @elementlookedFor =  fetchSubDivisionIntlPropertyelement
        return @elementlookedFor
      when "NA Property"
        @elementlookedFor =  fetchSubDivisionNAPropertyelement
        return @elementlookedFor
      when "Specialty"
        @elementlookedFor =  fetchSubDivisionSpecialtyelement
        return @elementlookedFor
      when "Specialty Non PE"
        @elementlookedFor =  fetchSubDivisionSpecialtyNonPEelement
        return @elementlookedFor
      when "Public Entity"
        @elementlookedFor =  fetchSubDivisionPublicEntityelement
        return @elementlookedFor
    end
  end


  def teamoption(team)
    @TeamName = team
    case @TeamName
      when "Casualty"
        @team =@browser.element(@@SubDivisionCasualtylocator)
        @sub1 =@browser.element(@@SubDivisionCasualtyTreatylocator)
        @sub2 =@browser.element(@@SubDivisionCasFaclocator)
        if @team.exist? && @sub1.exists? && @sub2.exists?
          return "true"
        end
      when "Property"
        @team = @browser.element(@@SubDivisionPropertylocator)
        @sub1 =@browser.element(@@SubDivisionIntlPropertylocator)
        @sub2 =@browser.element(@@SubDivisionNAPropertylocator)
        if @team.exist? && @sub1.exists? && @sub2.exists?
          return "true"
        end
      when "Specialty"
        @team = @browser.element(@@SubDivisionSpecialtylocator)
        @sub1 =@browser.element(@@SubDivisionSpecialtyNonPElocator)
        @sub2 =@browser.element(@@SubDivisionPublicEntitylocator)
        if @team.exist? && @sub1.exists? && @sub2.exists?
          return "true"
        end
    end
  end

  def verifyTeamoptions(team,sub1,sub2)
    @teamVerification = teamoption(team)
    #@browser.checkbox(:id => 'dashboard_subdivision_matChk_1-input').set(true)
    if @teamVerification == "true"
      print "\n Passed - "+ team +"Team with options "+ sub1 +" and "+ sub2+" exists.\n"
    else
      print "\n Failed - "+ team +"Team with options "+ sub1 +" and "+ sub2+" doesnot exists.\n"
      fail "\n Failed - "+ team +"Team with options "+ sub1 +" and "+ sub2+" doesnot exists.\n"
    end


  end
  begin
    def teamCheckbox(teams)
      @TeamName = teams
      case @TeamName
        when "Casualty"
          @CasualtyCheckbox = {:id => "dashboard_subdivision_matChk_0-input"}
          return @CasualtyCheckbox
        when "Casualty Treaty"
          @CasualtyTreatyCheckbox = {:id => "dashboard_subdivision_matChk_00-input"}
          return @CasualtyTreatyCheckbox
        when "Cas Fac"
          @CasFacCheckbox = {:id => "dashboard_subdivision_matChk_01-input"}
          return @CasFacCheckbox
        when "Property"
          @PropertyCheckbox = {:id => "dashboard_subdivision_matChk_1-input"}
          return @PropertyCheckbox
        when "Intl Property"
          @IntlPropertyCheckbox = {:id => "dashboard_subdivision_matChk_10-input"}
          return @IntlPropertyCheckbox
        when "NA Property"
          @NAPropertyCheckbox = {:id => "dashboard_subdivision_matChk_11-input"}
          return @NAPropertyCheckbox
        when "Specialty"
          @SpecialtyCheckbox = {:id => "dashboard_subdivision_matChk_2-input"}
          return @SpecialtyCheckbox
        when "Specialty Non-PE"
          @SpecialtyNon_PECheckbox = {:id => "dashboard_subdivision_matChk_20-input"}
          return @SpecialtyNon_PECheckbox
        when "Public Entity"
          @PublicEntityCheckbox = {:id => "dashboard_subdivision_matChk_21-input"}
          #@PublicEntityCheckbox = {:id => "dashboard_subdivision_matChk_21-input"}
          return @PublicEntityCheckbox
      end
    end
  end

  def selectTeamOptions(teams)
    @checkbox = teamCheckbox(teams)
    @browser.checkbox(@checkbox).set(true)
  end

  def verifyTeamsubOptions(sub1, sub2)
    @suboption1 = teamCheckbox(sub1)
    @suboption2 = teamCheckbox(sub2)

    @suboption1Checked = @browser.checkbox(@suboption1).set?
    @suboption2Checked = @browser.checkbox(@suboption2).set?

    if @suboption1Checked == true && @suboption2Checked == true
      print "\n Passed - Suboptions get selected by default on selecting the team.\n"
    else
      print "\n Failed - Sub options are not checked on selecting the team.\n"
      fail "\n Failed - Sub options are not checked on selecting the team.\n"
    end

  end

  def unselectTeamsubOptions(sub1)
    @suboption1 = teamCheckbox(sub1)
    @browser.checkbox(@suboption1).set(false)
  end

  def verifyUnselectTeamsubOptions(teams,sub1)
    @team = teamCheckbox(teams)
    @suboption1 = teamCheckbox(sub1)

    @teamunchecked = @browser.checkbox(@team).set?
    @suboption1unChecked = @browser.checkbox(@suboption1).set?

    if @suboption1unChecked == false && @teamunchecked ==false
      print "\n Passed - Sub-options is unselected.\n"
    else
      print "\n Failed - Sub options is not unselected.\n"
      fail "\n Failed - Sub options is not unselected.\n"
    end

  end
  @@clickoffThepopup = {:id =>"id=dashboard_subdivision_btn" }
  def selectPanel
    @panel = @browser.element(@@Inprogressbuttonlocator)
    ClickBtn(@panel)
    sleep 3
    ClickBtn(@panel)
    sleep 3
  end

  def verifySelectedTeam(teams)
    # print "\n"
    # print teams
    # print "\n"
    @team = teamCheckbox(teams)
    @elementneedtobeVisible = getTeamandOptionElement(teams)
    #@elementneeded = @browser.checkbox(@team)
    @elementVisible = @elementneedtobeVisible.present?
    if @elementVisible.to_s == "false"
      print "FAILED - The " + teams.to_s + " option and the checkbox is not visible in the Team Overlay pane.\n"
      fail "FAILED - The " + teams.to_s + " option and the checkbox is not visible in the Team Overlay pane.\n"
    else
     @teamstate = @browser.checkbox(@team).set?
     if @teamstate == true
       print "PASSED - the selection of the "+ teams.to_s + " team persists.\n"
     else
       print "FAILED - the selection of the "+ teams.to_s + " does not persist.\n"
       fail "FAILED - the selection of the "+ teams.to_s + " does not persist.\n"
     end
    end



  end

  def fetchLoginUsersName
    #@useridelement = fetchuseridelement
    @dbquery = DBQueries.new
    @sqlquery=@dbquery.getUserFullName.to_s
    @dbresult = sendQuery(@sqlquery)
    @dbresulthash = @dbresult[0]
    @dbresultval = @dbresulthash["displayName"]
    @Fullname = @dbresultval

    return @Fullname
  end

  def verifyUsersName
    #@useridelement = fetchuseridelement
    @dbquery = DBQueries.new
    @sqlquery=@dbquery.getUserFullName.to_s
    @dbresult = sendQuery(@sqlquery)
    @dbresulthash = @dbresult[0]
    @dbresultval = @dbresulthash["displayName"]
    @Fullname = @dbresultval
    sleep 2
    @NeededElementlocator = {visible_text: @Fullname}
    @NeededElement = @browser.element(@NeededElementlocator)

    if @NeededElement.exist?
      print "PASSED - The GRS Home page has the USERS Full Name displayed as " + @NeededElement.text + ".\n"
      #@reporter.ReportAction("PASSED - The GRS Home page has the USER ID displayed.")
    else
      print "FAILED - The GRS Home Page does not have the USERS Full Name displayed as " + @Fullname + ".\n"
      fail "FAILED - The GRS Home Page does not have the USERS Full Name displayed as " + @Fullname + ".\n"
      #@reporter.ReportAction("FAILED - The GRS Home Page does not have the USER ID.")
    end
  end
  def selectOperator(neededElement,operator)
    @Elementneeded = neededElement
    @Elementneeded.select operator.to_s
  end
  def inputFilterValue(textvalue)
    # print "\n"
    # print textvalue
    # print "\n"
    if (textvalue.to_s.include? "-").to_s == "true"  #&& textvalue.to_s != textvalue
      print "\nI am here\n"
      @splitteddate = textvalue.to_s.split("-")
      y = @splitteddate[0]
      m = @splitteddate[1]
      d = @splitteddate[2]
      # print "\n"
      # print y
      # print "\n"
      # print m
      # print "\n"
      # print d
      if Date.valid_date?(y.to_i, m.to_i, d.to_i)
        print " \nI am here too\n"
        @neededDateInputFilterElement = fetchGridAgColumnFilterDateInputElement
        @neededDateInputFilterElement.click
        @neededDateInputFilterElement.send_keys textvalue
      else
        @neededtextInputFilterElement = fetchGridAgColumnFilterTextInputElement
        @neededtextInputFilterElement.click
        @neededtextInputFilterElement.send_keys textvalue
      end
    else
      @neededtextInputFilterElement = fetchGridAgColumnFilterTextInputElement
      @neededtextInputFilterElement.click
      @neededtextInputFilterElement.send_keys textvalue
      #@neededElement.send_keys m
      #@neededElement.send_keys d
    end
  end
  def is_number?
    self.to_f == self
  end

  def verifyGridDataWithFilterResults(gData,gridColNames,columnname,textvalue)
    @dealgriddata = griddata
    @possibleRows = 1
    @output = ""
    @iterator = 0
    if @dealgriddata[[1, 0]].to_s != "" || @dealgriddata[[1, 1]].to_s != ""
      for @col in 0..gridColNames.length-1
        if @dealgriddata[[0, @col]].to_s  == columnname.to_s

        end
      end
    end

  end
  
  
  def verifyGridDataIsAsperFilter(gData,columnname,textvalue,operator)
    @GridData = gData
    # print "\n"
    # print @GridData
    # print "\n"
    if (textvalue.to_s.include? "-").to_s == "true"  #&& textvalue.to_s != textvalue
      #d, m, y = textvalue.split '-'
      @splitteddate = textvalue.to_s.split("-")
      y = @splitteddate[0]
      m = @splitteddate[1]
      d = @splitteddate[2]
      # print "\n"
      # print y
      # print "\n"
      # print m
      # print "\n"
      # print d
      if Date.valid_date?(y.to_i, m.to_i, d.to_i) #textvalue.valid_date? y.to_i, m.to_i, d.to_i
        @textvaluetype == "date"
        @datevalue = m.to_s + d.to_s + y.to_s
      end
    #elsif textvalue.is_number?    #is_a? Numeric #.to_i == textvalue
    #  @textvaluetype = "number"
    else #if textvalue.is_string? #.to_s == textvalue
      @textvaluetype = "string"
    end
    case columnname
      when "Deal Name"
        # @verifyvalue1 = @GridData[[1,1]]
        # @verifyvalue2 = @GridData[[14,1]]
        # @verifyvalue3 = @GridData[[27,1]]
        # @verifyvalue4 = @GridData[[40,1]]
        # @verifyvalue5 = @GridData[[53,1]]
        # @verifyvalue6 = @GridData[[66,1]]
        @verifyvalue1 = @GridData[[1,0]]
        @verifyvalue2 = @GridData[[2,0]]
        @verifyvalue3 = @GridData[[3,0]]
        @verifyvalue4 = @GridData[[4,0]]
        @verifyvalue5 = @GridData[[5,0]]
        @verifyvalue6 = @GridData[[6,0]]
        if operator.to_s == "Equals"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
            #$scenario.puts "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s==textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s==textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s==textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s==textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s==textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s==textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
            #$scenario.puts "PASSED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s!=textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s!=textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s!=textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s!=textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s!=textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s!=textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
            #$scenario.puts "FAILED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
          end
        elsif operator.to_s == "Not equal"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s==textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s==textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s==textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s==textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s==textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s==textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s!=textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s!=textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s!=textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s!=textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s!=textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s!=textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
          end
        elsif operator.to_s == "Contains" && @textvaluetype == "string"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s.include? textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s.include? textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s.include? textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s.include? textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s.include? textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s.include? textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed contains the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed does not contain the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed does not contain the value " + textvalue + " in the " + columnname + " column.\n"
          end
        elsif operator.to_s == "Not contains" && @textvaluetype == "string"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s.include? textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s.include? textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s.include? textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s.include? textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s.include? textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s.include? textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed contains the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed contains the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed does not contain the value " + textvalue + " in the " + columnname + " column.\n"
          end
        end

      when "Contract"
        # @verifyvalue1 = @GridData[[2,1]]
        # @verifyvalue2 = @GridData[[15,1]]
        # @verifyvalue3 = @GridData[[28,1]]
        # @verifyvalue4 = @GridData[[41,1]]
        # @verifyvalue5 = @GridData[[54,1]]
        # @verifyvalue6 = @GridData[[67,1]]
        @verifyvalue1 = @GridData[[1,1]]
        @verifyvalue2 = @GridData[[2,1]]
        @verifyvalue3 = @GridData[[3,1]]
        @verifyvalue4 = @GridData[[4,1]]
        @verifyvalue5 = @GridData[[5,1]]
        @verifyvalue6 = @GridData[[6,1]]
        if operator.to_s == "Equals"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s==textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s==textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s==textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s==textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s==textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s==textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s!=textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s!=textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s!=textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s!=textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s!=textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s!=textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
          end
        elsif operator.to_s == "Not equal"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s==textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s==textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s==textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s==textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s==textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s==textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s!=textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s!=textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s!=textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s!=textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s!=textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s!=textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
          end
        elsif operator.to_s == "Contains"
          print "Not a valid operator for the " + columnname + " column.\n"
        elsif operator.to_s == "Not contains"
          print "Not a valid operator for the " + columnname + " column.\n"
        end
      when "Inception"
        # @verifyvalue1 = @GridData[[3,1]]
        # @verifyvalue2 = @GridData[[16,1]]
        # @verifyvalue3 = @GridData[[29,1]]
        # @verifyvalue4 = @GridData[[42,1]]
        # @verifyvalue5 = @GridData[[55,1]]
        # @verifyvalue6 = @GridData[[68,1]]
        @verifyvalue1 = @GridData[[1,2]]
        @verifyvalue2 = @GridData[[2,2]]
        @verifyvalue3 = @GridData[[3,2]]
        @verifyvalue4 = @GridData[[4,2]]
        @verifyvalue5 = @GridData[[5,2]]
        @verifyvalue6 = @GridData[[6,2]]
        if operator.to_s == "Equals"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s==@datevalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s==@datevalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s==@datevalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s==@datevalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s==@datevalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s==@datevalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s!=@datevalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s!=@datevalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s!=@datevalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s!=@datevalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s!=@datevalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s!=@datevalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
          end
        elsif operator.to_s == "Not equal"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s==@datevalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s==textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s==textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s==textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s==textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s==textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s!=@datevalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s!=textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s!=textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s!=textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s!=textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s!=textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
          end
        elsif operator.to_s == "Contains"
          print "Not a valid operator for the " + columnname + " column.\n"
        elsif operator.to_s == "Not contains"
          print "Not a valid operator for the " + columnname + " column.\n"
        end
      when "Target Date"
        # @verifyvalue1 = @GridData[[4,1]]
        # @verifyvalue2 = @GridData[[17,1]]
        # @verifyvalue3 = @GridData[[30,1]]
        # @verifyvalue4 = @GridData[[43,1]]
        # @verifyvalue5 = @GridData[[56,1]]
        # @verifyvalue6 = @GridData[[69,1]]
        @verifyvalue1 = @GridData[[1,3]]
        @verifyvalue2 = @GridData[[2,3]]
        @verifyvalue3 = @GridData[[3,3]]
        @verifyvalue4 = @GridData[[4,3]]
        @verifyvalue5 = @GridData[[5,3]]
        @verifyvalue6 = @GridData[[6,3]]
        if operator.to_s == "Equals"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s==@datevalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s==@datevalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s==@datevalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s==@datevalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s==@datevalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s==@datevalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s!=@datevalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s!=@datevalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s!=@datevalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s!=@datevalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s!=@datevalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s!=@datevalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
          end
        elsif operator.to_s == "Not equal"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s==@datevalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s==@datevalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s==@datevalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s==@datevalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s==@datevalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s==@datevalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s!=@datevalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s!=@datevalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s!=@datevalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s!=@datevalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s!=@datevalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s!=@datevalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
          end
        elsif operator.to_s == "Contains"
          print "Not a valid operator for the " + columnname + " column.\n"
        elsif operator.to_s == "Not contains"
          print "Not a valid operator for the " + columnname + " column.\n"
        end
      when "Priority"
        # @verifyvalue1 = @GridData[[5,1]]
        # @verifyvalue2 = @GridData[[18,1]]
        # @verifyvalue3 = @GridData[[31,1]]
        # @verifyvalue4 = @GridData[[44,1]]
        # @verifyvalue5 = @GridData[[57,1]]
        # @verifyvalue6 = @GridData[[70,1]]
        @verifyvalue1 = @GridData[[1,4]]
        @verifyvalue2 = @GridData[[2,4]]
        @verifyvalue3 = @GridData[[3,4]]
        @verifyvalue4 = @GridData[[4,4]]
        @verifyvalue5 = @GridData[[5,4]]
        @verifyvalue6 = @GridData[[6,4]]
        if operator.to_s == "Equals"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s==textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s==textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s==textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s==textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s==textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s==textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s!=textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s!=textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s!=textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s!=textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s!=textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s!=textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
          end
        elsif operator.to_s == "Not equal"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s==textvalue.to_s) && (@verifyvalue2.to_s==textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s==textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s==textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s==textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s==textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s!=textvalue.to_s) && (@verifyvalue2.to_s!=textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s!=textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s!=textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s!=textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s!=textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
          end
        elsif operator.to_s == "Contains"
          print "Not a valid operator for the " + columnname + " column.\n"
        elsif operator.to_s == "Not contains"
          print "Not a valid operator for the " + columnname + " column.\n"
        end
      when "Submitted"
        # @verifyvalue1 = @GridData[[6,1]]
        # @verifyvalue2 = @GridData[[19,1]]
        # @verifyvalue3 = @GridData[[32,1]]
        # @verifyvalue4 = @GridData[[45,1]]
        # @verifyvalue5 = @GridData[[58,1]]
        # @verifyvalue6 = @GridData[[71,1]]
        @verifyvalue1 = @GridData[[1,5]]
        @verifyvalue2 = @GridData[[2,5]]
        @verifyvalue3 = @GridData[[3,5]]
        @verifyvalue4 = @GridData[[4,5]]
        @verifyvalue5 = @GridData[[5,5]]
        @verifyvalue6 = @GridData[[6,5]]
        if operator.to_s == "Equals"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s==@datevalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s==@datevalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s==@datevalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s==@datevalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s==@datevalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s==@datevalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s!=@datevalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s!=@datevalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s!=@datevalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s!=@datevalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s!=@datevalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s!=@datevalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
          end
        elsif operator.to_s == "Not equal"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s==@datevalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s==@datevalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s==@datevalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s==@datevalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s==@datevalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s==@datevalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s!=@datevalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s!=@datevalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s!=@datevalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s!=@datevalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s!=@datevalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s!=@datevalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
          end
        elsif operator.to_s == "Contains"
          print "Not a valid operator for the " + columnname + " column.\n"
        elsif operator.to_s == "Not contains"
          print "Not a valid operator for the " + columnname + " column.\n"
        end
      when "Status"
        print "Not a valid option for Status.\n"
      when "Deal Number"
        # @verifyvalue1 = @GridData[[8,1]]
        # @verifyvalue2 = @GridData[[22,1]]
        # @verifyvalue3 = @GridData[[34,1]]
        # @verifyvalue4 = @GridData[[47,1]]
        # @verifyvalue5 = @GridData[[60,1]]
        # @verifyvalue6 = @GridData[[73,1]]
        @verifyvalue1 = @GridData[[1,7]]
        @verifyvalue2 = @GridData[[2,7]]
        @verifyvalue3 = @GridData[[3,7]]
        @verifyvalue4 = @GridData[[4,7]]
        @verifyvalue5 = @GridData[[5,7]]
        @verifyvalue6 = @GridData[[6,7]]
        if operator.to_s == "Equals"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s==textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s==textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s==textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s==textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s==textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s==textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s!=textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s!=textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s!=textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s!=textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s!=textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s!=textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
          end
        elsif operator.to_s == "Not equal"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s==textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s==textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s==textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s==textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s==textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s==textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s!=textvalue.to_s) && (@verifyvalue2.to_s!=textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s!=textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s!=textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s!=textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s!=textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
          end
        elsif operator.to_s == "Contains" && @textvaluetype == "string"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s.include? textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s.include? textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s.include? textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s.include? textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s.include? textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s.include? textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed contains the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed does not contain the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed does not contain the value " + textvalue + " in the " + columnname + " column.\n"
          end
        elsif operator.to_s == "Not contains" && @textvaluetype == "string"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s.include? textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s.include? textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s.include? textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s.include? textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s.include? textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s.include? textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed contains the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed contains the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed does not contain the value " + textvalue + " in the " + columnname + " column.\n"
          end
        end
      when "Underwriter"
        # @verifyvalue1 = @GridData[[9,1]]
        # @verifyvalue2 = @GridData[[23,1]]
        # @verifyvalue3 = @GridData[[34,1]]
        # @verifyvalue4 = @GridData[[48,1]]
        # @verifyvalue5 = @GridData[[61,1]]
        # @verifyvalue6 = @GridData[[74,1]]
        @verifyvalue1 = @GridData[[1,8]]
        @verifyvalue2 = @GridData[[2,8]]
        @verifyvalue3 = @GridData[[3,8]]
        @verifyvalue4 = @GridData[[4,8]]
        @verifyvalue5 = @GridData[[5,8]]
        @verifyvalue6 = @GridData[[6,8]]

        if operator.to_s == "Equals"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s==textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s==textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s==textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s==textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s==textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s==textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s!=textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s!=textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s!=textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s!=textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s!=textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s!=textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
          end
        elsif operator.to_s == "Not equal"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s==textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s==textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s==textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s==textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s==textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s==textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s!=textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s!=textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s!=textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s!=textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s!=textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s!=textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
          end
        elsif operator.to_s == "Contains" && @textvaluetype == "string"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s.include? textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s.include? textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s.include? textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s.include? textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s.include? textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s.include? textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed contains the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed does not contain the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed does not contain the value " + textvalue + " in the " + columnname + " column.\n"
          end
        elsif operator.to_s == "Not contains" && @textvaluetype == "string"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s.include? textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s.include? textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s.include? textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s.include? textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s.include? textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s.include? textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed contains the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed contains the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed does not contain the value " + textvalue + " in the " + columnname + " column.\n"
          end
        end

      when "Underwriter 2"
        # @verifyvalue1 = @GridData[[10,1]]
        # @verifyvalue2 = @GridData[[24,1]]
        # @verifyvalue3 = @GridData[[35,1]]
        # @verifyvalue4 = @GridData[[49,1]]
        # @verifyvalue5 = @GridData[[62,1]]
        # @verifyvalue6 = @GridData[[75,1]]
        @verifyvalue1 = @GridData[[1,9]]
        @verifyvalue2 = @GridData[[2,9]]
        @verifyvalue3 = @GridData[[3,9]]
        @verifyvalue4 = @GridData[[4,9]]
        @verifyvalue5 = @GridData[[5,9]]
        @verifyvalue6 = @GridData[[6,9]]
        if operator.to_s == "Equals"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s==textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s==textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s==textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s==textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s==textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s==textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s!=textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s!=textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s!=textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s!=textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s!=textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s!=textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
          end
        elsif operator.to_s == "Not equal"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s==textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s==textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s==textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s==textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s==textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s==textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s!=textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s!=textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s!=textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s!=textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s!=textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s!=textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
          end
        elsif operator.to_s == "Contains" && @textvaluetype == "string"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s.include? textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s.include? textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s.include? textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s.include? textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s.include? textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s.include? textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed contains the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed does not contain the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed does not contain the value " + textvalue + " in the " + columnname + " column.\n"
          end
        elsif operator.to_s == "Not contains" && @textvaluetype == "string"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s.include? textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s.include? textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s.include? textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s.include? textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s.include? textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s.include? textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed contains the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed contains the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed does not contain the value " + textvalue + " in the " + columnname + " column.\n"
          end
        end
      when "TA"
        # @verifyvalue1 = @GridData[[11,1]]
        # @verifyvalue2 = @GridData[[25,1]]
        # @verifyvalue3 = @GridData[[36,1]]
        # @verifyvalue4 = @GridData[[50,1]]
        # @verifyvalue5 = @GridData[[63,1]]
        # @verifyvalue6 = @GridData[[76,1]]
        @verifyvalue1 = @GridData[[1,10]]
        @verifyvalue2 = @GridData[[2,10]]
        @verifyvalue3 = @GridData[[3,10]]
        @verifyvalue4 = @GridData[[4,10]]
        @verifyvalue5 = @GridData[[5,10]]
        @verifyvalue6 = @GridData[[6,10]]
        if operator.to_s == "Equals"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s==textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s==textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s==textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s==textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s==textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s==textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s!=textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s!=textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s!=textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s!=textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s!=textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s!=textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
          end
        elsif operator.to_s == "Not equal"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s==textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s==textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s==textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s==textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s==textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s==textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s!=textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s!=textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s!=textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s!=textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s!=textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s!=textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
          end
        elsif operator.to_s == "Contains" && @textvaluetype == "string"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s.include? textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s.include? textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s.include? textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s.include? textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s.include? textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s.include? textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed contains the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed does not contain the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed does not contain the value " + textvalue + " in the " + columnname + " column.\n"
          end
        elsif operator.to_s == "Not contains" && @textvaluetype == "string"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s.include? textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s.include? textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s.include? textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s.include? textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s.include? textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s.include? textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed contains the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed contains the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed does not contain the value " + textvalue + " in the " + columnname + " column.\n"
          end
        end
      when "Modeler"
        # @verifyvalue1 = @GridData[[12,1]]
        # @verifyvalue2 = @GridData[[26,1]]
        # @verifyvalue3 = @GridData[[37,1]]
        # @verifyvalue4 = @GridData[[51,1]]
        # @verifyvalue5 = @GridData[[64,1]]
        # @verifyvalue6 = @GridData[[77,1]]
        @verifyvalue1 = @GridData[[1,11]]
        @verifyvalue2 = @GridData[[2,11]]
        @verifyvalue3 = @GridData[[3,11]]
        @verifyvalue4 = @GridData[[4,11]]
        @verifyvalue5 = @GridData[[5,11]]
        @verifyvalue6 = @GridData[[6,11]]
        if operator.to_s == "Equals"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s==textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s==textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s==textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s==textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s==textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s==textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s!=textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s!=textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s!=textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s!=textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s!=textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s!=textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
          end
        elsif operator.to_s == "Not equal"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s==textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s==textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s==textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s==textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s==textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s==textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s!=textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s!=textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s!=textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s!=textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s!=textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s!=textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
          end
        elsif operator.to_s == "Contains" && @textvaluetype == "string"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s.include? textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s.include? textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s.include? textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s.include? textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s.include? textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s.include? textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed contains the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed does not contain the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed does not contain the value " + textvalue + " in the " + columnname + " column.\n"
          end
        elsif operator.to_s == "Not contains" && @textvaluetype == "string"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s.include? textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s.include? textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s.include? textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s.include? textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s.include? textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s.include? textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed contains the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed contains the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed does not contain the value " + textvalue + " in the " + columnname + " column.\n"
          end
        end
      when "Actuary"
        # @verifyvalue1 = @GridData[[13,1]]
        # @verifyvalue2 = @GridData[[27,1]]
        # @verifyvalue3 = @GridData[[38,1]]
        # @verifyvalue4 = @GridData[[52,1]]
        # @verifyvalue5 = @GridData[[65,1]]
        # @verifyvalue6 = @GridData[[78,1]]
        @verifyvalue1 = @GridData[[1,12]]
        @verifyvalue2 = @GridData[[2,12]]
        @verifyvalue3 = @GridData[[3,12]]
        @verifyvalue4 = @GridData[[4,12]]
        @verifyvalue5 = @GridData[[5,12]]
        @verifyvalue6 = @GridData[[6,12]]
        if operator.to_s == "Equals"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s==textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s==textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s==textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s==textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s==textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s==textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s!=textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s!=textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s!=textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s!=textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s!=textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s!=textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
          end
        elsif operator.to_s == "Not equal"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s==textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s==textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s==textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s==textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s==textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s==textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s!=textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s!=textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s!=textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s!=textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s!=textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s!=textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
          end
        elsif operator.to_s == "Contains" && @textvaluetype == "string"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s.include? textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s.include? textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s.include? textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s.include? textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s.include? textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s.include? textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed contains the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed does not contain the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed does not contain the value " + textvalue + " in the " + columnname + " column.\n"
          end
        elsif operator.to_s == "Not contains" && @textvaluetype == "string"
          if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
            print "No Data visible in the grid.\n"
          elsif (@verifyvalue1.to_s.include? textvalue.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s.include? textvalue.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s.include? textvalue.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s.include? textvalue.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s.include? textvalue.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s.include? textvalue.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "FAILED - Data displayed contains the value " + textvalue + " in the " + columnname + " column.\n"
            fail "FAILED - Data displayed contains the value " + textvalue + " in the " + columnname + " column.\n"
          elsif (@verifyvalue1.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s.include?(textvalue.to_s).to_s == "false" || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
            print "PASSED - Data displayed does not contain the value " + textvalue + " in the " + columnname + " column.\n"
          end
        end
    end

  end
  def verifyGridDataIsAsperFilterSelection(gridData,columnname,selection)
    @GridData = gridData
    # print "\n"
    # print @GridData
    # print "\n"

    # @verifyvalue1 = @GridData[[7,1]]
    # @verifyvalue2 = @GridData[[20,1]]
    # @verifyvalue3 = @GridData[[33,1]]
    # @verifyvalue4 = @GridData[[46,1]]
    # @verifyvalue5 = @GridData[[59,1]]
    # @verifyvalue6 = @GridData[[72,1]]
    @rowcount = 0
    case columnname
      when "Status"
        @colcount = 6
    end
    @verifyvalue1 = @GridData[[@rowcount,@colcount]]
    @verifyvalue2 = @GridData[[@rowcount+1,@colcount]]
    @verifyvalue3 = @GridData[[@rowcount+2,@colcount]]
    @verifyvalue4 = @GridData[[@rowcount+3,@colcount]]
    @verifyvalue5 = @GridData[[@rowcount+4,@colcount]]
    @verifyvalue6 = @GridData[[@rowcount+5,@colcount]]
    if (@verifyvalue1.to_s == "nil" || @verifyvalue1.to_s == "") && (@verifyvalue2.to_s == "nil" || @verifyvalue2.to_s == "")
      print "No Data visible in the grid.\n"
      #$scenario.puts "No Data visible in the grid.\n"
    elsif (@verifyvalue1.to_s==selection.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s==selection.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s==selection.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s==selection.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s==selection.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s==selection.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
      print "PASSED - Data displayed is having the value " + selection + " in the " + columnname + " column.\n"
      #$scenario.puts "PASSED - Data displayed is having the value " + textvalue + " in the " + columnname + " column.\n"
    elsif (@verifyvalue1.to_s!=selection.to_s || @verifyvalue1.to_s=="nil" || @verifyvalue1.to_s=="") && (@verifyvalue2.to_s!=selection.to_s || @verifyvalue2.to_s=="nil" || @verifyvalue2.to_s=="") && (@verifyvalue3.to_s!=selection.to_s || @verifyvalue3.to_s=="nil" || @verifyvalue3.to_s=="") && (@verifyvalue4.to_s!=selection.to_s || @verifyvalue4.to_s=="nil" || @verifyvalue4.to_s=="") && (@verifyvalue5.to_s!=selection.to_s || @verifyvalue5.to_s=="nil" || @verifyvalue5.to_s=="") && (@verifyvalue6.to_s!=selection.to_s || @verifyvalue6.to_s=="nil" || @verifyvalue6.to_s=="")
      print "FAILED - Data displayed is not having the value " + selection + " in the " + columnname + " column.\n"
      fail "FAILED - Data displayed is not having the value " + selection + " in the " + columnname + " column.\n"
      #$scenario.puts "FAILED - Data displayed is not having the value " + textvalue + " in the " + columnname + " column.\n"
    end
  end
  def verifyUserTeamSelection(user)
    dbQueries = DBQueries.new
    @sqlQuery = dbQueries.getPersonProfile(user)
    @queryResult = BrowserContainer.ExecuteQuery(@sqlQuery)
    @parsedResult = JSON.parse(@queryResult.to_json)
    @usersubDivision = @parsedResult[0]["subDivisions"]
    #@usersubDivision = @usersubDivision.gsub(" \"","\"")
    @userSubDivisionArr = @usersubDivision.split(",")

    print "The selected " + user + " user has the access to following subdivisions " + @userSubDivisionArr.to_s + ".\n"
    # print "\n"
    #print @queryResult
    # print "\n"
    # print @userSubDivisionArr
    # print "\n"
    #@Neededteams =
    #teamoption()
    for @count in 0..@userSubDivisionArr.length-1
      @userSubDivisionArrval = @userSubDivisionArr[@count].to_s.lstrip
      @userSubDivisionArrval = @userSubDivisionArrval.rstrip
      verifySelectedTeam(@userSubDivisionArrval.to_s)
    end
  end
  def modifyDBResult(dbresult)
    @dbresult = dbresult.to_s
    @modifiedDBResult = @dbresult.gsub(/"priority"=>nil/, "\"priority\"=>0")
    @modifiedDBResult = @modifiedDBResult.to_s.gsub(/  /," ")
    # print "\n######################"
    # print @modifiedDBResult
    # print "####################\n"
    return @modifiedDBResult
  end
  def handleIESAVEASdownloadPopup
    @tTime = Time.now
    @download_directory = File.join(File.absolute_path('../', File.dirname(__FILE__)),"GRSExcelExportFiles")
    #@autoitpopup = WIN32OLE.new("AutoItX3.Control")
    @filename = @tTime.strftime("%H%M%S").to_s + ".xls"
    @report_name = File.join(@download_directory,@filename)
    @wsh = WIN32OLE.new('Wscript.Shell')
    @wsh.SendKeys("{ENTER}")
    sleep 5
    @wsh.SendKeys("{ENTER}")
    # hwnd = ie.enabled_popup(30)  # get a handle if one exists
    # if (hwnd)  # yes there is a popup
    #   popup = WinClicker.new
    #   popup.makeWindowActive(hwnd)
    #   popup.clickWindowsButton("File Download", "Save", maxWaittime = 30)
    # end
    # Handle the additional popups
    #@autoitpopup.WinWaitActive("Save As")
    #@autoitpopup.Send(@report_name)
    #@autoitpopup.Send("{ENTER}")
    begin

    rescue Exception => e
      puts "Trapped Error, expecting modal dialog exception"
      puts e.backtrace
      puts "Continuing"
    end
    # if @browser.alert.exists?
    #   # @wsh = WIN32OLE.new('Wscript.Shell')
    #   # @wsh.SendKeys('sjanardanannair')
    #   # @wsh.SendKeys('{TAB}')
    #   # @wsh.SendKeys('Summer18')
    #   print "I am here.\n"
    #   @browser.alert.OK
    # end
    # popup_clicker('Save File')
    # @browser.send_keys(@report_name.to_s)
    #popup_clicker('OK')

    #@browser.popup(:visible_text, 'Save as').click
    #@browser.SendKeys(@report_name)
    #@browser.alert.ok #SendKeys("{ENTER}")
    # print "\n"
    # print @browser.popup.text
    # print "\n"
    # @saveprompt = @browser.prompt("Opening export.xls") do
    #   @browser.button('Save File').click
    # end
    # @browser.SendKeys(@report_name.to_s)
    # @browser.button('OK').click

  end


  def deleteexportfiles(firefoxExportFolder)
    @tobedeletedfiles = firefoxExportFolder.to_s + "\\export*.xls'"
    FileUtils.remove_file(@tobedeletedfiles,force = true)
    #File.delete(@tobedeletedfiles.to_s)
  end
  # def popup_clicker(text)
  #   begin
  #     #Timeout::timeout 2 , PopupTimeout do
  #       if @browser.enabled_popup
  #         hwnd = @browser.enabled_popup(5)
  #         w = WinClicker.new
  #         w.makeWindowActive(hwnd)
  #         w.clickWindowsButton_hwnd(hwnd,text)
  #       end
  #     #end
  #   #rescue PopupTimeout
  #     # Do this line if you can't find a popup
  #   end
  #   @browser.wait
  # end
  def filtergridByDealnumber(dealnumber)
    @columnname = "Deal Number"
    @operator = "Equals"
    @textvalue = dealnumber
    @fieldElement = fetchGridFieldElement(@columnname)
    @ElementToBEClicked = fetchMenuFieldElement(@fieldElement)
    ClickGridCol(@ElementToBEClicked)
    @ParentofElementtobeClicked = fetchToolMenuHeaderelement
    @ElementToBEClicked = @ParentofElementtobeClicked.span(index:2)
    ClickGridCol(@ElementToBEClicked)
    @neededElement = fetchGridAgColumnFilterElement
    selectOperator(@neededElement,@operator)
    @neededElement = fetchGridAgColumnFilterElement
    selectOperator(@neededElement,@operator)
    inputFilterValue(@textvalue)
    @neededapplyelement = fetchGridAgColumnFilterApplyButtonElement
    ClickGridCol(@neededapplyelement)
    @dealgridColumns = @grshomepg.fetchDealGridColumns   #boya Added
    @dealGridColCount  = @dealgridColumns.length   # Boya Added
    @dealgrid = fetchDealGridtable
    @GridData = getGridValues(@dealgrid,@dealGridColCount)
    verifyGridDataIsAsperFilter(@GridData,@columnname,@textvalue,@operator)
    @fieldElement = fetchGridFieldElement(@columnname)
    @ElementToBEClicked = fetchMenuFieldElement(@fieldElement)
    ClickGridCol(@ElementToBEClicked)
  end
  def clickdealrow(dealnumber,dataavailability)
    if dataavailability == 1
      @fetchelement = FetchElementDetails.new
      @neededRowElementClass = @fetchelement.getAgGridRowclass
      @neededCellElementLocator = {:class => /#{@neededRowElementClass}/, :visible_text => /#{dealnumber}/}
      @neededCellElement = @browser.element(@neededCellElementLocator)
      ClickGridCol(@neededCellElement)
    else
      print "Skipping the step as there is no data visible in the GRID.\n"
    end

  end
  def verifynonEditableFields(fields,editable,dataavailability)
    if dataavailability == 1
      @fieldsarray = fields.to_s.split(',')
      for @count in 0..@fieldsarray.length-1
        @neededFieldElement = fetchEditableFormElement(@fieldsarray[@count].to_s)
        #sleep
        if @neededFieldElement.enabled? == false && editable == "0"
          print "PASSED - The field #{@fieldsarray[@count]} is expected to be a non editable field and is disabled for editing.\n"
        elsif @neededFieldElement.enabled? && editable == "1"
          print "PASSED - The field #{@fieldsarray[@count]} is expected to be a editable field and is enabled for editing.\n"
        elsif @neededFieldElement.enabled? && editable == "0"
          print "FAILED - The field #{@fieldsarray[@count]} is expected to be a non editable field and is enabled for editing.\n"
          fail "FAILED - The field #{@fieldsarray[@count]} is expected to be a non editable field and is enabled for editing.\n"
        elsif @neededFieldElement.enabled? == false && editable == "1"
          print "FAILED - The field #{@fieldsarray[@count]} is expected to be a editable field and is disabled for editing.\n"
          fail "FAILED - The field #{@fieldsarray[@count]} is expected to be a editable field and is disabled for editing.\n"
        end
      end
    else
      print "Skipping the step as there is no data visible in the GRID.\n"
    end
  end
  def updateeditablefields(user,fields,dataavailability)
    @fetchelement = FetchElementDetails.new
    @editablegridsubmitButton = fetchEditableDealFormSubmitButtonElement
    if dataavailability == 1 && user != "Read Only Access"
      #@fieldstobeUpdatedArr = [dealname.to_s,targetdate.to_s,priority.to_s, status.to_s,primaryUnderwriter.to_s,secondaryUnderwriter.to_s,ta.to_s,modeler.to_s,actuary.to_s]
      @EditableDealFormmatoptionclass = @fetchelement.getEditableDealFormmatoptionclass
      @fieldstobeUpdatedArr = fields.to_s.split('*')
      # print "\n"
      # print @fieldstobeUpdatedArr
      # print "\n"
      @dealname = @fieldstobeUpdatedArr[0]
      @targetdate = @fieldstobeUpdatedArr[1]
      @priority = @fieldstobeUpdatedArr[2]
      @status = @fieldstobeUpdatedArr[3]
      @primaryUnderwriter = @fieldstobeUpdatedArr[4]
      @secondaryUnderwriter = @fieldstobeUpdatedArr[5]
      @ta = @fieldstobeUpdatedArr[6]
      @modeler = @fieldstobeUpdatedArr[7]
      @actuary = @fieldstobeUpdatedArr[8]
      @dealnameeditableelement = fetchEditableDealFormDealNameElement
      @targetdateeeditableelement = fetchEditableDealFormTargetDateElement
      @priorityeditableelement = fetchEditableDealFormPriorityElement
      @statuseditableelement = fetchEditableDealFormDealStatusElement
      @primaryUnderwritereditableelement = fetchEditableDealFormUnderwritterElement
      @secondaryUnderwritereditableelement = fetchEditableDealFormUnderwritter2Element
      @technicalAssteditableelement = fetchEditableDealFormTechAsstElement
      @modelereditableelement = fetchEditableDealFormModelerElement
      @actuaryeditableelement = fetchEditableDealFormActuaryElement
      @dealnameeditableelement.set(@dealname.to_s)
      @targetdateeeditableelement.set(@targetdate.to_s)
      @priorityeditableelement.set(@priority.to_s)
      @statuseditableelement.click
      #@browser.send_keys(@status.to_s)
      @statuseditableoptionelementlocator = {:class => /#{@EditableDealFormmatoptionclass}/,:visible_text => /#{@status.to_s}/}
      @statuseditableoptionelement = @browser.span(@statuseditableoptionelementlocator)
      @statuseditableoptionelement.click
      #@statuseditableelement.set(@status.to_s)
      @primaryUnderwritereditableelement.click
      @primaryUnderwritereditableoptionelementlocator = {:class => /#{@EditableDealFormmatoptionclass}/,:visible_text => /#{@primaryUnderwriter.to_s}/}
      @primaryUnderwritereditableoptionelement = @browser.span(@primaryUnderwritereditableoptionelementlocator)
      @primaryUnderwritereditableoptionelement.click


      #@primaryUnderwritereditableelement.option(:text => /#{@primaryUnderwriter.to_s}/).click
      #@browser.select({:visible_text => @primaryUnderwriter.to_s}).select
      @secondaryUnderwritereditableelement.click
      @secondaryUnderwritereditableoptionelementlocator = {:class => /#{@EditableDealFormmatoptionclass}/,:visible_text => /#{@secondaryUnderwriter.to_s}/}
      @secondaryUnderwritereditableoptionelement = @browser.span(@secondaryUnderwritereditableoptionelementlocator)
      @secondaryUnderwritereditableoptionelement.click


      #@secondaryUnderwritereditableelement.option(:text => /#{@secondaryUnderwriter.to_s}/).click
      #@browser.select_list({:visible_text => @secondaryUnderwriter.to_s}).select
      @technicalAssteditableelement.click
      @technicalAssteditableoptionelementlocator = {:class => /#{@EditableDealFormmatoptionclass}/,:visible_text => /#{@ta.to_s}/}
      @technicalAssteditableoptionelement = @browser.span(@technicalAssteditableoptionelementlocator)
      @technicalAssteditableoptionelement.click

      #@technicalAssteditableelement.option(:text => /#{@ta.to_s}/).click
      #@browser.select_list({:visible_text => @ta.to_s}).select
      @modelereditableelement.click
      @modelereditableoptionelementlocator = {:class => /#{@EditableDealFormmatoptionclass}/,:visible_text => /#{@modeler.to_s}/}
      @modelereditableoptionelement = @browser.span(@modelereditableoptionelementlocator)
      @modelereditableoptionelement.click

      #@modelereditableelement.option(:text => /#{@modeler.to_s}/).click
      #@browser.select_list({:visible_text => @modeler.to_s}).select
      @actuaryeditableelement.click
      @actuaryeditableoptionelementlocator = {:class => /#{@EditableDealFormmatoptionclass}/,:visible_text => /#{@actuary.to_s}/}
      @actuaryeditableoptionelement = @browser.span(@actuaryeditableoptionelementlocator)
      @actuaryeditableoptionelement.click

      #@actuaryeditableelement.option(:text => /#{@actuary.to_s}/).click
      #@browser.select_list({:visible_text => @actuary.to_s}).select

      ClickDealEditScreenBtn(user,@editablegridsubmitButton)

    elsif dataavailability == 1 && user == "Read Only Access" && @editablegridsubmitButton.enabled? == false
      print "PASSED - Skipping the step as the user is a #{user} user and the Submit button is disabled.\n"
    else
      print "Skipping the step as there is no data visible in the GRID.\n"
    end

  end
  def updateeditablefieldsusingdatepicker(user,fields,dataavailability)
    @fetchelement = FetchElementDetails.new
    @editablegridsubmitButton = fetchEditableDealFormSubmitButtonElement
    if dataavailability == 1 && user != "Read Only Access"
      #@fieldstobeUpdatedArr = [dealname.to_s,targetdate.to_s,priority.to_s, status.to_s,primaryUnderwriter.to_s,secondaryUnderwriter.to_s,ta.to_s,modeler.to_s,actuary.to_s]
      @EditableDealFormmatoptionclass = @fetchelement.getEditableDealFormmatoptionclass
      @fieldstobeUpdatedArr = fields.to_s.split('*')
      # print "\n"
      # print @fieldstobeUpdatedArr
      # print "\n"
      @dealname = @fieldstobeUpdatedArr[0]
      @targetdate = @fieldstobeUpdatedArr[1]
      @targetdatesplit = @targetdate.to_s.split('/')
      # @modifiedtargetdate = @targetdate.gsub('/','-')
      # @parseddate = @targetdate.
      #@targetdateformated = DateTime.strptime(@targetdate,'%b/%d/%y')
      @targetdatemonth = @targetdatesplit[0]

      #@targetdatesplit[0]
      @targetdateday = @targetdatesplit[1]
      @targetdateyear = @targetdatesplit[2]
      @targetdatemonthname = gettargetdatemonthname(@targetdatemonth)
      @priority = @fieldstobeUpdatedArr[2]
      @status = @fieldstobeUpdatedArr[3]
      @primaryUnderwriter = @fieldstobeUpdatedArr[4]
      @secondaryUnderwriter = @fieldstobeUpdatedArr[5]
      @ta = @fieldstobeUpdatedArr[6]
      @modeler = @fieldstobeUpdatedArr[7]
      @actuary = @fieldstobeUpdatedArr[8]
      @dealnameeditableelement = fetchEditableDealFormDealNameElement
      @priorityeditableelement = fetchEditableDealFormPriorityElement
      @statuseditableelement = fetchEditableDealFormDealStatusElement
      @primaryUnderwritereditableelement = fetchEditableDealFormUnderwritterElement
      @secondaryUnderwritereditableelement = fetchEditableDealFormUnderwritter2Element
      @technicalAssteditableelement = fetchEditableDealFormTechAsstElement
      @modelereditableelement = fetchEditableDealFormModelerElement
      @actuaryeditableelement = fetchEditableDealFormActuaryElement
      @dealnameeditableelement.set(@dealname.to_s)
      @targetdatedatepickerelement = fetchEditableDealFormTargetDateToggleElement
      @targetdatedatepickerelement.click
      sleep 1
      @targetdateMATCalendarPeriodElement = fetchEditableDealFormTargetDateMATCalendarPeriodElement
      @targetdateMATCalendarPeriodElement.click
      sleep 1
      @targetdateyearselectionelementlocator = {:class => /#{@@EditableDealFormTargetDateMATCalendarPeriodCellContentClass}/,:visible_text => @targetdateyear.to_s}
      @targetdatemonthselectionelementlocator = {:class => /#{@@EditableDealFormTargetDateMATCalendarPeriodCellContentClass}/,:visible_text => @targetdatemonthname.to_s}
      @targetdatedayselectionelementlocator = {:class => /#{@@EditableDealFormTargetDateMATCalendarPeriodCellContentClass}/,:visible_text => @targetdateday.to_s}
      @targetdateCalendarNextButtonElement = fetchEditableDealFormTargetDateMATCalendarNextButtonElement
      @targetdateCalendarPrevButtonElement = fetchEditableDealFormTargetDateMATCalendarPreviousButtonElement
      @targetdateyearselectionelement = @browser.div(@targetdateyearselectionelementlocator)
      @targetdateCalendarPrevButtonElement.click
      @targetdateCalendarPrevButtonElement.click
      if @targetdateyearselectionelement.exist? == false
        @targetdateCalendarNextButtonElement.click
        @targetdateyearselectionelementretry1 = @browser.div(@targetdateyearselectionelementlocator)
        if @targetdateyearselectionelementretry1.exists? == false
          @targetdateCalendarNextButtonElement.click
          @targetdateyearselectionelementretry2 = @browser.div(@targetdateyearselectionelementlocator)
          if @targetdateyearselectionelementretry2.exists? == false
            @targetdateCalendarNextButtonElement.click
            @targetdateyearselectionelementretry3 = @browser.div(@targetdateyearselectionelementlocator)
            if @targetdateyearselectionelementretry3.exist? == false
              @targetdateCalendarNextButtonElement.click
              @targetdateyearselectionelementretry4 = @browser.div(@targetdateyearselectionelementlocator)
              if @targetdateyearselectionelementretry4.exist? == false
                print "Year Element not found stopping the identification of year calendar element.\n"
              else
                # print "\n"
                # print @targetdateyearselectionelementretry4.text
                # print "\n"
                @targetdateyearselectionelementretry4.click
              end
            else
              # print "\n"
              # print @targetdateyearselectionelementretry3.text
              # print "\n"
              @targetdateyearselectionelementretry3.click
            end
          else
            # print "\n"
            # print @targetdateyearselectionelementretry2.text
            # print "\n"
            @targetdateyearselectionelementretry2.click
          end
        else
          # print "\n"
          # print @targetdateyearselectionelementretry1.text
          # print "\n"
          @targetdateyearselectionelementretry1.click
        end
      else
        # print "\n"
        # print @targetdateyearselectionelement.text
        # print "\n"
        @targetdateyearselectionelement.click
      end
      sleep 1
      @targetdatemonthselectionelement = @browser.div(@targetdatemonthselectionelementlocator)
      @targetdatemonthselectionelement.click
      sleep 1
      @targetdatedayselectionelement = @browser.div(@targetdatedayselectionelementlocator)
      @targetdatedayselectionelement.click
      #@targetdateeeditableelement.set(@targetdate.to_s)
      @priorityeditableelement.set(@priority.to_s)
      @statuseditableelement.click
      #@browser.send_keys(@status.to_s)
      @statuseditableoptionelementlocator = {:class => /#{@EditableDealFormmatoptionclass}/,:visible_text => /#{@status.to_s}/}
      @statuseditableoptionelement = @browser.span(@statuseditableoptionelementlocator)
      @statuseditableoptionelement.click
      #@statuseditableelement.set(@status.to_s)
      @primaryUnderwritereditableelement.click
      @primaryUnderwritereditableoptionelementlocator = {:class => /#{@EditableDealFormmatoptionclass}/,:visible_text => /#{@primaryUnderwriter.to_s}/}
      @primaryUnderwritereditableoptionelement = @browser.span(@primaryUnderwritereditableoptionelementlocator)
      @primaryUnderwritereditableoptionelement.click


      #@primaryUnderwritereditableelement.option(:text => /#{@primaryUnderwriter.to_s}/).click
      #@browser.select({:visible_text => @primaryUnderwriter.to_s}).select
      @secondaryUnderwritereditableelement.click
      @secondaryUnderwritereditableoptionelementlocator = {:class => /#{@EditableDealFormmatoptionclass}/,:visible_text => /#{@secondaryUnderwriter.to_s}/}
      @secondaryUnderwritereditableoptionelement = @browser.span(@secondaryUnderwritereditableoptionelementlocator)
      @secondaryUnderwritereditableoptionelement.click


      #@secondaryUnderwritereditableelement.option(:text => /#{@secondaryUnderwriter.to_s}/).click
      #@browser.select_list({:visible_text => @secondaryUnderwriter.to_s}).select
      @technicalAssteditableelement.click
      @technicalAssteditableoptionelementlocator = {:class => /#{@EditableDealFormmatoptionclass}/,:visible_text => /#{@ta.to_s}/}
      @technicalAssteditableoptionelement = @browser.span(@technicalAssteditableoptionelementlocator)
      @technicalAssteditableoptionelement.click

      #@technicalAssteditableelement.option(:text => /#{@ta.to_s}/).click
      #@browser.select_list({:visible_text => @ta.to_s}).select
      @modelereditableelement.click
      @modelereditableoptionelementlocator = {:class => /#{@EditableDealFormmatoptionclass}/,:visible_text => /#{@modeler.to_s}/}
      @modelereditableoptionelement = @browser.span(@modelereditableoptionelementlocator)
      @modelereditableoptionelement.click

      #@modelereditableelement.option(:text => /#{@modeler.to_s}/).click
      #@browser.select_list({:visible_text => @modeler.to_s}).select
      @actuaryeditableelement.click
      @actuaryeditableoptionelementlocator = {:class => /#{@EditableDealFormmatoptionclass}/,:visible_text => /#{@actuary.to_s}/}
      @actuaryeditableoptionelement = @browser.span(@actuaryeditableoptionelementlocator)
      @actuaryeditableoptionelement.click

      #@actuaryeditableelement.option(:text => /#{@actuary.to_s}/).click
      #@browser.select_list({:visible_text => @actuary.to_s}).select

      ClickDealEditScreenBtn(user,@editablegridsubmitButton)

    elsif dataavailability == 1 && user == "Read Only Access" && @editablegridsubmitButton.enabled? == false
      print "PASSED - Skipping the step as the user is a #{user} user and the Submit button is disabled.\n"
    else
      print "Skipping the step as there is no data visible in the GRID.\n"
    end

  end

  def reseteditablefields(user,fieldsArr,dataavailability)
    @fetchelement = FetchElementDetails.new
    @editablegridsubmitButton = fetchEditableDealFormSubmitButtonElement
    if dataavailability == 1 && user != "Read Only Access"
      #@fieldstobeUpdatedArr = [dealname.to_s,targetdate.to_s,priority.to_s, status.to_s,primaryUnderwriter.to_s,secondaryUnderwriter.to_s,ta.to_s,modeler.to_s,actuary.to_s]
      @EditableDealFormmatoptionclass = @fetchelement.getEditableDealFormmatoptionclass
      @fieldstoberesetedArr = fieldsArr.to_s.split('*')
      # print "\n"
      # print @fieldstoberesetedArr
      # print "\n"
      @dealname = @fieldstoberesetedArr[0]
      @targetdate = @fieldstoberesetedArr[1]
      @priority = @fieldstoberesetedArr[2]
      @status = @fieldstoberesetedArr[3]
      @primaryUnderwriter = @fieldstoberesetedArr[4]
      @secondaryUnderwriter = @fieldstoberesetedArr[5]
      @ta = @fieldstoberesetedArr[6]
      @modeler = @fieldstoberesetedArr[7]
      @actuary = @fieldstoberesetedArr[8]
      @dealnameeditableelement = fetchEditableDealFormDealNameElement
      @targetdateeeditableelement = fetchEditableDealFormTargetDateElement
      @priorityeditableelement = fetchEditableDealFormPriorityElement
      @statuseditableelement = fetchEditableDealFormDealStatusElement
      @primaryUnderwritereditableelement = fetchEditableDealFormUnderwritterElement
      @secondaryUnderwritereditableelement = fetchEditableDealFormUnderwritter2Element
      @technicalAssteditableelement = fetchEditableDealFormTechAsstElement
      @modelereditableelement = fetchEditableDealFormModelerElement
      @actuaryeditableelement = fetchEditableDealFormActuaryElement
      @dealnameeditableelement.set(@dealname.to_s)
      @targetdateeeditableelement.set(@targetdate.to_s)
      @priorityeditableelement.set(@priority.to_s)
      @statuseditableelement.click
      #@browser.send_keys(@status.to_s)
      @statuseditableoptionelementlocator = {:class => /#{@EditableDealFormmatoptionclass}/,:visible_text => /#{@status.to_s}/}
      @statuseditableoptionelement = @browser.span(@statuseditableoptionelementlocator)
      @statuseditableoptionelement.click

      if @primaryUnderwriter == nil || @primaryUnderwriter == ""
        @primaryUnderwriter = "Select"
      end
      if @secondaryUnderwriter == nil || @secondaryUnderwriter == ""
        @secondaryUnderwriter = "Select"
      end
      if @ta == nil || @ta == ""
        @ta = "Select"
      end
      if @modeler == nil || @modeler == ""
        @modeler = "Select"
      end
      if @actuary == nil || @actuary == ""
        @actuary = "Select"
      end
      # @primaryUnderwritereditableoptionelementlocator = {:class => @EditableDealFormmatoptionclass,:visible_text => @primaryUnderwriter.to_s}
      # @primaryUnderwritereditableoptionelement = @browser.span(@primaryUnderwritereditableoptionelementlocator)
      # @primaryUnderwritereditableoptionelement.click
      # @secondaryUnderwritereditableoptionelementlocator = {:class => @EditableDealFormmatoptionclass,:visible_text => @secondaryUnderwriter.to_s}
      # @secondaryUnderwritereditableoptionelement = @browser.span(@secondaryUnderwritereditableoptionelementlocator)
      # @secondaryUnderwritereditableoptionelement.click
      # @technicalAssteditableoptionelementlocator = {:class => @EditableDealFormmatoptionclass,:visible_text => @ta.to_s}
      # @technicalAssteditableoptionelement = @browser.span(@technicalAssteditableoptionelementlocator)
      # @technicalAssteditableoptionelement.click
      # @modelereditableoptionelementlocator = {:class => @EditableDealFormmatoptionclass,:visible_text => @modeler.to_s}
      # @modelereditableoptionelement = @browser.span(@modelereditableoptionelementlocator)
      # @modelereditableoptionelement.click
      # @actuaryeditableoptionelementlocator = {:class => @EditableDealFormmatoptionclass,:visible_text => @actuary.to_s}
      # @actuaryeditableoptionelement = @browser.span(@actuaryeditableoptionelementlocator)
      # @actuaryeditableoptionelement.click
      #
      #
      @primaryUnderwritereditableelement.click
      @primaryUnderwritereditableoptionelementlocator = {:class => /#{@EditableDealFormmatoptionclass}/,:visible_text => /#{@primaryUnderwriter.to_s}/}
      @primaryUnderwritereditableoptionelement = @browser.span(@primaryUnderwritereditableoptionelementlocator)
      @primaryUnderwritereditableoptionelement.click
      @secondaryUnderwritereditableelement.click
      @secondaryUnderwritereditableoptionelementlocator = {:class => /#{@EditableDealFormmatoptionclass}/,:visible_text => /#{@secondaryUnderwriter.to_s}/}
      @secondaryUnderwritereditableoptionelement = @browser.span(@secondaryUnderwritereditableoptionelementlocator)
      @secondaryUnderwritereditableoptionelement.click
      @technicalAssteditableelement.click
      @technicalAssteditableoptionelementlocator = {:class => /#{@EditableDealFormmatoptionclass}/,:visible_text => /#{@ta.to_s}/}
      @technicalAssteditableoptionelement = @browser.span(@technicalAssteditableoptionelementlocator)
      @technicalAssteditableoptionelement.click
      @modelereditableelement.click
      @modelereditableoptionelementlocator = {:class => /#{@EditableDealFormmatoptionclass}/,:visible_text => /#{@modeler.to_s}/}
      @modelereditableoptionelement = @browser.span(@modelereditableoptionelementlocator)
      @modelereditableoptionelement.click
      @actuaryeditableelement.click
      @actuaryeditableoptionelementlocator = {:class => /#{@EditableDealFormmatoptionclass}/,:visible_text => /#{@actuary.to_s}/}
      @actuaryeditableoptionelement = @browser.span(@actuaryeditableoptionelementlocator)
      @actuaryeditableoptionelement.click
      ClickDealEditScreenBtn(user,@editablegridsubmitButton)
    elsif dataavailability == 1 && user == "Read Only Access" && @editablegridsubmitButton.enabled? == false
      print "PASSED - Skipping the step as the user is a #{user} user and the Submit button is disabled.\n"
    else
      print "Skipping the step as there is no data visible in the GRID.\n"
    end

  end
  def generateexpectedvalues(dealnum,fieldvaluestobeExpectedArr)
    @ExpectedfieldvaluesArray = fieldvaluestobeExpectedArr.to_s.split('*')
    @ExpectedRes = [{"dealNumber"=>dealnum.to_i, "dealName"=>@ExpectedfieldvaluesArray[0].to_s, "targetDate"=>@ExpectedfieldvaluesArray[1].to_s, "priority"=>@ExpectedfieldvaluesArray[2].to_i, "status"=>@ExpectedfieldvaluesArray[3].to_s, "primaryUnderwriterName"=>@ExpectedfieldvaluesArray[4].to_s, "secondaryUnderwriterName"=>@ExpectedfieldvaluesArray[5].to_s, "technicalAssistantName"=>@ExpectedfieldvaluesArray[6].to_s, "modellerName"=>@ExpectedfieldvaluesArray[7].to_s, "actuaryName"=>@ExpectedfieldvaluesArray[8].to_s}]
    @ExpectedRes=@ExpectedRes.to_s.gsub('\"','"')
    @ExpectedRes=@ExpectedRes.to_s.gsub('"["','"')
    @ExpectedRes=@ExpectedRes.to_s.gsub('"]"','"')
    @ExpectedRes=@ExpectedRes.to_s.gsub('/','-')
    @ExpectedRes=@ExpectedRes.to_s.gsub('""','"')
    @ExpectedRes=@ExpectedRes.to_s.gsub('" "','"')
    return @ExpectedRes
  end
  def verifydealavailableinGrid(gData)
    @dealgriddata = gData
    if @dealgriddata[[0,12]].to_s == "Actuary" #&& (@dealgriddata[[3,3]].to_s == "nil" || @dealgriddata[[3,3]].to_s == "")
      @actualcompareddata = Array.new
      @colcount = 0
      @rowcount = 1
      if @dealgriddata[[@rowcount+6,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount+6,@colcount]].to_s == ""
        @possibleRows=6
      end
      if @dealgriddata[[@rowcount+5,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount+5,@colcount]].to_s == ""
        @possibleRows=5
      end
      if @dealgriddata[[@rowcount+4,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount+4,@colcount]].to_s == ""
        @possibleRows=4
      end
      if @dealgriddata[[@rowcount+3,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount+3,@colcount]].to_s == ""
        @possibleRows=3
      end
      if @dealgriddata[[@rowcount+2,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount+2,@colcount]].to_s == ""
        @possibleRows=2
      end
      if @dealgriddata[[@rowcount+1,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount+1,@colcount]].to_s == ""
        @possibleRows=1
      end
      if @dealgriddata[[@rowcount,@colcount]].to_s == "nil" || @dealgriddata[[@rowcount,@colcount]].to_s == ""
        @possibleRows=0
      end
      #print "\n"
      print "Grid is displayed with " + @possibleRows.to_s + " deals.\n"
      #print "\n"
    end
    if @possibleRows > 0
      return 1
    else
      return 0
    end
  end
  def gettargetdatemonthname(targetdatemonth)
    case @targetdatemonth
      when "1"
        @targetdatemonthname = "JAN"
      when "2"
        @targetdatemonthname = "FEB"
      when "3"
        @targetdatemonthname = "MAR"
      when "4"
        @targetdatemonthname = "APR"
      when "5"
        @targetdatemonthname = "MAY"
      when "6"
        @targetdatemonthname = "JUN"
      when "7"
        @targetdatemonthname = "JUL"
      when "8"
        @targetdatemonthname = "AUG"
      when "9"
        @targetdatemonthname = "SEP"
      when "10"
        @targetdatemonthname = "OCT"
      when "11"
        @targetdatemonthname = "NOV"
      when "12"
        @targetdatemonthname = "DEC"
    end
      return @targetdatemonthname
  end
  #Sprint 5 sanjeevs changes
  # ###########################################################################################################################################################################################################
  @AllTimeEleFilter = @fetchelement.getAllTimeElementID
  @PastInceptionTimeEleFilter = @fetchelement.getPastInceptionTimeElementID
  @Over30daysTimeEleFilter = @fetchelement.getOver30daysTimeElementID
  @Within30daysTimeEleFilter = @fetchelement.getWithin30daysTimeElementID

  @@AllTimeEleFilterLocator = {:id => @AllTimeEleFilter}
  @@PastInceptionTimeEleFilterLocatior = {:id => @PastInceptionTimeEleFilter}
  @@Over30daysTimeEleFilterLocator = {:id => @Over30daysTimeEleFilter}
  @@Within30daysTimeEleFilterLocator = {:id => @Within30daysTimeEleFilter}

  def verifyTimeElementFilters

    @pastInceptionEle = @browser.element(@@PastInceptionTimeEleFilterLocatior).exists?
    @over30daysEle = @browser.element(@@Over30daysTimeEleFilterLocator).exists?
    @within30daysEle = @browser.element(@@Within30daysTimeEleFilterLocator).exists?
    @allEle = @browser.element(@@AllTimeEleFilterLocator).exists?

    if @pastInceptionEle && @over30daysEle && @within30daysEle && @allEle
      print "\n Passed - Time Element Filter are visible above the Grid"
    else
      print "\n Failed - Time Element filter are not visible above the Grid"
      fail "\n Failed - Time Element filter are not visible above the Grid"
    end


  end

  def getTimeEleFilterOpt(timeElement)
    @ele = timeElement
    case @ele
      when "Past Inception"
        return @browser.element(@@PastInceptionTimeEleFilterLocatior)
      when "Over 30 Days"
        return @browser.element(@@Over30daysTimeEleFilterLocator)
      when "Within 30 Days"
        return @browser.element(@@Within30daysTimeEleFilterLocator)
      when "All"
        return @browser.element(@@AllTimeEleFilterLocator)
    end
  end

  def fetchTimeEleFilter(timeElement)
    @TimeEleFilter =  getTimeEleFilterOpt(timeElement)

    return @TimeEleFilter

  end

  def getGridFirstRowValues(dealgrid)
    @dealgridEle = dealgrid
    @dealgriddata = Hash.new #Array.new(Array.new)
    @ccount = 0
    @rcount = 0
    @gridSize = 1
    @dealgridEle.each do |div|
      #if @count == 130
      #  exit
      #else
      if @ccount > @gridSize
        @ccount = 0
        @rcount = @rcount+1
      end
      # print "\n"
      # print "rowcount,columncount =" + @rcount.to_s + "," + @ccount.to_s + ". The value is :"
      # print "\n"
      # if (div.text == nil || div.text == "" || div.text.to_s == "nil") && @rcount == 0 && @ccount == 9
      #   @gridSize = 8
      # else
      #   @gridSize = 12
      # end

      # if (div.text == "0" || div.text == nil || div.text == "" || div.text == 0) && @ccount == 4
      #   print "\nI am Here \n"
      #   @dealgriddata[[@rcount,@ccount]] = nil
      # end
      if div.text == ""
        @dealgriddata[[@rcount,@ccount]] = nil
      else
        # print "\n"
        # print div.text.to_s
        # print "\n"
        @dealgriddata[[@rcount,@ccount]] = div.text.to_s
        # end
      end
      @ccount = @ccount + 1
    end
    #print "\n"
    # @colcount=4
    # for @rowcount in 1..6
    #   if @dealgriddata[[@rowcount,@colcount]] == nil
    #     @dealgriddata[[@rowcount,@colcount]] = 0
    #   end
    # end
    # print "\n"
    # print @dealgriddata
    # print "\n"
    @dealgriddata.delete("")
    return @dealgriddata
  end

  def verifying_GridData(timeElement)
    dcur = DateTime.now
    dplus30 = DateTime.now.next_day(29)

    currentDate = dcur.strftime("%m-%d-%Y")
    over30Date = dplus30.strftime("%m-%d-%Y")
    #cur = Date.strptime(currentDate,"%m-%d-%Y")

    @@inceptionDate = {:id => "deal_dealdetails_matInput_inceptiondate" }
    @@firstDeal = {:xpath => "//*[@id='borderLayout_eGridPanel']/div[1]/div/div[4]/div[3]/div/div/div[4]"}
    #@@firstDealAfterSorting = {:xpath => "//*[@id='borderLayout_eGridPanel']/div[1]/div/div[4]/div[3]/div/div/div[1]/div[3]"}


    @ele = timeElement
    case @ele
      when "Past Inception"
        #Sorting Inception column in Descending order
        ClickBtn(@browser.element(@@InceptionCollocator))
        ClickBtn(@browser.element(@@InceptionCollocator))
        sleep 3
        ClickBtn(@browser.element(@@firstDeal))
        sleep 2

        inceptionDateEnd = @browser.element(@@inceptionDate).value
        #print "\n"+inceptionDateEnd
        mm = inceptionDateEnd[0]
        dd = inceptionDateEnd[2]
        yyyy = inceptionDateEnd[4..7]

        # print "\n"+mm.to_s
        # print "\n"+dd.to_s
        # print "\n"+yyyy.to_s
        date1 = dd.to_s+"-"+mm.to_s+"-"+yyyy.to_s
        inceptionDate_End = DateTime.parse(date1)

        # print "\n"+inceptionDate_End.to_s
        if dcur > inceptionDate_End
          print "\n Passed - Inception dates are less than Current date: "+ currentDate
        else
          print "\n Failed - Inception date are greater than or equal to Current date :"+ currentDate
          fail "\n Failed - Inception date are greater than or equal to Current date :"+ currentDate
        end

      when "Over 30 Days"
        #Sorting Inception column in Ascending order

        ClickBtn(@browser.element(@@InceptionCollocator))
        sleep 3
        ClickBtn(@browser.element(@@firstDeal))
        sleep 2

        inceptionDatestr = @browser.element(@@inceptionDate).value
        #print "\n"+inceptionDatestr
        mm = inceptionDatestr[0]
        dd = inceptionDatestr[2]
        yyyy = inceptionDatestr[4..7]

        # print "\n"+mm.to_s
        # print "\n"+dd.to_s
        # print "\n"+yyyy.to_s
        date1 = dd.to_s+"-"+mm.to_s+"-"+yyyy.to_s
        inceptionDateStart = DateTime.parse(date1)

        #print "\n"+inceptionDateStart.to_s
        # print "\n"+ince.to_s

        if  dplus30 < inceptionDateStart #DateDiff
          print "\n Passed - Inception date are over 30 days date: "+ over30Date
        else
          print "\n Failed - Inception dates are not over 30 days  date: "+ over30Date
          fail "\n Failed - Inception dates are not over 30 days  date: "+ over30Date
        end

      when "Within 30 Days"
        #Sorting Inception column in Ascending order

        ClickBtn(@browser.element(@@InceptionCollocator))
        sleep 3
        ClickBtn(@browser.element(@@firstDeal))
        sleep 3

        inceptionDatestr = @browser.element(@@inceptionDate).value
        #print "\n"+inceptionDatestr
        mm = inceptionDatestr[0]
        dd = inceptionDatestr[2]
        yyyy = inceptionDatestr[4..7]

        # print "\n"+mm.to_s
        #  print "\n"+dd.to_s
        #  print "\n"+yyyy.to_s
        date1 = dd.to_s+"-"+mm.to_s+"-"+yyyy.to_s
        inceptionDateStart = DateTime.parse(date1)

        # print "\n"+inceptionDateStart.to_s
        # # #
        #Sorting Inception column in Descending order
        ClickBtn(@browser.element(@@InceptionCollocator))

        sleep 3
        ClickBtn(@browser.element(@@firstDeal))
        sleep 3

        inceptionDateEnd = @browser.element(@@inceptionDate).value
        #print "\n"+inceptionDateEnd
        mm = inceptionDateEnd[0]
        dd = inceptionDateEnd[2]
        yyyy = inceptionDateEnd[4..7]

        # print "\n"+mm.to_s
        # print "\n"+dd.to_s
        # print "\n"+yyyy.to_s
        date1 = dd.to_s+"-"+mm.to_s+"-"+yyyy.to_s
        inceptionDate_End = DateTime.parse(date1)

        #print "\n"+inceptionDate_End.to_s

        if inceptionDateStart >= dcur && inceptionDate_End <= dplus30
          print "\n Passed - Inception date are within 30 days from current date: "+ currentDate
        else
          print "\n Failed - Inception dates are not within 30 days from current date: "+currentDate
          fail "\n Failed - Inception dates are not within 30 days from current date: "+currentDate
        end

    end
  end
  # ###########################################################################################################################################################################################################
  # Changes done by Simran

  def VerifyTimeCounter(timeEle)
    @DesiredEle = timeEle
    if @DesiredEle.exists?
      print " Pass : Time Counter Element found.\n"
    else
      print " Fail : Time Counter Element not found.\n"
      fail " Fail : Time Counter Element not found.\n"
    end
  end

  def VerifyRefreshIcon(refreshEle)  #-- Added by Boya on 10 jan 2019
    @RefreshEle = refreshEle
    if @RefreshEle.exists?
      print " Pass : Refresh Icon Element found.\n"
    else
      print " Fail : Refresh Icon Element not found.\n"
      fail " Fail : Refresh Icon Element not found.\n"
    end
  end

  def VerifyTimeCounterelementtext(time)
    @homepage_TimeCounterlement = @browser.element(@@TimeCounterText)
    if(time == "2 minutes ago")
      if (@homepage_TimeCounterlement.text == time)
        print "Pass : Time Counter Incrementing successfully.\n"
      else
        print "Fail : Time Counter Element not incrementing as expected.\n"
        fail "Fail : Time Counter Element not incrementing as expected.\n"
      end
    end

    if(time == "a few seconds ago")
      if (@homepage_TimeCounterlement.text == time)
        print "Pass : Time Counter reset successfully.\n"
      else
        print "Fail : Time Counter Element not reset as expected.\n"
        fail "Fail : Time Counter Element not reset as expected.\n"
      end
    end
  end


  def SysTimeVsCounter(timeMin)
    timeMin2 = Time.now.min
    timeDiff = timeMin2-timeMin
    if(timeDiff == 2)
      print "Pass: Time Counter change matches the change in System Time.\n"
    else
      print "Fail: Time Counter change does not match the change in System Time.\n"
      fail "Fail: Time Counter change does not match the change in System Time.\n"
    end
  end


  # ###########################################################################################################################################################################################################

  def fetchQuickEditSubmitButtonElement
    @quickEditSubmit = @browser.button(@@quickEditSubButtonElement)
    return @quickEditSubmit
  end

  def fetchQuickEditNotesButtonElement
    @quickEditNotes = @browser.span(@@quickEditNotesButtonElement)
    # @quickEditNotes = @browser.element(@@quickEditNotesButtonElement)
    return @quickEditNotes
  end

  def verifyQuickEditScreen(eleObj)
    @quickEditScreen = eleObj
    sleep 2
    @quickEditScreen.focus
    if @quickEditScreen.exist?
      print "PASSED - Quick Edit Screen is opened successfully.\n"
    else
      print "FAILED - Quick Edit Screen  is not displayed.\n"
      fail "FAILED - Quick Edit Screen is not displayed.\n"
    end
  end

  def fetchNotesWindowTextElement
    @notesWinEle = @browser.span(@@notesWindowElement)
    return @notesWinEle
  end

  def fetchNotesAuthorClassElement
    @quickEditNotes = @browser.span(@@notesAuthorElement)
    return @quickEditNotes
  end

  def fetchNotesWindowAllElement
    @quickEditNotes = @browser.element(@@notesWindowAllEle)
    return @quickEditNotes
  end

  def fetchNotesInfoArea
    @notesTextArea = @browser.textarea(@@notesInfoText)
    return @notesTextArea
  end

  def fetchNotesReadOnlyArea
    @notesReadOnlyCheck = @browser.textarea(@@notesReadOnly)
    return @notesReadOnlyCheck
  end

  def fetchNotesSaveButton
    @notesSaveClick = @browser.element(@@notesEditWinClass).span(:visible_text => "Save")
    return @notesSaveClick
  end

  def fetchNotesCancelButton
    @notesCancelObj = @browser.div(@@notesCancelButtonClass).button(@@notesCancelButtonType)
    return @notesCancelObj
  end

  def fetchNotesCancelPopupVerification
    @notesCancelPopObj = @browser.element(@@notesCancelPopupEle)
    return @notesCancelPopObj
  end

  def fetchNotesCancelPopupVerificationNO
    @notesCancelPopNoObj = @browser.element(@@notesCancelPopupNOEle)
    return @notesCancelPopNoObj
  end

  def fetchNotesWrittenByOthers(other)
    @notesAuthorElementOther = @browser.span({:class => /#{@noteAuthorClass}/, :visible_text => other})
    return @notesAuthorElementOther
  end

  def fetchAddNotesElement
    sleep 2
    @addNote = @browser.span(@@addNotesElement)
    # @quickEditNotes = @browser.element(@@quickEditNotesButtonElement)
    return @addNote
  end

  def fetchAddNotesTypeElement
    sleep 2
    @addNoteType = @browser.element(@@addNoteTypeEle)
    # @quickEditNotes = @browser.element(@@quickEditNotesButtonElement)
    return @addNoteType
  end

  def fetchNoteCrossIconElement
    sleep 2
    @addNoteType = @browser.i(@@noteCrossIconEle)
    # @quickEditNotes = @browser.element(@@quickEditNotesButtonElement)
    return @addNoteType
  end

  def fetchSelectNotesTypeElement
    sleep 2
    @selectNoteType = @browser.elements(@@selectNoteTypeEle)
    # @quickEditNotes = @browser.element(@@quickEditNotesButtonElement)
    return @selectNoteType
  end

  def selectNoteType(noteTypeelements, noteTypeToBeSelected)
    sleep 2
    noteTypeelements.each {|elem|
      if elem.text == noteTypeToBeSelected
        case elem.text
          when "Peer Review"
            elem.click
          when "Misc./General"
            elem.click
          when "Negotiation"
            elem.click
          when "Accommodations"
            elem.click
          when "Modeling/Pricing"
            elem.click
          when "Internal Communications"
            elem.click
          when "UW Info"
            elem.click
        end
      end
    }

  end

  def validateNotesDBResponse(dbResult, expectedResult)

    @dbresulthash = dbResult[0]
    @dbresultval = @dbresulthash["notes"]
    if @dbresultval == expectedResult
      puts "Passed : Db Result is successfully updated with the notes " + expectedResult
    else
      puts "Failed : Db Result is not updated with the notes " + expectedResult
      fail "Failed : Db Result is not updated with the notes " + expectedResult
    end
  end


  def fetchQuickEditDocumentsButtonElement
    @quickEditDocu = @browser.span(@@quickEditDocumentButtonElement)
    # @quickEditNotes = @browser.element(@@quickEditNotesButtonElement)
    return @quickEditDocu
  end

  def fetchDocumentsFolderStruSchemaElement
    sleep 4
    @docFolder = @browser.elements(@@documentsFolderSchemaEle)
    # @quickEditNotes = @browser.element(@@quickEditNotesButtonElement)
    return @docFolder
  end

  def getDocumentsFolderSchema(schema)
    # puts schema.length
    @counter = 0
    @docFol = Array.new
    schema.each {|key|
      @val = key.text
      # puts @val
      @arrayVal = @val.split('  (')
      # puts @arrayVal
      @docFol[@counter] = @arrayVal[0]
      @counter = @counter + 1
    }
    return @docFol
  end

  def getDocumentsFolderPageCount(schema)
    # puts schema.length
    @counter = 0
    @docFol = Array.new
    schema.each {|key|
      @val = key.text
      puts @val
      @arrayVal = @val.delete("^0-9")
      puts @arrayVal
      @docFol[@counter] = @arrayVal.to_i
      @counter = @counter + 1
    }
    # puts @docFol
    puts @docFol
    puts @docFol.sum
    return @docFol.sum
  end

  def scrolltoView(element)
    @browser.execute_script('arguments[0].scrollIntoView();', element)
  end

  def scroll_to(param)
    args = case param
             when :top, :start
               'window.scrollTo(0, 0);'
             when :center
               'window.scrollTo(window.outerWidth / 2, window.outerHeight / 2);'
             when :bottom, :end
               'window.scrollTo(0, document.body.scrollHeight);'
             when Array
               ['window.scrollTo(arguments[0], arguments[1]);', Integer(param[0]), Integer(param[1])]
             else
               raise ArgumentError, "Don't know how to scroll to: #{param}!"
           end

    @browser.execute_script(*args)
  end

  # This method pulls the object on the page you want to interact with, then it 'jumps to it'.
  def jump_to(param)
    # Leveraging the scroll_to(param) logic, this grabs the cooridnates,
    # and then makes them an array that is able to be located and moved to.
    # This is helpful when pages are getting too long and you need to click a button
    # or interact with the browser, but the page 'Cannot locate element'.
    @location = param.wd.location
    @location = @location.to_a
    # @browser.scroll_to(location)
    @args = case @location
             when :top, :start
               'window.scrollTo(0, 0);'
             when :center
               'window.scrollTo(window.outerWidth / 2, window.outerHeight / 2);'
             when :bottom, :end
               'window.scrollTo(0, document.body.scrollHeight);'
             when Array
               ['window.scrollTo(arguments[0], arguments[1]);', Integer(@location[0]), Integer(@location[1])]
             else
               raise ArgumentError, "Don't know how to scroll to: #{@location}!"
           end

    @browser.execute_script(*@args)
  end

  def fetchDocumentsFolderStruExpandButton(param)
    sleep 5
    puts param.length
    param.each {|val|
      @docFolder = @browser.element({:xpath => "//span[text() ='"+val+"']/preceding-sibling::*[contains(@class ,'cusrorPointer')]"})
      # @quickEditNotes = @browser.element(@@quickEditNotesButtonElement)
      @docFolder.focus
      sleep 1
      @docFolder.click
      sleep 2
    }
    # return @docFolder
  end

  def fetchDocumentKeyDocumentElements
    sleep 2
    @keyDocs = @browser.elements(@@documentFolderKeyDocumentsEle)
    # @quickEditNotes = @browser.element(@@quickEditNotesButtonElement)
    return @keyDocs
  end

  def fetchKeyDocumentViewAllDocsElement
    sleep 2
    @keyDocs = @browser.elements(@@keyViewdocumentFolderAllDocsEle)
    # @quickEditNotes = @browser.element(@@quickEditNotesButtonElement)
    return @keyDocs
  end

  def fetchDocumentTreeViewLink
    sleep 5
    @treeViewLink = @browser.element(@@documentTreeViewLinkEle)
    # @quickEditNotes = @browser.element(@@quickEditNotesButtonElement)
    return @treeViewLink
  end

  def fetchKeyDocViewKeyDocuments
    sleep 2
    @keyDocElement = @browser.elements(@@keyDocsViewKeyDocsEle)
    # @quickEditNotes = @browser.element(@@quickEditNotesButtonElement)
    return @keyDocElement
  end

  def fetchGridTableLoadingElement
    sleep 2
    @gridTableLoading = @browser.element(@@gridTableLoadingEleLocator)
    # @quickEditNotes = @browser.element(@@quickEditNotesButtonElement)
    return @gridTableLoading
  end

  def verifyKeyDocsCountWithDB(docsCount,queryCount)
    @keyDocsCount = docsCount.length
    @dbcount = queryCount
    #print "\n"
    #print @panelcount
    #print "\n"
    #print @dbcount
    #print "\n"

    if @keyDocsCount==@dbcount
      print "PASSED - Counts Matched successfully. key Docs count is " + @keyDocsCount.to_s + " and DB Value is " + @dbcount.to_s + ".\n"
      #@reporter.ReportAction("PASSED - Counts Matched successfully.\n")
    else
      print "FAILED - Counts failed to match. key Docs count is " + @keyDocsCount.to_s + " and DB Value is " + @dbcount.to_s + ".\n"
      fail "FAILED - Counts failed to match. key Docs count is " + @keyDocsCount.to_s + " and DB Value is " + @dbcount.to_s + ".\n"
      #@reporter.ReportAction("FAILED - Counts failed to match.\n")
    end
  end

  def fetchDocumentPageCount(docs)
    @count = 0
    @docFolCount = Array.new
    docs.each {|key|
      @val = key.text
      # puts @val
      @arrayVal = @val.split('(')
      # puts @arrayVal
      @temp = @arrayVal[1].delete("^0-9")
      # puts @temp
      @docFolCount[@count] = @temp.to_i
      @count = @count + 1
    }
    return @docFolCount.sum
  end

  def fetchKeyDocumentNames(docs)
    @count = 0
    @docFolNames = Array.new
    docs.each {|key|
      @val = key.text
      # puts @val
      @arrayVal = @val.split('(')
      # puts @arrayVal
      @temp = @arrayVal[0].rstrip
      # puts @temp
      @docFolNames[@count] = @temp
      @count = @count + 1
    }
    return @docFolNames.sort
  end

  def fetchKeyDocumentNamesInKeyDocView(docs)
    @count = 0
    @keydocFolNames = Array.new
    docs.each {|key|
      @val = key.text
      # puts @val
      # @arrayVal = @val.split('(')
      # # puts @arrayVal
      # @temp = @arrayVal[0].rstrip
      # # puts @temp
      @keydocFolNames[@count] = @val
      @count = @count + 1
    }
    return @keydocFolNames.sort
  end

  def verifyCount(param1,param2)
    # @keyDocsCount = docsCount.length
    # @dbcount = queryCount
    #print "\n"
    #print @panelcount
    #print "\n"
    #print @dbcount
    #print "\n"

    if param1==param2
      print "PASSED - Counts Matched successfully document type " + param1.to_s + " and documents is " + param2.to_s + ".\n"
      #@reporter.ReportAction("PASSED - Counts Matched successfully.\n")
    else
      print "FAILED - Counts not Matched document type " + param1.to_s + " and documents is " + param2.to_s + ".\n"
      fail "FAILED - Counts not Matched document type " + param1.to_s + " and documents is " + param2.to_s + ".\n"
      #@reporter.ReportAction("FAILED - Counts failed to match.\n")
    end
  end

  def checkIfButtonIsDisabled(buttonElement)
    @attribute=buttonElement
    puts @attribute.to_s
    if @attribute.nil?
      print "PASS- Button is disbaled"
    else
      fail "FAIL-Button is enabled"
    end
  end


end

