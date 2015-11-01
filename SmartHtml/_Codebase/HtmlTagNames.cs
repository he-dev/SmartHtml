using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable InconsistentNaming

namespace SmartHtml
{
    /// <summary> </summary>
    /// <remarks>http://www.w3schools.com/tags/</remarks>
    public class HtmlTagNames
    {
        //public const string comment <!--...-->	Defines a comment
        public const string doctype = "!DOCTYPE"; // Defines the document type

        public const string a = "";//> Defines a hyperlink
        public const string abbr = "";//> Defines an abbreviation or an acronym
        public const string acronym = "";//> Not supported in HTML5.Use<abbr> instead.Defines an acronym
        public const string address = "";//> Defines contact information for the author/owner of a document
        public const string applet = "";//>	Not supported in HTML5.Use<embed> or <object> instead.Defines an embedded applet

        [Void]
        public const string area = "";//>	Defines an area inside an image-map
        public const string article = "";//> Defines an article
        public const string aside = "";//>	Defines content aside from the page content
        public const string audio = "";//> Defines sound content
        public const string b = "";//>	Defines bold text

        [Void]
        public const string @base = "";//> Specifies the base URL/target for all relative URLs in a document
        public const string basefont = "";//>	Not supported in HTML5.Use CSS instead.Specifies a default color, size, and font for all text in a document
        public const string bdi = "";//>	Isolates a part of text that might be formatted in a different direction from other text outside it
        public const string bdo = "";//>	Overrides the current text direction
        public const string big = "";//> Not supported in HTML5.Use CSS instead.Defines big text
        public const string blockquote = "";//> Defines a section that is quoted from another source
        public const string body = "";//>	Defines the document's body

        [Void]
        public const string br = "";//>	Defines a single line break
        public const string button = "";//>	Defines a clickable button
        public const string canvas = "";//>	Used to draw graphics, on the fly, via scripting (usually JavaScript)
        public const string caption = "";//>	Defines a table caption
        public const string center = "";//>    Not supported in HTML5.Use CSS instead.Defines centered text
        public const string cite = "";//> Defines the title of a work
        public const string code = "";//> Defines a piece of computer code

        [Void]
        public const string col = "";//> Specifies column properties for each column within a <colgroup> element
        public const string colgroup = "";//> Specifies a group of one or more columns in a table for formatting
        public const string datalist = "";//> Specifies a list of pre-defined options for input controls
        public const string dd = "";//>	Defines a description/value of a term in a description list
        public const string del = "";//> Defines text that has been deleted from a document
        public const string details = "";//>	Defines additional details that the user can view or hide
        public const string dfn = "";//>	Represents the defining instance of a term
        public const string dialog = "";//> Defines a dialog box or window
        public const string dir = "";//> Not supported in HTML5.Use<ul> instead.Defines a directory list
        public const string div = "";//>	Defines a section in a document
        public const string dl = "";//>	Defines a description list
        public const string dt = "";//>	Defines a term/name in a description list
        public const string em = "";//> Defines emphasized text 

        [Void]
        public const string embed = "";//>	Defines a container for an external (non-HTML) application
        public const string fieldset = "";//> Groups related elements in a form
        public const string figcaption = "";//>    Defines a caption for a<figure> element
        public const string figure = "";//>	Specifies self-contained content
        public const string font = "";//>	Not supported in HTML5.Use CSS instead.Defines font, color, and size for text
        public const string footer = "";//> Defines a footer for a document or section
        public const string form = "";//>	Defines an HTML form for user input
        public const string frame = "";//>	Not supported in HTML5.Defines a window (a frame) in a frameset
        public const string frameset = "";//>  Not supported in HTML5.Defines a set of frames
        public const string h1 = "";//> to <h6>	Defines HTML headings
        public const string head = "";//> Defines information about the document
        public const string header = "";//>	Defines a header for a document or section

        [Void]
        public const string hr = "";//>	Defines a thematic change in the content
        public const string html = "html";//>	Defines the root of an HTML document
        public const string i = "";//> Defines a part of text in an alternate voice or mood
        public const string iframe = "";//> Defines an inline frame

