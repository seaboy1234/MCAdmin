#MCAdmin
*Remote Minecraft Server Administration Over the Web*

##Introduction
This program is ment to act as a remote control panel for your Minecraft Server.  It features an easy to use web-interface that allows you to manage your server in the simplest way possible.

----------

##Usage
When it is finnished, this program will allow you to use the web-interface to install and manage the Minecraft Server.

----------

##Developing
**Development** 
 
Please use the [C# Coding Standards document](http://weblogs.asp.net/lhunt/attachment/591275.ashx "") when writing code.  

Property modifiers may be on the same line when used as `public string Name { get; set; }` 
but not when used as  
`public string Name  
{  
    get  
    {  
        return _name;  
    }  
    set  
    {  
        _name = value  
    }  
}`

Note: in abstract classes, follow this sequence for method/property placement:  

`public abstract Type PropertyName { get; set; }   
protected abstract Type PropertyName { get; set; }  
internal abstract Type PropertyName { get; set; }  

`public abstract type MethodName();  
protected abstract type MethodName();  
internal abstract type MethodName(); 

`public virtual Type PropertyName { get; set; }  
protected virtual Type PropertyName { get; set; }    
internal virtual Type PropertyName { get; set; }  

`public type virtual MethodName()  
{ /*...*/ }  
protected type virtual MethodName()  
{ /*...*/ }  
internal type virtual MethodName()  
{ /*...*/ } 
 
`public Type PropertyName {get; set}  
protected Type PropertyName {get; set}  
internal Type PropertyName {get; set}  
private Type PropertyName {get; set}  

`public ClassName()  
{ /*...*/ }  
protected ClassName()  
{ /*...*/ }  
internal ClassName()  
{ /*...*/ }  
private ClassName()  
{ /*...*/ }

`public type MethodName()  
{ /*...*/ }  
protected type MethodName()  
{ /*...*/ }  
internal type MethodName()  
{ /*...*/ }  
private type MethodName()  
{ /*...*/ }`  
etc.

----------

**Commiting**  
Commit messages should follow this template:
> Capitalized, short (50 chars or less) summary
> 
> More detailed explanatory text, if necessary.  Wrap it to about 72
> characters or so.  In some contexts, the first line is treated as the
> subject of an email and the rest of the text as the body.  The blank
> line separating the summary from the body is critical (unless you omit
> the body entirely); tools like rebase can get confused if you run the
> two together.
> 
> Write your commit message in the present tense: "Fix bug" and not "Fixed
> bug."  This convention matches up with commit messages generated by
> commands like git merge and git revert.
> 
> Further paragraphs come after blank lines.
> 
> - Bullet points are okay, too
> 
> - Typically a hyphen or asterisk is used for the bullet, preceded by a
>   single space, with blank lines in between, but conventions vary here
> 
> - Use a hanging indent

(Shamelessly \*borrowed\* from [tbaggery.com](http://tbaggery.com/2008/04/19/a-note-about-git-commit-messages.html).)