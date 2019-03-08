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


  @Sprint10 @Done @regression
  Scenario Outline: Verify the all the Tree View Page numbers count is correctly displayed
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    And I click on the tool menu of the <columnname> column
    And I click on the Filter tab within the tool menu popup
    And I click on the Filter operator drop down with the <operator> list
    And I select the <operator> operator and enter the <textvalue> filter text in the Filter text field
    And I click on the Apply filter button
    And I will be able to see grid is filtered as per the <columnname> column <textvalue> filter string with operator <operator>
    And I click on the <textvalue> deal Number and validate quick Edit Screen is opened
    And I click on Documents icon and get all Document Types page count from the tree view
    Then I validate key document type page count with the documents count for a given deal
    Examples:
      | status                  | columnname  | operator     | textvalue  | notes |
      | Bound - Pending Actions | Deal Number | Equals       | 1367588    | Testing automation123|


  @Sprint10 @Done @regression
  Scenario Outline: Verify the all DocumentType page number count in the key Document View is correctly displayed
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    And I click on the tool menu of the <columnname> column
    And I click on the Filter tab within the tool menu popup
    And I click on the Filter operator drop down with the <operator> list
    And I select the <operator> operator and enter the <textvalue> filter text in the Filter text field
    And I click on the Apply filter button
    And I will be able to see grid is filtered as per the <columnname> column <textvalue> filter string with operator <operator>
    And I click on the <textvalue> deal Number and validate quick Edit Screen is opened
    And I click on Documents icon and click on Tree View and get all Document Types page count from key document view
    Then I validate key document view document type page count with the documents count for a given deal
    Examples:
      | status                  | columnname  | operator     | textvalue  | notes |
      | Bound - Pending Actions | Deal Number | Equals       | 1367588    | Testing automation123|




  @Sprint10 @Done @regression
  Scenario Outline: Verify the all key Document present in the key Document View and Tree View is matched correctly
    Given I open the browser and navigate to GRS link and click on the <status> Panel
    And I click on the tool menu of the <columnname> column
    And I click on the Filter tab within the tool menu popup
    And I click on the Filter operator drop down with the <operator> list
    And I select the <operator> operator and enter the <textvalue> filter text in the Filter text field
    And I click on the Apply filter button
    And I will be able to see grid is filtered as per the <columnname> column <textvalue> filter string with operator <operator>
    And I click on the <textvalue> deal Number and validate quick Edit Screen is opened
    And I click on Documents icon and get all the key documents from Tree view
    And I click on Tree View and get all key Document from key document view
    Then I validate key documents present in both Tree view and Key document view are matched successfully
    Examples:
      | status                  | columnname  | operator     | textvalue  | notes |
      | Renewable - 6 Months    | Deal Number | Equals       | 1383315    | Testing automation123|
