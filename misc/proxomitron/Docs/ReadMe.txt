#############################################
Proxomitron Version Naoko 4.5 
(C) 1999-2003 Scott R. Lemmon
#############################################

WHAT'S NEW IN VERSION NAOKO-4.5:

Please see the Changes.txt file for details on what's been added since
version Naoko 4.4.

NEW USERS:

If this is the first time you've used Proxomitron, be sure to read
the "Installation and Eradication" section of the help file. You'll
need to follow those instructions to get Proxomitron working with
your browser. 

LICENSE:

Proxomitron is copyrighted 1999-2003 by Scott R. Lemmon

Proxomitron is "Shonenware", and if free for personal use. You can use the
program as often as you like for as long as you like. You are under no
restriction to stop using it after so many days, or indeed ever! You can
also, of course, freely copy it as long as you abide by certain conditions
(see the license.html file for full details).

** USE PROXOMITRON AT YOUR OWN RISK! **

I believe the Proxomitron to be a safe and useful application, but I can
make no guarantee that it will work properly (or at all), or that some
unforeseen bug won't inadvertently cause damage, compromise your system, or
even wake Godzilla.

This software is provided "AS IS" and without any warranty or condition,
express, implied or statutory. In no event will the authors be held liable
for any damages arising from the use of this software. Only use it if you
agree to these terms!

DISTRIBUTION:

I retain all right to the program, but freely authorize the distribution of
copies of this version as long as...

   1. no fee is charged for the program except to cover the basic
      distribution costs of media and shipping (not to exceed $5.00 US),
   2. the program and all associated files are only distributed in
      their original archive and are not altered in any way,
   3. it is not included as part of another commercial product,
   4. absolutely no bison are harmed in the distribution process. ^_^

This product may be included in any software collection downloadable
through electronic means (including web sites, FTP sites, and Bulletin
Board Systems) as long as no fee is charged to gain access to or download
this product.

For inclusion of this product with a commercial product, or inclusion on
compilation CD ROMs or other media sold for profit, permission must be
gotten first. Please contact me at the following email address:
slemmon@proxomitron.cjb.net


## SSL Support ##

Proxomitron now supports SSL (secure https) connections.  This comes in two
flavors - SSLeay mode and pass-thru mode.  The HTTP options under the
"settings" dialog control which is used by default.


SSLeay/OpenSSL mode
-------------------

In this mode Proxomitron decrypt incoming data, filters it, then re-encrypts
it before sending it on.  This allows for nearly transparent filtering and
full control over https connections. This feat is accomplished using the
very nice Open Source SSLeay/OpenSSL libraries (not included - see below).

** WARNING **

This mode is experimental! I would strongly discourage using active
SSL filtering for important transactions such as on-line banking or purchases.
The connection may not be as secure, and it's better not to risk a filter
potentially creating troubles on such a page.  However, since the casual use 
of SSL on less important pages is increasing, sometimes you may wish to 
filter it anyway.  Still, keep in mind that you do so at your own risk.

To use this mode Proxomitron must have access to "slleay32.dll" and
"libeay32.dll" which contain all the SSL libraries and all cryptographic
routines. Otherwise "Pass-Thru" mode will be used.

Because of all the legal and patent problems involved in the USA
with any program that uses encryption, Proxomitron comes with NO
ENCRYPTION CODE WHATSOEVER.  In order to filter SSL connection
you must get a copy of two additional files - "slleay32.dll" and
"libeay32.dll".  These files are part of the SSLeay/OpenSSL
library and contain all the needed routines to do SSL encryption
and decryption. In order to work they should be relatively recent
versions (this has recently been updated by me) and must be complied
with all algorithms needed for https. I know it's a pain but it's the
only safe way I know to offer SSL support.  Here's a few sources of
working DLL files at the time of this writing...

Shining Light Productions is now offering a Windows compile of OpenSSL
Other Win32 compiles of OpenSSL may work too.

http://www.shininglightpro.com/index.php?treeloc=35

The Off-By-One browser also includes these DLLs in it's zip
file (and is also a small download, under 1 meg).

http://homepagesw.com/images/OffByOne.zip

NOTE: If anyone wishes to mirror these files and is located
      somewhere legally able to do so please let me know. 