        [Void]
        public const string img = "";//> Defines an image

        [Void]
        public const string input = "";//>	Defines an input control
        public const string ins = "";//>	Defines a text that has been inserted into a document
        public const string kbd = "";//>	Defines keyboard input

        [Void]
        public const string keygen = "";//> Defines a key-pair generator field (for forms)
        public const string label = "";//>	Defines a label for an<input> element
        public const string legend = "";//>    Defines a caption for a<fieldset> element
        public const string li = "";//>	Defines a list item

        [Void]
        public const string link = "";//>	Defines the relationship between a document and an external resource (most used to link to style sheets)
        public const string main = "";//>	Specifies the main content of a document
        public const string map = "";//> Defines a client-side image-map
        public const string mark = "";//> Defines marked/highlighted text
        public const string menu = "";//>  Defines a list/menu of commands
        public const string menuitem = "";//> Defines a command/menu item that the user can invoke from a popup menu

        [Void]
        public const string meta = "";//> Defines metadata about an HTML document
        public const string meter = "";//> Defines a scalar measurement within a known range (a gauge)
        public const string nav = "";//>	Defines navigation links
        public const string noframes = "";//> Not supported in HTML5.Defines an alternate content for users that do not support frames
        public const string noscript = "";//> Defines an alternate content for users that do not support client-side scripts
        public const string @object = "";//>	Defines an embedded object
        public const string ol = "";//>	Defines an ordered list
        public const string optgroup = "";//>	Defines a group of related options in a drop-down list
        public const string option = "";//>	Defines an option in a drop-down list
        public const string output = "";//>	Defines the result of a calculation
        public const string p = "";//>	Defines a paragraph

        [Void]
        public const string param = "";//> Defines a parameter for an object
        public const string pre = "";//>	Defines preformatted text
        public const string progress = "";//> Represents the progress of a task
        public const string q = "";//> Defines a short quotation
        public const string rp = "";//>	Defines what to show in browsers that do not support ruby annotations
        public const string rt = "";//>	Defines an explanation/pronunciation of characters (for East Asian typography)
        public const string ruby = "";//>	Defines a ruby annotation(for East Asian typography)
        public const string s = "";//>	Defines text that is no longer correct
        public const string samp = "";//> Defines sample output from a computer program
        public const string script = "";//>    Defines a client-side script
        public const string section = "";//>	Defines a section in a document
        public const string select = "";//>	Defines a drop-down list
        public const string small = "";//>	Defines smaller text

        [Void]
        public const string source = "";//> Defines multiple media resources for media elements (<video> and<audio>)
        public const string span = "";//>	Defines a section in a document
        public const string strike = "";//>    Not supported in HTML5.Use<del> or <s> instead.Defines strikethrough text
        public const string strong = "";//> Defines important text
        public const string style = "";//>	Defines style information for a document
        public const string sub = "";//>	Defines subscripted text
        public const string summary = "";//> Defines a visible heading for a<details> element
        public const string sup = "";//>	Defines superscripted text
        public const string table = "";//> Defines a table
        public const string tbody = "";//>	Groups the body content in a table
        public const string td = "";//>	Defines a cell in a table
        public const string textarea = "";//>	Defines a multiline input control (text area)
        public const string tfoot = "";//>	Groups the footer content in a table
        public const string th = "";//>    Defines a header cell in a table
        public const string thead = "";//>	Groups the header content in a table
        public const string time = "";//>	Defines a date/time
        public const string title = "";//> Defines a title for the document
        public const string tr = "";//>	Defines a row in a table

        [Void]
        public const string track = "";//>	Defines text tracks for media elements (<video> and<audio>)
        public const string tt = "";//>	Not supported in HTML5.Use CSS instead.Defines teletype text
        public const string u = "";//> Defines text that should be stylistically different from normal text
        public const string ul = "";//> Defines an unordered list
        public const string var = "";//> Defines a variable
        public const string video = "";//> Defines a video or movie

        [Void]
        public const string wbr = "";//> Defines a possible line-break
    }
}
