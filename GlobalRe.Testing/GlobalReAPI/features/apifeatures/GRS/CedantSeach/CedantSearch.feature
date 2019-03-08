Feature: GRS cedant search Feature
  This feature is to verify the cedant search API

  @regression @api @sprint7
    Scenario Outline: Verify if Cedant Search And Select API is working as expected
    Given I submit a GET request for cedant search and select API for <cedantname> cedantname and <parentgroupname> parentgroupname and <cedantid> cedantid and <parentgroupid> parentgroupid and <locationid> locationid and <expectedresult> expected result
    Then I receive a response with status as successful for <cedantname> cedantname and <parentgroupname> parentgroupname and <cedantid> cedantid and <parentgroupid> parentgroupid and <locationid> locationid and <expectedresult> expected result
    Examples:
    | cedantname |parentgroupname|cedantid|parentgroupid|locationid| expectedresult|
    | A          |               |        |             |               |   400       |
    |            |  A            |        |             |               |    400      |
    |            |               |        |             |               |    400      |
    |     A      |              A|        |             |               |   400        |
    | Am         |               |        |             |               |   200        |

#  @regression @api @sprint7
#  Scenario Outline: Verify if Cedant Search And Select API is working as expected
#    Given I submit a GET request for cedant search and select API for <cedantname> cedantname and <parentgroupname> parentgroupname and <cedantid> cedantid and <parentgroupid> parentgroupid and <locationid> locationid
#    Then I receive a response with status as successful for <cedantname> cedantname and <parentgroupname> parentgroupname and <cedantid> cedantid and <parentgroupid> parentgroupid and <locationid> locationid
#    Examples:
#      | cedantname |parentgroupname|cedantid|parentgroupid|locationid|
#      |    Am    |               |        |             |               |
  @api @Sprint7 @Done @regression
    Scenario Outline: Verify whether the Cedant Search And Select GET API request response matches with the schema provided
    Given I submit a GET request for cedant search and select API for <cedantname> cedantname and <parentgroupname> parentgroupname and <cedantid> cedantid and <parentgroupid> parentgroupid and <locationid> locationid
    Then I match the schema with the response with status as successful for <cedantname> cedantname and <parentgroupname> parentgroupname and <cedantid> cedantid and <parentgroupid> parentgroupid and <locationid> locationid
    Examples:
     | cedantname |parentgroupname|cedantid|parentgroupid|locationid|
     | Am        |              |        |             |          |

