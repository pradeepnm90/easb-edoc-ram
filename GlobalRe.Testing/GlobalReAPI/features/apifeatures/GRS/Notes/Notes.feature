Feature: GRS Notes API Feature
  This feature is to verify the Notes API GET, POST and PUT Methods

  @api @Sprint7 @Done @regression @notes
  Scenario Outline: Verify whether I get a successful response for the fetch Notes api call
    Given that I am submitting a Notes API GET request for fetching all the notes associated to a <dealnumber>
    Then I receive a response with status as successful for the GET Notes API for deal  <dealnumber>
    Examples:
      | dealnumber    |
      | 138     |
    |3841651  |



  @api @Sprint7 @Done @regression @notes
  Scenario Outline: Ver ify whether the GET Notes request response matches with DB
    Given that I am submitting a Notes API GET request for fetching all the notes associated to a <dealnumber>
    Then The Notes GET  request response for <dealnumber> deal and the db query result is matched successfully
    Examples:
      | dealnumber    |
      | 138     |
    |3841651  |

  @api @Sprint7 @Done @regression @notes
  Scenario Outline: Verify whether the GET Notes request response matches with the schema provided
    Given that I am submitting a Notes API GET request for fetching all the notes associated to a <dealnumber>
    Then The Notes GET  request response for <deal> deal and the schema provided is matched successfully
    Examples:
      | dealnumber    |
      | 138     |
      |3841651  |

  @api @Sprint7 @Done @regression @notes
  Scenario Outline: Verify whether I get appropriate error ifI send invalid deal number as parameter for Notes API GET requeust
    Given that I am submitting a Notes API GET request for fetching all the notes associated to a <dealnumber>
    Then I verify the response code as <expectedResponseCode> for  <dealnumber>
    Examples:
      | dealnumber     |expectedResponseCode|
      | 12345678       |404                |
      | $              |404                |
      |                |404                |


  @api @Sprint7 @Done @regression @notes
  Scenario Outline: Verify whether I get a successful response for the POST Notes api call
    Given that I am submitting a Notes API POST request for creating a note with <notedate> <notetype> <notes> <dealnumber>
    Then I receive a response with status as successful for the POST Notes API
    And I verify if the note is created successfully in the database for dealnumber <dealnumber>
#    And I clean up by deleting the created note for dealnumber <dealnumber>
  Examples:
  | notedate         | notetype   | notes   | dealnumber  |
  | 12-14-18         | 3          | Automation     | 3841651 |

   @api @Sprint7 @Done @regression @notes
  Scenario Outline: Verify whether I get a successful response for the PUT Notes api call
    Given that I am submitting a Notes API POST request for creating a note with <notedate> <notetype> <notes> <dealnumber>
    Then I receive a response with status as successful for the POST Notes API
     And I submit a PUT request to update notes with <updatedNotes>
     Then I verify if I get successful response for PUT notes API request
     And I verify if note description received in response is <updatedNotes> for dealnumber <dealnumber>
     And I verify if the note is updated successfull in the database for dealnumber <dealnumber>

  Examples:
  | notedate         | notetype   | notes   | dealnumber  | updatedNotes|
  | 12-14-18         | 3          | Automation     | 3841651 | Updated description|
















