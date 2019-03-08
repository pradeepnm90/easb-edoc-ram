Feature: GRS SubDivisions to fetch count of all Status type API Feature
  This feature is to verify the SubDivisions API

  @api @Sprint32
  Scenario: Verify whether I get a successful response returned for the SubDivisions api call
    Given that I am submitting a SubDivisions get request for fetching count of all the status types
    Then I receive a response with status as successful for the count of SubDivisions status types request

  @api @Sprint32
  Scenario: Verify whether the SubDivisions request response matches with DB
    Given that I am submitting a SubDivisions get request for fetching count of all the status types
    And I fetch the SubDivisions from DB
    Then the SubDivisions get request response to and the db query result is matched successfully

  @api @Sprint32
  Scenario: Verify whether the SubDivisions request response matches with the schema provided
    Given that I am submitting a SubDivisions get request for fetching count of all the status types
    Then the SubDivisions status type count GET request response and the schema provided is matched successfully