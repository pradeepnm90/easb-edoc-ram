Feature: HomePageFeature
  This feature deals with the functionality of the GRS Home Page

  @poc @regression
  Scenario: Verify the GRS Home Page
    Given I logged in to GRS I can see the GRS Home page
    And I see five panels with various status
    And I see each of the panels have the label and count of deals having the mentioned status
    And I see a panel as disabled if the count is zero
    Then if I click any of the enabled panels having a count more than zero, i see deals having that status is displayed below

  @regression
  Scenario: Verify the GRS Home Page
    Given I logged in to GRS I can see the GRS Home page
    And if I click any of the enabled panels having a count more than zero, i see deals having that status is displayed below in ascending order
    And I click on the grid column the deals are sorted in descending order
    Then I click on the grid column the deals are sorted in ascending order





