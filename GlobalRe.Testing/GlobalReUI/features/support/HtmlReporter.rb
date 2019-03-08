=begin license
Copyright (c) 2005, Qantom Software
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer. 
Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution. 
Neither the name of Qantom Software nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission. 
THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

(based on BSD Open Source License)
=end

=begin
    Please dont call the methods of this class directly yet. Currently it is
    directly called by various actions of the Web objects. There is 
    a lot of work planned for the reporter feature and so the methods are
    likely to change. 
=end
module Watir
        class HtmlReporter 
            
#:stopdoc:            
            Newline = "\r\n"
            Tab = "\t"            

		HTML_HEADER_START = "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 3.2 Final//EN\"><HTML><HEAD><TITLE>" ;
		HTML_HEADER_END = "</TITLE> \n<META NAME=\"Generator\" " +  
        "CONTENT=\"Qantom Webtest Extensions\">\n" + 
        "<META NAME=\"Author\" CONTENT=\"Qantom\">\n</HEAD>" ;
		HTML_BODY_START = "<BODY BGCOLOR=\"#FFFFFF\">" ;
		HTML_END = "</BODY>\n</HTML>" ;
    TAG_H1_START = "<H1>" ;
		TAG_H1_END = "</H1>" ;
		TAG_FONT_START = "<FONT " ;
		ELEM_FONT_FACE = " FACE = " ;
		ELEM_FONT_SIZE = " SIZE = " ;
		ELEM_FONT_COLOR = " COLOR = "  ;
		ELEM_FONT_STYLE = " STYLE = " ;
		TAG_FONT_END = "</FONT>";
		TAG_BULLET_START = "<LI>" ;
		TAG_BULLET_END = "</LI>" ;
		TAG_INDENT_START = "<UL>" ;
		TAG_INDENT_END = "</UL>" ;
		TAG_START = "<" ;
		TAG_END_START = "</" ;
		TAG_END = ">" ;
		TAG_LINE_BREAK = "<br>" ;
        TAG_SPACE="&nbsp;" ; 
		DEFAULT_TEXT_SIZE = 2 ;
    # added by SHyam
    TAG_IMAGE_START = "<IMG SRC= ";
		#/** Default font to be used for logging */
		DEFAULT_FONT_FACE = "Arial" ;
		#/** Color to be used for displaying a critical error */
		COLOR_ERROR_CRITICAL = "RED" ;
		#/** Color to be used for displaying an incorrect functionality*/
		COLOR_INCORRECT_FUNCTIONALITY = "RED" ;
		#/** Color to be used for displaying a Major (yet not a blocker) error*/
		COLOR_ERROR_COSMETIC_MAJOR = "MAROON" ;
		#/** Color to be used for displaying a Minor Cosmetic issue*/
		COLOR_ERROR_COSMETIC_MINOR = "#FF6600" ;
		#/** Color to be used for displaying a Success message*/
		COLOR_ERROR_NONE = "GREEN" ;

		COLOR_SUCCESS = "GREEN" ; 

		#/** Color to be used for displaying the status of button press / key press operations*/
		COLOR_ACTION = "BLUE" ;
		
		PASS = 1
		FAIL = 2
		ERROR = 3

#:startdoc:

=begin rdoc
    Create a new reporter object. 
    
    Note: Typically it is not required by the tester to directly invoke the reporter class. 
    Instead When the Test Definition is loaded, this is automatically done. To use the currently
    loaded reporter class, simply use the reporter() method in the main module     
=end
            def initialize(pReportSuccess)
                clear
                # added by Shyam 5/14/2013 to check the config property and deciding to print success or not
                @pReportSuccess = pReportSuccess
                if @pReportSuccess == false
                  puts "Success Logs will not be printed in report"
                  end
            end

#:stopdoc:
            def clear
                @sbHtmlLog = nil;
                @hasDocEnded = false ;
                @isTitleSet = false ;
                @sbHeader = nil; 
                init()            
            end
            
            def SetTitle(title)            
                if  !@isTitleSet 			
                    @isTitleSet = true ; 
                    @sbHeader = @sbHeader + title + HTML_HEADER_END
                    sbTemp = ""
                    sbTemp = sbTemp + HTML_BODY_START 
                    sbTemp= sbTemp + TAG_H1_START 
                    sbTemp = sbTemp + "Test Results for "+ title
                    sbTemp=sbTemp + " at "+ ::Time.now.to_s() 
                    sbTemp = sbTemp +TAG_H1_END+ "\n"
                    @sbHtmlLog = sbTemp + @sbHtmlLog					
                end
            end
            
            def IsTitleSet()
                return @isTitleSet
            end
#:startdoc:

=begin rdoc
    Begin indenting text starting from the next line onwards    
=end
            def StartIndentation()
                @sbHtmlLog = @sbHtmlLog + TAG_INDENT_START ;
            end

=begin rdoc
    Stop indentation of text that is currently being printed as indented text
=end
            def EndIndentation()
                @sbHtmlLog = @sbHtmlLog + TAG_INDENT_END ;                
            end
            
