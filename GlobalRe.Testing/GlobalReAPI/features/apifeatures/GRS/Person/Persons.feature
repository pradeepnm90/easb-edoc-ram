Feature: GRS Persons API Feature
  This feature is to verify the Persons API

  @api @regression @Sprint4 @Done
  Scenario Outline: Verify whether I get a successful response returned for the Persons api call
    Given that I am submitting a Persons get request for fetching the details of the user having the personid <personid>
    Then I receive a response with status as successful for the Persons request for user having the personid <personid>
    Examples:
    |personid|
    |1135763 |

  @api @Sprint4 @regression @Done
  Scenario Outline: Verify whether the Persons request response matches with DB
    Given that I am submitting a Persons get request for fetching the details of the user having the personid <personid>
    And I fetch the Persons from DB by <personid> person id
    Then the Persons get request response to and the db query result for the <personid> person id is matched successfully
    Examples:
      |personid|
      |null    |

  @api @Sprint4 @regression @Done
  Scenario Outline: Verify whether the Persons request response matches with the schema provided
    Given that I am submitting a Persons get request for fetching the details of the user having the personid <personid>
    Then the Persons get request response for user having the person id <personid> and the schema provided is matched successfully
    Examples:
      |personid|
      |null    |