Feature: GRS Home Page - Validate the Key Documents

  @Sprint10 @Done @regression
  Scenario Outline: Verify the all the key documents in the Tree View
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    And I click on the tool menu of the <columnname> column
    And I click on the Filter tab within the tool menu popup
    And I click on the Filter operator drop down with the <operator> list
    And I select the <operator> operator and enter the <textvalue> filter text in the Filter text field
    And I click on the Apply filter button
    And I will be able to see grid is filtered as per the <columnname> column <textvalue> filter string with operator <operator>
    And I click on the <textvalue> deal Number and validate quick Edit Screen is opened
    And I click on Documents icon and get all the folder structure
    Then I validate key document of the deal <textvalue> with the database
    Examples:
      | status                  | columnname  | operator     | textvalue  | notes |
      | Bound - Pending Actions | Deal Number | Equals       | 1367588    | Testing automation123|

