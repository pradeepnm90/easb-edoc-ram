Feature: API to retrive a single note
  This feature is to verify if the GET request to fetch note by note number is working as expected

  @api @Sprint9 @Done @regression @notes
  Scenario Outline: Verify whether I get a successful response for the fetch Note by notenumber api call
    Given that I am submitting a Notes API POST request for creating a note with <notedate> <notetype> <notes> <dealnumber>
    Then I receive a response with status as successful for the POST Notes API
    And I submit a GET request for fetching note for notenumber
    And I verify if I get response code as <expectedResponseCode>
    And I verify if  the API response  and the db query result for the dealnumber <dealnumber> is matched successfully
    Examples:
      | notedate         | notetype   | notes          | dealnumber  | expectedResponseCode|
      | 01-10-18         | 3          | Automation     | 3841651     | 200                 |




  @api @Sprint9 @Done @regression @notes
  Scenario Outline: Verify whether the GET request response to fetch note by note number  matches with the schema provided
    Given that I am submitting a Notes API POST request for creating a note with <notedate> <notetype> <notes> <dealnumber>
    Then I receive a response with status as successful for the POST Notes API
    And I submit a GET request for fetching note for notenumber
  And I verify if the response matches with the schema provided
  Examples:
      | notedate         | notetype   | notes          | dealnumber  |
      | 01-10-18         | 3          | Automation     | 3841651     |