OpenSSL is developed by the OpenSSL Project for use in the
OpenSSL Toolkit. (http://www.OpenSSL.org/) which includes 
cryptographic software written by Eric Young (eay@cryptsoft.com).
and includes software written by Tim Hudson (tjh@cryptsoft.com).      

(See openssl.txt for the full license)


Server Certificates And Such
----------------------------

There are some other limitations to SSL filtering. In order for 
Proxomitron to act as a SSL server it must have a "certificate". 
Certificates are used by web servers to identify themselves to your web
browser. They must be digitally "signed" by a known company like VeriSign
or your browser will generate a warning.

Proxomitron's certificate is located in the "proxcert.pem" file.  It's
a self-signed certificate created using SSLeay (if you're familiar
with SSLeay you could create a certificate of your own). As such it
should *not* be considered secure.  However it's only used for the
connection between Proxomitron and your web browser - the connection
between Proxomitron and the remote site relies on the site's certificate
not Proxomitron's.  Normally the local connection to your browser never
passes outside your PC, so its security isn't really an issue. In fact,
the only reason to encrypt it at all is to make your browser think it's
connecting directly to a secure site.

This does have a few drawbacks though. When you first visit a secure
site being filtered through Proxomitron, your browser will usually
issue a warning.  This happens for two reasons. First Proxomitron's
certificate won't initially be recognized by your browser (normally
you'll be allowed to add it though). Secondly, Proxomitron's
certificate will not match the name of the site your visiting (since
it can't know that ahead of time).

Unfortunately (or perhaps fortunately) these warning are unavoidable
since SSL was intentionally designed to prevent an intermediary from
secretly intercepting your data. Proxomitron *is* intercepting your
data, but under your control.

One way around this is to use a sort of "half-SSL" technique. 
Proxomitron lets you specify in a normal non-secure "http://" URL that
you want to make a SSL connection to the actual web server - just write
the URL like so...

Original:  https://some.secure.site.com/
New     :  http://https..some.secure.site.com/

Change the protocol back to "http://" then add "https.." to the front of the
hostname. This make it so the connection between your browser and
Proxomitron is not encrypted but the connection from Proxomitron to the
final server is! Since the local connection to Proxomitron is usually
confined to your PC alone, this is really no less secure.  However your
browser thinks it's got a normal insecure connection and won't do any
certificate checks. This can also be used to access secure pages from
browsers that may not have https support at all.


NEW: Proxomitron now can check to make sure the certificate on the 
remote server is valid.  It looks for a file named "certs.pem" in
the Proxomitron base folder. If found, this file should contain 
a list of trusted certificate authorities in the PEM format used
by OpenSSL.  

Proxomitron will pop-up it's own certificate warning dialog if 
the SSL site's certificate has problems or can't be matched to
one of the trusted certificates on file. 

By default a list of some of the more common authorities is included
(VeriSign, Thawte, and the like). A more complete file can be extracted
from the database IE uses. It's possible to use OpenSSL to convert
these into the PEM format used by OpenSSL. 

First extract the ones to convert...

* Under the control panel go to...
  Internet Options->Content->Certificates
* Go to the "Trusted Root Certification Authorities" tab
* Select "Advanced"
* Check *only* "Server Authentication"
* "Export Format" should be PCKS #7
* exit back to certificates tab
* Pick <advanced purposes> from the drop-down selector at the top
  of the "Certificate manager" tab
* Select all the certificates left in the tab's listbox and click "Export"
* Follow through and select a file to save the certs under

Now convert to PEM...

* Next run OpenSSL with the following command line and cross your fingers...

openssl pkcs7 -inform DER -outform PEM 
   -in ie-export-filename.p7b 
   -out certs.pem -print_certs

(note that's all one line - ignore the line breaks)

You should now have an OpenSSL compatible format of IE's cert list!


Keep in mind certificates are just used to help insure your actually
connecting to the site you think you are and not some "spoofed" site.
Whether they actually do this or not is debatable. Many sites (especially
smaller ones) may not be using properly "signed" certificates, but this
doesn't mean your connection is not as encrypted. Really all it means is
they didn't cough up some money for VeriSign's official stamp of approval.
Likewise, a valid certificate is no guarantee a site won't rip you off -
you must still be careful before trusting a site with sensitive data. 

Still, that being said, it's always safer to connect in pass-thru mode
(see below) in cases where security is critical.

 
Pass-Thru mode
--------------

This is similar to the support offered by many other proxies. In this mode
SSL data is simply passed to the server without any alteration.  This mode
requires no special cytological support as the data is never decoded. 
However this method also gives little benefit except to allow proxy
switching. Proxomitron will always use pass-thru mode when bypassed, and
when the "Use SSLeay" mode is not enabled (or the SSLeay dll files are not
present). This is also the safest mode to use from a security standpoint,
and is the default mode used by Proxomitron.


## Proxy Related Stuff ##

CGI PROXY SUPPORT:
------------------

Proxies that use a URL prefix can now be used in Proxomitron.  Simply add
the URL to the proxy selector as you would a normal proxy (but the http://
isn't required). The only restriction is that the proxy must work by tacking
the destination URL to the end.  For example, if the proxy was...

http://somehost.com/cgi-bin/proxy.cgi/

and you were connecting to...

http://anotherhost.com/some/webpage.html

Then Proxomitron will automatically form...

somehost.com/cgi-bin/proxy.cgi/http://anotherhost.com/some/webpage.html

While a DeleGate style proxy might look like...

www.delegate.com:8080/-_-

and form...

www.delegate.com:8080/-_-http://anotherhost.com/some/webpage.html

Just like in a web browser multiple CGI style proxies can be chained by
simply combining them together...

somehost.com/cgi-bin/proxy.cgi/http://www.delegate.com:8080/-_-http://anotherhost.com/some/webpage.html

CHAINING REGULAR A PROXY TO CGI PROXIES:
----------------------------------------

In addition to CGI proxies it's also possible to chain a regular proxy to the
start of the list.  This is the equivalent of using a normal proxy in your
browser while also using a CGI proxy.  The syntax for this is to use '->'
like so...

www.aproxy.com:8080->somehost.com/cgi-bin/proxy.cgi/



+++++++++++++++++++

Well that's about it for now. be sure to check the all new HTML help file
for full details on all the new features.

-Scott-