Feature: Login To GRS Feature
  This feature deals with the functionality of the GRS Login page

  @login @regression
  Scenario Outline: Verify the GRS Login Page and successful login
    Given I open the mentioned link i can see the login page
    Then I login using the valid <username> and <password> I can see the GRS Home page

    Examples:
      | username | password |
      | test     | test     |

