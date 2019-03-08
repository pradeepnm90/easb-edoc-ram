require 'rubygems'
#require 'cucumber'
#require 'cucumber/rake/task'
=begin
task :run_tests, [:environment, :browser, :tags, :feature, :scenario] do |t, args|
  cucumberStatement = 'cucumber ENVIRONMENT=' + args[:environment] + ' BROWSER=' + args[:browser]
  if (!args[:tags].nil? && !args[:tags].empty?)
    splitTags = args[:tags].split ' '
    splitTags.each do |singleTag|
      cucumberStatement += ' --tags @' + singleTag
    end
  elsif (!args[:feature].nil? && !args[:feature].empty?)
    cucumberStatement += ' features/' + args[:feature] + '.feature'
  elsif (!args[:scenario].nil? && !args[:scenario].empty?)
    cucumberStatement += ' features --name "' + args[:scenario] + '"'
  end
  puts cucumberStatement
  sh(cucumberStatement) do |success, _exit_code |
    @success &= success
  end
end
=end
task :sprint1 do
  puts "Starts Test Automation Execution"
  sh 'cucumber --tags @sprint1'
  puts "Ends test"
end
task :poc do
  puts "Starts Test Automation Execution"
  sh 'cucumber --tags @login,@poc'
  puts "Ends test"
end
task :all do
  puts "Starts Test Automation Execution"
  sh 'cucumber --tags @sprint1'
  puts "Ends test"
end
task :Sprint3 do
  puts "Starts Test Automation Execution"
  sh 'cucumber --tags @Sprint3'
  puts "Ends test"
end
task :Sprint4 do
  puts "Starts Test Automation Execution"
  sh 'cucumber --tags @Sprint4'
  puts "Ends test"
end
task :regression do
  puts "Starts Test Automation Execution"
  sh 'cucumber --tags @regression'
  puts "Ends test"
end
