Feature: GRS_POC Deal Count By status feature
  This feature is to verify the Deal Count By Status

  @api
  Scenario: Verify whether service is up and running and the deal count by status JSON is returned successfully
    Given the GRS POC environment has servicename provisioned and the client calls the service
    Then the response is SUCCESS

  @api
  Scenario: Verify whether response returned matches with the DB Query details
    Given the response is received for the submitted request
    Given the corresponding the DB query is executed
    Then the submitted request and the DB query result is matching

  @api
  Scenario: Verify whether JSON response returned matches with the provided JSON message
    Given the response is received for the submitted request
    Then the schema for the JSON response for the submitted request and the provided JSON message schema is matching