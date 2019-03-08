Feature: Views - Delete Existing Views
  This feature is to verify the delete view and to validate if the existing view is deleted successfully and validate the same in the data base


  @Sprint10 @regression
  Scenario Outline: Verify whether I get a successful response returned for the PUT view API call and the data is successfully submitted to DB
    Given that I am submitting a DELETE view API request by passing viewID <viewId>
    And I receive a response with status as successful for the DELETE view API request
    Then I am not able to find the data of the viewId <viewId> in the DB
    Examples:
      | viewId   |
      | 147      |

