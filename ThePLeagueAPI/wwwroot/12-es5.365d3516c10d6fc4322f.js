function _classCallCheck(e,n){if(!(e instanceof n))throw new TypeError("Cannot call a class as a function")}function _defineProperties(e,n){for(var t=0;t<n.length;t++){var o=n[t];o.enumerable=o.enumerable||!1,o.configurable=!0,"value"in o&&(o.writable=!0),Object.defineProperty(e,o.key,o)}}function _createClass(e,n,t){return n&&_defineProperties(e.prototype,n),t&&_defineProperties(e,t),e}(window.webpackJsonp=window.webpackJsonp||[]).push([[12],{scBa:function(e,n,t){"use strict";t.r(n);var o,r,a=t("ofXK"),c=t("tyNb"),i=t("JIr8"),s=t("vkgz"),u=t("RL7/"),l=t("pKDw"),f=t("ezqR"),p=t("fXoL"),b=[{path:"",component:(o=function(){function e(n,t,o,r,a){_classCallCheck(this,e),this.authService=n,this.router=t,this.snackBar=o,this.eventBus=r,this.location=a}return _createClass(e,[{key:"ngOnInit",value:function(){var e=this;this.authService.logout().pipe(Object(i.a)((function(n){return e.location.back(),e.snackBar.openSnackBarFromComponent("An error occured while logging out","Dismiss",f.a.Error),n})),Object(s.a)((function(){return e.router.navigate(["about"])})),Object(s.a)((function(n){e.snackBar.openSnackBarFromComponent("You have successfully logged out","Dismiss",f.a.Success)}))).subscribe()}}]),e}(),o.\u0275fac=function(e){return new(e||o)(p.Sb(u.a),p.Sb(c.c),p.Sb(f.b),p.Sb(l.a),p.Sb(a.h))},o.\u0275cmp=p.Mb({type:o,selectors:[["app-logout"]],decls:0,vars:0,template:function(e,n){},styles:[""]}),o)}],h=((r=function e(){_classCallCheck(this,e)}).\u0275mod=p.Qb({type:r}),r.\u0275inj=p.Pb({factory:function(e){return new(e||r)},imports:[[c.g.forChild(b)],c.g]}),r);t.d(n,"LogoutModule",(function(){return k}));var g,k=((g=function e(){_classCallCheck(this,e)}).\u0275mod=p.Qb({type:g}),g.\u0275inj=p.Pb({factory:function(e){return new(e||g)},imports:[[a.c,h]]}),g)}}]);