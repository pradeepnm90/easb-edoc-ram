require 'nokogiri'
class FetchElementDetails
  def initialize
    xmlFile = File.join(File.absolute_path('../', File.dirname(__FILE__)),"support","Element_Details.xml")
    @@pDoc = Nokogiri::XML(File.open(xmlFile))
  end

  def getPanelButtonClass
    return @@pDoc.search("PanelButtonClass").text
  end

  def getOnHoldPanelButton
    return @@pDoc.search("OnHoldPanelButton").text
  end
  def getBoundPanelButton
    return @@pDoc.search("BoundPendingActionsPanelButton").text
  end
  def getInProgressPanelButton
    return @@pDoc.search("InProgressPanelButton").text
  end
  def getRenewable6MonthsPanelButton
    return @@pDoc.search("Renewable6MonthsPanelButton").text
  end
  def getInProgress_UnderReviewCheckBox
    return @@pDoc.search("InProgress_UnderReviewCheckBox").text
  end
  def getInProgress_AuthorizeCheckBox
    return @@pDoc.search("InProgress_AuthorizeCheckBox").text
  end
  def getInProgress_OutStandingQuoteCheckBox
    return @@pDoc.search("InProgress_OutStandingQuoteCheckBox").text
  end
  def getInProgress_ToBeDeclinedCheckBox
    return @@pDoc.search("InProgress_ToBeDeclinedCheckBox").text
  end
  def getInProgress_BoundPendingDataEntryCheckBox
    return @@pDoc.search("InProgress_BoundPendingDataEntryCheckBox").text
  end
  def getInProgress_UnderReviewCheckBoxInput
    return @@pDoc.search("InProgress_UnderReviewCheckBoxInput").text
  end
  def getInProgress_AuthorizeCheckBoxInput
    return @@pDoc.search("InProgress_AuthorizeCheckBoxInput").text
  end
  def getInProgress_OutStandingQuoteCheckBoxInput
    return @@pDoc.search("InProgress_OutStandingQuoteCheckBoxInput").text
  end
  def getInProgress_ToBeDeclinedCheckBoxInput
    return @@pDoc.search("InProgress_ToBeDeclinedCheckBoxInput").text
  end
  def getInProgress_BoundPendingDataEntryCheckBoxInput
    return @@pDoc.search("InProgress_BoundPendingDataEntryCheckBoxInput").text
  end
  def getDummywithnoSubstatusButton
    return @@pDoc.search("DummywithnoSubstatusButton").text
  end
  def getDummyInquiredsubstatus
    return @@pDoc.search("DummyInquiredsubstatus").text
  end
  def getDummyInDueDiligencesubstatus
    return @@pDoc.search("DummyInDueDiligencesubstatus").text
  end
  def getDummyInformationalAgreementsubstatus
    return @@pDoc.search("DummyInformationalAgreementsubstatus").text
  end
  def getDummywithSubstatusButton
    return @@pDoc.search("DummywithSubstatusButton").text
  end
  def getTeamButtonid
    return @@pDoc.search("TeamButtonid").text
  end
  def getSubDivisionOverlayPaneid
    return @@pDoc.search("SubDivisionOverlayPaneid").text
  end
  def getSubDivisionLabelClass
    return @@pDoc.search("SubDivisionLabelClass").text
  end
  def getSubDivisionCasualtyText
    return @@pDoc.search("SubDivisionCasualtyText").text
  end
  def getSubDivisionCasualtyTreatyText
    return @@pDoc.search("SubDivisionCasualtyTreatyText").text
  end
  def getSubDivisionCasFacText
    return @@pDoc.search("SubDivisionCasFacText").text
  end
  def getSubDivisionPropertyText
    return @@pDoc.search("SubDivisionPropertyText").text
  end
  def getSubDivisionIntlPropertyText
    return @@pDoc.search("SubDivisionIntlPropertyText").text
  end
  def getSubDivisionNAPropertyText
    return @@pDoc.search("SubDivisionNAPropertyText").text
  end
  def getSubDivisionSpecialtyText
    return @@pDoc.search("SubDivisionSpecialtyText").text
  end
  def getSubDivisionSpecialtyNonPEText
    return @@pDoc.search("SubDivisionSpecialtyNonPEText").text
  end
  def getSubDivisionPublicEntityText
    return @@pDoc.search("SubDivisionPublicEntityText").text
  end

  def getStatusGrid
    return @@pDoc.search("StatusGrid").text
  end
  def getGridDealName
    return @@pDoc.search("GridDealNameText").text
  end
  def getGridContractNumber
    return @@pDoc.search("GridContractNumberText").text
  end
  def getGridInception
    return @@pDoc.search("GridInceptionText").text
  end
  def getGridTargetDate
    return @@pDoc.search("GridTargetDateText").text
  end
  def getGridPriority
    return @@pDoc.search("GridPriorityText").text
  end
  def getGridSubmitted
    return @@pDoc.search("GridSubmittedText").text
  end
  def getGridStatus
    return @@pDoc.search("GridStatusText").text
  end
  def getGridDealNumber
    return @@pDoc.search("GridDealNumberText").text
  end
  def getGridUnderwriterName
    return @@pDoc.search("GridUnderwriterNameText").text
  end
  def getGridUnderwriter2Name
    return @@pDoc.search("GridUnderwriter2NameText").text
  end
  def getGridTAName
    return @@pDoc.search("GridTANameText").text
  end

  def getGridModelerName
    return @@pDoc.search("GridModelerNameText").text
  end
  def getGridActuaryName
    return @@pDoc.search("GridActuaryNameText").text
  end
  def getGridExpiration
    return @@pDoc.search("GridExpirationText").text
  end
  def getGridBrokerName
    return @@pDoc.search("GridBrokerNameText").text
  end
  def getGridBrokerContact
    return @@pDoc.search("GridBrokerContactText").text
  end
  def getGridFieldNameClass
    return @@pDoc.search("GridFieldNameClass").text
  end

  def getDealGridList
    return @@pDoc.search("DealGridList").text
  end
  def getUserIDelement
    return @@pDoc.search("DealGridList").text
  end
  def getDealGridContainer
    return @@pDoc.search("DealGridContainer").text
  end
  def getQlikViewButton
    return @@pDoc.search("QlikViewButton").text
  end

  def getQuickLinks
    return @@pDoc.search("QuickLinks").text
  end

  def getQuickLinksClassElement
    return @@pDoc.search("QuickLinksClassEle").text
  end

  def getERMSButton
    return @@pDoc.search("ERMSButton").text
  end

  def getAgToolMenuHeader
    return @@pDoc.search("AgToolMenuHeader").text
  end

  def getAgGridTableLoadingEle
    return @@pDoc.search("GridLoadingEle").text
  end
  def getAgToolMenuBody
    return @@pDoc.search("AgToolMenuBody").text
  end
  def getAgToolMenuHeaderTab
    return @@pDoc.search("AgToolMenuHeaderTab").text
  end
  def getAgToolMenuColumnSelectPanel
    return @@pDoc.search("AgToolMenuColumnSelectPanel").text
  end
  def getAgToolMenuColSelPanelFieldClass
    return @@pDoc.search("AgToolMenuColSelPanelFieldClass").text
  end
  def getAgToolMenuToolPanelOptionID
    return @@pDoc.search("AgToolMenuToolPanelOptionID").text
  end
  def getAgToolMenuToolPanelOptionText
    return @@pDoc.search("AgToolMenuToolPanelOptionText").text
  end
  def getAgGridRightFloatingBottomClass
    return @@pDoc.search("AgGridRightFloatingBottomClass").text
  end
  def getAgGridCellclass
    return @@pDoc.search("AgGridCellclass").text
  end
  def getAgRowLevel0Class
    return @@pDoc.search("AgRowLevel0Class").text
  end
  def getAgIconFilterClass
    return @@pDoc.search("AgIconFilterClass").text
  end
  def getAgColumnFilterID
    return @@pDoc.search("AgColumnFilterID").text
  end
  def getAgColumnFilterEqualOption
    return @@pDoc.search("AgColumnFilterEqualOption").text
  end
  def getAgColumnFilterGTOption
    return @@pDoc.search("AgColumnFilterGTOption").text
  end
  def getAgColumnFilterLTOption
    return @@pDoc.search("AgColumnFilterLTOption").text
  end
  def getAgColumnFilterNEOption
    return @@pDoc.search("AgColumnFilterNEOption").text
  end
  def getAgColumnFilterIROption
    return @@pDoc.search("AgColumnFilterIROption").text
  end
  def getAgColumnFilterLTOEqualOption
    return @@pDoc.search("AgColumnFilterLTOEqualOption").text
  end
  def getAgColumnFilterGTOEqualOption
    return @@pDoc.search("AgColumnFilterGTOEqualOption").text
  end
  def getAgColumnFilterContainsOption
    return @@pDoc.search("AgColumnFilterContainsOption").text
  end
  def getAgColumnFilternotContainsOption
    return @@pDoc.search("AgColumnFilternotContainsOption").text
  end
  def getAgColumnFilterSelectAllId
    return @@pDoc.search("AgColumnFilterSelectAllId").text
  end
  def getAgColumnFilterSelectClass
    return @@pDoc.search("AgColumnFilterSelectClass").text
  end
  def getAgColumnFilterSelectAuthorizetext
    return @@pDoc.search("AgColumnFilterSelectAuthorizetext").text
  end
  def getAgColumnFilterSelectURtext
    return @@pDoc.search("AgColumnFilterSelectURtext").text
  end
  def getAgColumnFilterSelectBPDEtext
    return @@pDoc.search("AgColumnFilterSelectBPDEtext").text
  end
  def getAgColumnFilterSelectOQtext
    return @@pDoc.search("AgColumnFilterSelectOQtext").text
  end
  def getAgColumnFilterSelectTBDtext
    return @@pDoc.search("AgColumnFilterSelectTBDtext").text
  end
  def getAgColumnFilterSelectOHtext
    return @@pDoc.search("AgColumnFilterSelectOHtext").text
  end
  def getAgColumnFilterSelectBoundtext
    return @@pDoc.search("AgColumnFilterSelectBoundtext").text
  end
  def getAgColumnFilterStatusInputID
    return @@pDoc.search("AgColumnFilterStatusInputID").text
  end
  def getAgColumnFilterInputClass
    return @@pDoc.search("AgColumnFilterInputClass").text
  end
  def getAgColumnFilterApplyButtonID
    return @@pDoc.search("AgColumnFilterApplyButtonID").text
  end
  def getAgColumnFilterDateInputID
    return @@pDoc.search("AgColumnFilterDateInputID").text
  end
  def getAgColumnFilterTextInputID
    return @@pDoc.search("AgColumnFilterTextInputID").text
  end
  def getAgColumnFilterApplyButtonText
    return @@pDoc.search("AgColumnFilterApplyButtonText").text
  end
  def getAgColumnFilterTextInputClass
    return @@pDoc.search("AgColumnFilterTextInputClass").text
  end
  def getAgColumnFilterDateInputClass
    return @@pDoc.search("AgColumnFilterDateInputClass").text
  end
  def getEditableDealFormDealNameId
    return @@pDoc.search("EditableDealFormDealNameId").text
  end
  def getEditableDealFormContactNumberID
    return @@pDoc.search("EditableDealFormContactNumberID").text
  end
  def getEditableDealFormDealStatusID
    return @@pDoc.search("EditableDealFormDealStatusID").text
  end
  def getEditableDealFormDealNumberID
    return @@pDoc.search("EditableDealFormDealNumberID").text
  end
  def getEditableDealFormInceptionDateID
    return @@pDoc.search("EditableDealFormInceptionDateID").text
  end
  def getEditableDealFormInceptionDateToggleID
    return @@pDoc.search("EditableDealFormInceptionDateToggleID").text
  end
  def getEditableDealFormInceptionDatepickerID
    return @@pDoc.search("EditableDealFormInceptionDatepickerID").text
  end
  def getEditableDealFormTargetDateID
    return @@pDoc.search("EditableDealFormTargetDateID").text
  end
  def getEditableDealFormTargetDateToggleID
    return @@pDoc.search("EditableDealFormTargetDateToggleID").text
  end
  def getEditableDealFormTargetDatePickerID
    return @@pDoc.search("EditableDealFormTargetDatePickerID").text
  end
  def getEditableDealFormTargetDatePickerClass
    return @@pDoc.search("EditableDealFormTargetDatePickerClass").text
  end
  def getEditableDealFormSubmittedID
    return @@pDoc.search("EditableDealFormSubmittedID").text
  end
  def getEditableDealFormSubmittedToggleID
    return @@pDoc.search("EditableDealFormSubmittedToggleID").text
  end
  def getEditableDealFormSubmittedDatePickerID
    return @@pDoc.search("EditableDealFormSubmittedDatePickerID").text
  end
  def getEditableDealFormPriorityID
    return @@pDoc.search("EditableDealFormPriorityID").text
  end
  def getEditableDealFormUnderwritterID
    return @@pDoc.search("EditableDealFormUnderwritterID").text
  end
  def getEditableDealFormUnderwritter2ID
    return @@pDoc.search("EditableDealFormUnderwritter2ID").text
  end
  def getEditableDealFormTechAsstID
    return @@pDoc.search("EditableDealFormTechAsstID").text
  end
  def getEditableDealFormActuaryID
    return @@pDoc.search("EditableDealFormActuaryID").text
  end
  def getEditableDealFormModelerID
    return @@pDoc.search("EditableDealFormModelerID").text
  end
  def getEditableDealFormCancelButtonID
    return @@pDoc.search("EditableDealFormCancelButtonID").text
  end
  def getEditableDealFormSubmitButtonID
    return @@pDoc.search("EditableDealFormSubmitButtonID").text
  end
  def getAgMenuClass
    return @@pDoc.search("AgMenuClass").text
  end
  def getAgMenuExportOptionID
    return @@pDoc.search("AgMenuExportOptionID").text
  end
  def getAgMenuExportOptionText
    return @@pDoc.search("AgMenuExportOptionText").text
  end
  def getAgMenuExcelExportOptionText
    return @@pDoc.search("AgMenuExcelExportOptionText").text
  end
  def getAgStatusGridClass
    return @@pDoc.search("StatusGrid").text
  end
  def getAgGridRowclass
    return @@pDoc.search("AgGridRowclass").text
  end
  def getEditableDealFormmatoptionclass
    return @@pDoc.search("EditableDealFormmatoptionclass").text
  end
  def getEditableDealFormDealDetailsContainerClass
    return @@pDoc.search("EditableDealFormDealDetailsContainerClass").text
  end
  def getAgGridRootClass
    return @@pDoc.search("AgGridRootClass").text
  end
  def getEditableDealFormTargetDateMATCalendarPeriodClass
    return @@pDoc.search("EditableDealFormTargetDateMATCalendarPeriodClass").text
  end
  def getEditableDealFormTargetDateMATCalendarPeriodCellContentClass
    return @@pDoc.search("EditableDealFormTargetDateMATCalendarPeriodCellContentClass").text
  end
  def getEditableDealFormTargetDateMATCalendarNextButtonClass
    return @@pDoc.search("EditableDealFormTargetDateMATCalendarNextButtonClass").text
  end
  def getEditableDealFormTargetDateMATCalendarPreviousButtonClass
    return @@pDoc.search("EditableDealFormTargetDateMATCalendarPreviousButtonClass").text
  end
  #Sanjeev
  # Sprint 5
  def getAllTimeElementID
    return @@pDoc.search("AllID").text
  end
  def getPastInceptionTimeElementID
    return @@pDoc.search("PastInceptionID").text
  end
  def getWithin30daysTimeElementID
    return @@pDoc.search("Witin30daysID").text
  end
  def getOver30daysTimeElementID
    return @@pDoc.search("Over30daysID").text
  end

  def getRefreshButtonClass
    return @@pDoc.search("RefreshButton").text
  end

  def getDealGridHeaderColumns
    return @@pDoc.search("DealGridHeaderColumns").text
  end

  def getQuickEditSubmitButton
    return @@pDoc.search("SubmitButton").text
  end
  def getQuickEditNotesButton
    return @@pDoc.search("NotesButton").text
  end

  def getNotesWindow
    return @@pDoc.search("NotesWindow").text
  end

  def getNotesAuthor
    return @@pDoc.search("NoteAuthor").text
  end

  def getNotesInfoTextArea
    return @@pDoc.search("NoteInoTextArea").text
  end

  def getNoteReadOnlyArea
    return @@pDoc.search("NoteReadonly").text
  end

  def getEditNoteWindowClass
    return @@pDoc.search("EditNoteWindow").text
  end

  def getNotesCancelButtonClass
    return @@pDoc.search("CancelButtonClass").text
  end

  def getNotesCancelButtonType
    return @@pDoc.search("buttonType").text
  end
  def getDocumentsIconButton
    return @@pDoc.search("DocumentsButton").text
  end
  def getDocumentsFolders
    return @@pDoc.search("DocumentFolders").text
  end

  def getDocumentsKeyDocsXpath
    return @@pDoc.search("KeyDocument").text
  end

  def getkeyViewDocumentsAllDocsXpath
    return @@pDoc.search("KeyViewDocuments").text
  end

  def getAddNotesElement
    return @@pDoc.search("AddNotesButton").text
  end

  def getAddNotesTypeElement
    return @@pDoc.search("AddNoteType").text
  end

  def getSelectNotesTypeElement
    return @@pDoc.search("SelectNoteType").text
  end

  def getNoteCrossIcon
    return @@pDoc.search("NoteCrossIcon").text
  end

  def getNotesCancelPopupXpath(param)
    if param == "Yes"
      puts @@pDoc.search("NotesCancelPopup").text + param +"')]"
      return @@pDoc.search("NotesCancelPopup").text + param +"')]"
    else
      return @@pDoc.search("NotesCancelPopup").text + param +"')]"
    end
  end

  def getTreeViewLink
    return @@pDoc.search("TreeViewLink").text
  end

  def getTreeDocumentViewDocsList
    return @@pDoc.search("KeyDocumentViewDocsList").text
  end

  def getKeyDocViewKeyDocumentList
    return @@pDoc.search("KeyDocumentViewKeyDocsList").text
  end

end
