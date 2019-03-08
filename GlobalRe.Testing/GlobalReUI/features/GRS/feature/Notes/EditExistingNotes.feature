Feature: GRS Home Page - Edit existing Notes functionality

  @Sprint10 @Done @regression
  Scenario Outline: Verify the existing note created by me can be able to edit and save the note
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    And I click on the tool menu of the <columnname> column
    And I click on the Filter tab within the tool menu popup
    And I click on the Filter operator drop down with the <operator> list
    And I select the <operator> operator and enter the <textvalue> filter text in the Filter text field
    And I click on the Apply filter button
    And I will be able to see grid is filtered as per the <columnname> column <textvalue> filter string with operator <operator>
    And I click on the <textvalue> deal Number and validate quick Edit Screen is opened
    And I click on Notes icon and open a note written by self
    Then I can edit the notes <notes> and save the note
    And I validate the notes <notes> is updated in the database for the deal <textvalue>
    Examples:
      | status      | columnname  | operator     | textvalue  | notes |
      | In Progress | Deal Number | Equals       | 1369797    | Testing automation123|



  @Sprint10 @Done @regression
  Scenario Outline: Verify the existing note created by Others cannot be able to edit
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    And I click on the tool menu of the <columnname> column
    And I click on the Filter tab within the tool menu popup
    And I click on the Filter operator drop down with the <operator> list
    And I select the <operator> operator and enter the <textvalue> filter text in the Filter text field
    And I click on the Apply filter button
    And I will be able to see grid is filtered as per the <columnname> column <textvalue> filter string with operator <operator>
    And I click on the <textvalue> deal Number and validate quick Edit Screen is opened
    And I click on Notes icon and open a note written by others <userName>
    Then I can cannot edit the notes written by others and save the note
    Examples:
      | status      | columnname  | operator     | textvalue    | userName   |
      | In Progress | Deal Number | Equals       | 1383000      | Ajay Veera |


#  @Sprint10 @Done @regression
#  Scenario Outline: Verify the db
#    Given I validate the notes <notes> is updated in the database for the deal <textvalue>
#
#    Examples:
#      | status      | columnname  | operator     | textvalue  | notes |
#      | In Progress | Deal Number | Equals       | 1383000    | Testing automation|



