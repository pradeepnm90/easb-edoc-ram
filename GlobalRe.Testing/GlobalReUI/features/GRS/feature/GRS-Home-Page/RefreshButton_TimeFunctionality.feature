Feature: Verifying the Refresh Button and Time functionality GRS Home Page Feature


  @regression
  Scenario: Verify the Refresh button and Time counter and  its functionality on the GRS Homepage
    Given I open the browser and navigate to GRS link
    And I will be able to see a Refresh Icon and clicked on the icon
    And I will be able to see a Time Counter on the page
    And I can see that the Time Counter is incrementing in minutes
    And I will be able to see a Refresh Icon and clicked on the icon
    Then the counter should start from zero and continue incrementing


  @regression
  Scenario Outline: Verify the Refresh button and Time counter and its functionality by selecting the status panels on the GRS Homepage
    Given I open the browser and navigate to GRS link
    And I will be able to see a Refresh Icon and clicked on the icon
    And I will be able to see a Time Counter on the page
    And I can see that the Time Counter is incrementing in minutes
    Then the time lapsed in the Time Counter should match with the time lapsed in the System clock
    And I click on the <status> status panel
    Then I will be able to see the counts of status panels refreshes with most recent counts
    Then the counter should start from zero and continue incrementing
    Examples:
      | status  |
      | On Hold |
      | In Progress|
      | Renewable - 6 Months|
      | Bound - Pending Actions|

