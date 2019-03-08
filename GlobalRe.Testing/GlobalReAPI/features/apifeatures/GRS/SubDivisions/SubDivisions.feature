Feature: GRS SubDivisions API Feature
  This feature is to verify the SubDivisions API

  @api @Sprint3 @regression
  Scenario: Verify whether I get a successful response returned for the SubDivisions api call
    Given that I am submitting a SubDivisions get request for fetching all the subdivisions
    Then I receive a response with status as successful for the SubDivisions request

  @api @Sprint3 @regression
  Scenario: Verify whether the SubDivisions request response matches with DB
    Given that I am submitting a SubDivisions get request for fetching all the subdivisions
    And I fetch the SubDivisions from DB
    Then the SubDivisions get request response to and the db query result is matched successfully

  @api @Sprint3 @regression
  Scenario: Verify whether the SubDivisions request response matches with the schema provided
    Given that I am submitting a SubDivisions get request for fetching all the subdivisions
    Then the SubDivisions get request response and the schema provided is matched successfully