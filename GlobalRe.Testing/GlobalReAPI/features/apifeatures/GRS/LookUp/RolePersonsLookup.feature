Feature: GRS Role Persons Lookup API Feature
  This feature is to verify the Role Persons Lookup API and Key Members update

  @api @Sprint5 @Done @regression
  Scenario Outline: Verify whether I get a successful response returned for the Role Persons Lookup api call
    Given that I am submitting a Role Persons Lookup get request for fetching all the persons with the <userrole> role
    Then I receive a response with status as successful for the Role Persons Lookup request for <userrole> role
    Examples:
      | userrole    |
      | Actuary     |
      | Modeler     |
      | Underwriter |
      | UA/TA       |
      | Actuary Manager|
      | Modeler Manager|
      | Underwriter Manager|
      | Property UA/TA     |


  @api @Sprint5 @Done @regression
  Scenario Outline: Verify whether the Role Persons Lookup request response matches with DB
    Given that I am submitting a Role Persons Lookup get request for fetching all the persons with the <userrole> role
    Then the Role Persons Lookup get request response for <userrole> role to and the db query result is matched successfully
    Examples:
      | userrole    |
      | Actuary     |
      | Modeler     |
      | Underwriter |
      | UA/TA       |
      | Actuary Manager|
      | Modeler Manager|
      | Underwriter Manager|
      | Property UA/TA     |

  @api @Sprint5 @Done @regression
  Scenario Outline: Verify whether the Role Persons Lookup request response matches with the schema provided
    Given that I am submitting a Role Persons Lookup get request for fetching all the persons with the <userrole> role
    Then the Role Persons Lookup get request response for <userrole> role and the schema provided is matched successfully
    Examples:
      | userrole    |
      | Actuary     |
      | Modeler     |
      | Underwriter |
      | UA/TA       |
      | Actuary Manager|
      | Modeler Manager|
      | Underwriter Manager|
      | Property UA/TA     |