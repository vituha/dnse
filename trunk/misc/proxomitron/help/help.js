/* Proxomitron HTML Help ToolTip */
var d=document;

var mode;
if(document.getElementById){mode="DOM";}
else{mode==(eval(document.all))? "IE":"NS";}

var hwWid;
if(!hwWid) hwWid = 350;
var pXoff= -(hwWid/2);
var pYoff= 15;

var hdr="<table width='"+hwWid+"px' border=0 cellspacing=0 cellpadding=0><tr>"+
"<td colspan=3 class=tbd><img src='images/clear.gif' height=4 width=4></td>"+
"<td><img src='images/clear.gif' height=4 width=4></td></tr><tr>"+
"<td class=tbd><img src='images/clear.gif' height=4 width=4></td>"+
"<td class=tip><table><tr><td>";
var ftr="</td></tr></table></td>"+
"<td class=tbd><img src='images/clear.gif' height=4 width=4></td>"+
"<td class=tsh><img src='images/clear.gif' height=4 width=4></td>"+
"</tr><tr><td colspan=3 class=tbd><img src='images/clear.gif' height=4 width=4></td>"+
"<td class=tsh><img src='images/clear.gif' height=4 width=4></td>"+
"</tr><tr><td align=left><img src='images/clear.gif' height=6 width=4></td>"+
"<td colspan=2 class=tsh><img src='images/clear.gif' height=6 width=4></td>"+
"<td class=tsh><img src='images/clear.gif' height=6 width=4></td>"+
"</tr></table>";



function nop(){}
function pophelp(current,e,txt){
 var x;

 if (mode=="DOM"){
  var winX=(document.all)? document.body.scrollLeft:window.pageXOffset;
  var winY=(document.all)? document.body.scrollTop:window.pageYOffset;

  document.getElementById("help").innerHTML= hdr+txt+ftr;
  x=e.clientX+winX+pXoff;
  x=(x<0)? 0:x;
  rt=document.getElementById("help").style.left=x;
  document.getElementById("help").style.top=e.clientY+winY+pYoff;
  document.getElementById("help").style.visibility="visible";

 }else if (mode=="IE"){
  document.all.help.innerHTML= hdr+txt+ftr;
  x=event.clientX+document.body.scrollLeft+pXoff;
  x=(x<0)? 0:x;
  document.all.help.style.pixelLeft=x;
  document.all.help.style.pixelTop=event.clientY+document.body.scrollTop+pYoff;
  document.all.help.style.visibility="visible";
 }else{
  document.help.document.write(hdr+txt+ftr);
  document.help.document.close();
  x=e.pageX+pXoff;
  x=(x<0)? 0:x;
  document.help.left=x;
  document.help.top=e.pageY+pYoff;
  document.help.visibility="show";
  document.captureEvents(Event.MOUSEUP);
 }
 document.onmouseup = popstop;
}

function popstop(){
 document.onmouseup = null;
 if (mode=="DOM") document.getElementById("help").style.visibility="hidden";
 else if (mode=="IE") document.all.help.style.visibility="hidden";
 else document.help.visibility="hidden";
}
