Feature: GRS Home Page - Cancel Notes - Cancel Verification functionality

  @Sprint10 @Done @regression
  Scenario Outline: Verify the existing note created by me can be able to edit and Can able to cancel and validate cancel verification
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    And I click on the tool menu of the <columnname> column
    And I click on the Filter tab within the tool menu popup
    And I click on the Filter operator drop down with the <operator> list
    And I select the <operator> operator and enter the <textvalue> filter text in the Filter text field
    And I click on the Apply filter button
    And I will be able to see grid is filtered as per the <columnname> column <textvalue> filter string with operator <operator>
    And I click on the <textvalue> deal Number and validate quick Edit Screen is opened
    And I click on Notes icon and open a note written by self
    Then I can edit the notes <notes> and press cancel button and validate the cancel verification
#    And I validate the notes <notes> is updated in the database for the deal <textvalue>
    Examples:
      | status      | columnname  | operator     | textvalue  | notes |
      | In Progress | Deal Number | Equals       | 1369797    | Testing automation1234|



  @Sprint10 @Done @regression
  Scenario Outline: Verify the cancel verification popup for new Note
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    And I click on the tool menu of the <columnname> column
    And I click on the Filter tab within the tool menu popup
    And I click on the Filter operator drop down with the <operator> list
    And I select the <operator> operator and enter the <textvalue> filter text in the Filter text field
    And I click on the Apply filter button
    And I will be able to see grid is filtered as per the <columnname> column <textvalue> filter string with operator <operator>
    And I click on the <textvalue> deal Number and validate quick Edit Screen is opened
    And I click on Notes icon and i click on Add Notes button and enter fields <noteType> <notes>
    Then I can edit the notes <notes> and press cancel button and validate the cancel verification
    Examples:
      | status      | columnname  | operator     | textvalue    | notes                     | noteType    |
      | In Progress | Deal Number | Equals       | 1369797      | AddNoteCancelVerification | Negotiation |


  @Sprint10 @Done @regression
  Scenario Outline: Verify the cancel verification popup appears if i click on cross icon on the AddNote window
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    And I click on the tool menu of the <columnname> column
    And I click on the Filter tab within the tool menu popup
    And I click on the Filter operator drop down with the <operator> list
    And I select the <operator> operator and enter the <textvalue> filter text in the Filter text field
    And I click on the Apply filter button
    And I will be able to see grid is filtered as per the <columnname> column <textvalue> filter string with operator <operator>
    And I click on the <textvalue> deal Number and validate quick Edit Screen is opened
    And I click on Notes icon and i click on Add Notes button and enter fields <noteType> <notes>
    Then I can edit the notes <notes> and press cross button and validate the cancel verification
    Examples:
      | status      | columnname  | operator     | textvalue    | notes                     | noteType    |
      | In Progress | Deal Number | Equals       | 1369797      | AddNoteCancelVerification | Negotiation |


  @Sprint10 @Done @regression
  Scenario Outline: Verify the cancel verification popup remains after clicking NO option in Add New Note window
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    And I click on the tool menu of the <columnname> column
    And I click on the Filter tab within the tool menu popup
    And I click on the Filter operator drop down with the <operator> list
    And I select the <operator> operator and enter the <textvalue> filter text in the Filter text field
    And I click on the Apply filter button
    And I will be able to see grid is filtered as per the <columnname> column <textvalue> filter string with operator <operator>
    And I click on the <textvalue> deal Number and validate quick Edit Screen is opened
    And I click on Notes icon and i click on Add Notes button and enter fields <noteType> <notes>
    Then I edit the notes <notes> and press cancel button and validate cancel verification will not disappear after clicking on NO option in warning popup
    Examples:
      | status      | columnname  | operator     | textvalue    | notes                     | noteType    |
      | In Progress | Deal Number | Equals       | 1369797      | AddNoteCancelVerification | Negotiation |



#  @Sprint10 @Done @regression
#  Scenario Outline: Verify the db
#    Given I validate the notes <notes> is updated in the database for the deal <textvalue>
#
#    Examples:
#      | status      | columnname  | operator     | textvalue  | notes |
#      | In Progress | Deal Number | Equals       | 1383000    | Testing automation|



