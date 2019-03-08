Feature: GRS Deal Status Lookup API Feature
  This feature is to verify the Deal Status Lookup API

  @api @Sprint5 @Done @regression
  Scenario: Verify whether I get a successful response returned for the Deal Status Lookup api call
    Given that I am submitting a Deal Status Lookup get request for fetching all the status types
    Then I receive a response with status as successful for the Deal Status Lookup request

  @api @Sprint5 @Done @regression
  Scenario: Verify whether the Deal Status Lookup request response matches with DB
    Given that I am submitting a Deal Status Lookup get request for fetching all the status types
    Then the Deal Status Lookup get request response to and the db query result is matched successfully

  @api @Sprint5 @Done @regression
  Scenario: Verify whether the Deal Status Lookup request response matches with the schema provided
    Given that I am submitting a Deal Status Lookup get request for fetching all the status types
    Then the Deal Status Lookup get request response and the schema provided is matched successfully