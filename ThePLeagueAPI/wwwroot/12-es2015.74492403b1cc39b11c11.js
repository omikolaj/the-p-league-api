(window.webpackJsonp=window.webpackJsonp||[]).push([[12],{scBa:function(t,o,e){"use strict";e.r(o);var n=e("ofXK"),s=e("tyNb"),r=e("JIr8"),c=e("vkgz"),a=e("RL7/"),i=e("pKDw"),u=e("ezqR"),p=e("fXoL");const b=[{path:"",component:(()=>{class t{constructor(t,o,e,n,s){this.authService=t,this.router=o,this.snackBar=e,this.eventBus=n,this.location=s}ngOnInit(){this.authService.logout().pipe(Object(r.a)(t=>(this.location.back(),this.snackBar.openSnackBarFromComponent("An error occured while logging out","Dismiss",u.a.Error),t)),Object(c.a)(()=>this.router.navigate(["about"])),Object(c.a)(t=>{this.snackBar.openSnackBarFromComponent("You have successfully logged out","Dismiss",u.a.Success)})).subscribe()}}return t.\u0275fac=function(o){return new(o||t)(p.Sb(a.a),p.Sb(s.d),p.Sb(u.b),p.Sb(i.a),p.Sb(n.h))},t.\u0275cmp=p.Mb({type:t,selectors:[["app-logout"]],decls:0,vars:0,template:function(t,o){},styles:[""]}),t})()}];let h=(()=>{class t{}return t.\u0275mod=p.Qb({type:t}),t.\u0275inj=p.Pb({factory:function(o){return new(o||t)},imports:[[s.h.forChild(b)],s.h]}),t})();e.d(o,"LogoutModule",(function(){return l}));let l=(()=>{class t{}return t.\u0275mod=p.Qb({type:t}),t.\u0275inj=p.Pb({factory:function(o){return new(o||t)},imports:[[n.c,h]]}),t})()}}]);