Feature: DB Verification
  This feature is created to verify all the DB related story implementation

  @regression
  Scenario: Verify whether the view created for Global Re fetches all the Global Re related data
    Given I fetch the data from the view
    Then I see the fetched data has only GlobalRe related data
