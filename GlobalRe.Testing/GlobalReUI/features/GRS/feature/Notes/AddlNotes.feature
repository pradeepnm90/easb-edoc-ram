Feature: GRS Home Page - Add Notes  functionality

  @Sprint10 @Done @regression
  Scenario Outline: Verify if Add Note functionality is working as expected
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    And I click on the tool menu of the <columnname> column
    And I click on the Filter tab within the tool menu popup
    And I click on the Filter operator drop down with the <operator> list
    And I select the <operator> operator and enter the <textvalue> filter text in the Filter text field
    And I click on the Apply filter button
    And I will be able to see grid is filtered as per the <columnname> column <textvalue> filter string with operator <operator>
    And I click on the <textvalue> deal Number and validate quick Edit Screen is opened
    And I click on Add Note button and select note type as <noteType>
    And I add text in notes as <notes> in input field
    Then I click on save button to save the note
    And I validate the notes <notes> is updated in the database for the deal <textvalue>
    Examples:
      | status      | columnname  | operator     | textvalue    | notes                     | noteType    |
      | In Progress | Deal Number | Equals       | 1369797      | AddNoteAutomation         | Negotiation |
#      | In Progress | Deal Number | Equals       | 1369797      | AddNoteAutomation         | Underwriting Info |
#      | In Progress | Deal Number | Equals       | 1369797      | AddNoteAutomation         | General/Misc. |

  @Sprint10 @Done @regression
  Scenario Outline: Verify if Save button is not enabled until mandatory fields are filled in
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    And I click on the tool menu of the <columnname> column
    And I click on the Filter tab within the tool menu popup
    And I click on the Filter operator drop down with the <operator> list
    And I select the <operator> operator and enter the <textvalue> filter text in the Filter text field
    And I click on the Apply filter button
    And I will be able to see grid is filtered as per the <columnname> column <textvalue> filter string with operator <operator>
    And I click on the <textvalue> deal Number and validate quick Edit Screen is opened
    And I click on Add Note button and select note type as <noteType>
    And I add text in notes as <notes> in input field
    Then I validate that save button is not enabled

    Examples:
      | status      | columnname  | operator     | textvalue    | notes                     | noteType    |
      | In Progress | Deal Number | Equals       | 1369797      |                           | Negotiation |
#      | In Progress | Deal Number | Equals       | 1369797      | Notes                     |         |