#:stopdoc:            
            def Append(text, face=DEFAULT_FONT_FACE, color="Black", size=DEFAULT_TEXT_SIZE)
            		
			    @sbHtmlLog = @sbHtmlLog + TAG_FONT_START 
                @sbHtmlLog = @sbHtmlLog + ELEM_FONT_FACE + "\"" + face+ "\""
			    @sbHtmlLog = @sbHtmlLog + ELEM_FONT_COLOR + "\""
                @sbHtmlLog = @sbHtmlLog + color + "\"" +ELEM_FONT_SIZE 
                @sbHtmlLog = @sbHtmlLog + size.to_s() + TAG_END + text
                @sbHtmlLog = @sbHtmlLog + TAG_FONT_END
			    @sbHtmlLog = @sbHtmlLog + TAG_LINE_BREAK + "\n"
        end
        # added by SHyam below method to capture the screen shot
     def ReportScreenShot(text)
               @sbHtmlLog = @sbHtmlLog + TAG_BULLET_START
               #########
               @sbHtmlLog = @sbHtmlLog + TAG_IMAGE_START 
               @sbHtmlLog = @sbHtmlLog + "\""+ text +"\"" 
               @sbHtmlLog = @sbHtmlLog + " WIDTH="+"700"+ " HEIGHT="+"500" +TAG_END
			    @sbHtmlLog = @sbHtmlLog + "\n"
          ############
                @sbHtmlLog = @sbHtmlLog + TAG_BULLET_END + "\n"
            end
            
            def AppendBold(text)
                Append(text, DEFAULT_FONT_FACE, "Black", DEFAULT_TEXT_SIZE+1) ;
            end

#:startdoc:

=begin rdoc
    Report an error condition in the results. An error is printed in Bold Red color font. 
=end
            def ReportError(text)
                text += " => " + "Error" 
			    AppendBulletedText(text, DEFAULT_FONT_FACE, COLOR_INCORRECT_FUNCTIONALITY, DEFAULT_TEXT_SIZE+2) 				
                if @result < ERROR
				    @result = ERROR
				end
            end


=begin rdoc
    Report a failure in the results. Besides the failure message, you can also pass the following two 
    arguments:
       exp = The text that was expected
       act = The actual test that was seen
    A failure message is printed in Red in the generated report. 
=end
            #extra_text is an array of additional text that can be passed as
            #extra information
            def ReportFailure(text,  exp=nil, act=nil)
                text += " => " + "Fail" 
                indent = TAG_SPACE*4
                if exp != nil
                    text = "#{text}<br>#{indent}Expected = #{exp}"
                    #puts "{{{{{#{text}}}}}}"
                end
                if act != nil
                    text = "#{text}<br>#{indent}Actual #{TAG_SPACE*4}= #{act}"
                end
			    AppendBulletedText(text, DEFAULT_FONT_FACE, COLOR_INCORRECT_FUNCTIONALITY, DEFAULT_TEXT_SIZE) 
				if @result < FAIL 
				    @result = FAIL 
				end
            end
            
=begin rdoc
    Report about a 'passed' test step. A passed test step is printed in 'green' font
=end
            def ReportSuccess(text)
                 if @pReportSuccess == true
                text = text + " => " + "Pass" 
                AppendBulletedText(text, DEFAULT_FONT_FACE, COLOR_SUCCESS, DEFAULT_TEXT_SIZE) 
                      if @result < PASS
                          @result = PASS
                        end
                  end
            end

=begin rdoc
    Report about a 'command' that was attempted. A 'command' is printed in 'blue' font
=end
            def ReportAction(text)
                AppendBulletedText(text, DEFAULT_FONT_FACE, COLOR_ACTION, DEFAULT_TEXT_SIZE) ; 
            end
            
=begin rdoc
    Report a 'general' text message. A general text message is printed out in black
=end            
            def ReportGeneral(text)
                AppendBulletedText(text, DEFAULT_FONT_FACE, "BLACK", DEFAULT_TEXT_SIZE) ;
            end
            
=begin rdoc
    Save the report to the path specified. 
    
    Note: Typically you dont have to call this method directly if you are running tests using 
    the WetRunner or BatchRunner.The results are saved automatically depending on the path 
    specified in the test definition
=end            
            def Save(path)
              # made this change as Dir is throwing an error. 11/12/2015- SM
              dirname = File.dirname(path)
                unless File.directory?(dirname)
                  FileUtils.mkdir_p(dirname)
                end
                #~ Dir.mk_all_dirs(File.dirname(path))
                f = File.new(path, File::CREAT|File::RDWR)
                f.write(@sbHtmlLog )
                f.close()
            end

=begin rdoc
    Get the overall result of test run. An integer value is returned depending on the result of test execution. The
    returned integer can be interpretted as follows:
       PASS = 1
       FAIL = 2
       ERROR = 3
=end
			def result_status()
				return @result
			end
            
#:stopdoc:            
            def to_s()
			    return @sbHtmlLog.to_s()
            end
          
            def to_str_debug()
                stReturn = "" + @sbHtmlLog
                if !@hasDocEnded				
				    stReturn= @sbHeader + stReturn                    
			    end
			    return stReturn		    
            end
#:startdoc:

        private
            def init()
                @sbHtmlLog = ""
			    @sbHeader = "" + HTML_HEADER_START  
			    @hasDocEnded = false ;
				@result = PASS
            end
            
            def end_doc()
                if !@hasDocEnded			
                    @sbHtmlLog = @sbHeader + @sbHtmlLog
                    if  !@isTitleSet				
                        SetTitle("Test Script") ; 
                    end
                    @sbHtmlLog = @sbHtmlLog + HTML_END				
                    hasDocEnded = true ;
                end
            end

            def AppendBulletedText(text, face, color, size)
                @sbHtmlLog = @sbHtmlLog + TAG_BULLET_START
                Append(text, face, color, size) 
                @sbHtmlLog = @sbHtmlLog + TAG_BULLET_END + "\n"
            end


        end

        
end