Feature: GRS Exposure Tree Lookup API Feature
  This feature is to verify Exposure Tree Lookup API to retrieve Subdivision/PL2/Exposure Group/Exposure Name hierarchy

  @api @Sprint6 @Done @regression
  Scenario: Verify whether I get a successful response returned for the Exposure Tree Lookup GET api call
    Given that I am submitting an Exposure Tree Lookup GET request for fetching all the Subdivision/PL2/Exposure Group/Exposure Name hierarchy
    Then I receive a response with status as successful for the Exposure Tree Lookup request


  @api @Sprint6 @Done @regression
  Scenario: Verify whether the Exposure Tree Lookup request response matches with DB
    Given that I am submitting an Exposure Tree Lookup GET request for fetching all the Subdivision/PL2/Exposure Group/Exposure Name hierarchy
    Then  Exposure Tree Lookup get request response and the db query result is matched successfully


  @api @Sprint6 @Done @regression
  Scenario: Verify whether the Exposure Tree Lookup request response matches with the schema provided
    Given that I am submitting an Exposure Tree Lookup GET request for fetching all the Subdivision/PL2/Exposure Group/Exposure Name hierarchy
    Then the Exposure Tree Lookup get request response and the schema provided is matched successfully
