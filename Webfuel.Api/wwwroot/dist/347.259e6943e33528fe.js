"use strict";(self.webpackChunkWebfuel_App=self.webpackChunkWebfuel_App||[]).push([[347],{4204:(A,E,n)=>{n.d(E,{u:()=>L});var e=n(9862),t=n(9212),s=n(1993),p=n(95),h=n(3620),r=n(7003),c=n(553),u=n(8819),l=n(2198),i=n(9663),d=n(6814),m=n(2853),T=n(3993),D=n(7147),M=n(9616);function v(_,f){if(1&_&&(t.TgZ(0,"form",9),t._UZ(1,"file-input",10),t.qZA()),2&_){const o=t.oxw();t.Q6J("formGroup",o.form),t.xp6(),t.Q6J("showFiles",!1)("progress",o.progress)}}function I(_,f){if(1&_&&(t.TgZ(0,"a",11),t._UZ(1,"i",12),t._uU(2),t.qZA()),2&_){const o=f.$implicit,a=t.oxw();t.s9C("href",a.sasRedirect(o),t.LSH),t.xp6(2),t.hij("\xa0",o.fileName,"")}}function P(_,f){if(1&_&&t._uU(0),2&_){const o=f.$implicit,a=t.oxw();t.hij(" ",a.formatSize(o)," ")}}function B(_,f){1&_&&(t.TgZ(0,"div")(1,"i"),t._uU(2,"Support Request Form"),t.qZA()())}function U(_,f){if(1&_&&(t.TgZ(0,"div"),t._uU(1),t.ALo(2,"async"),t.qZA()),2&_){const o=t.oxw().$implicit,a=t.oxw();t.xp6(),t.hij(" ",t.lcZ(2,1,a.userService.formatUser(o.uploadedByUserId))," ")}}function y(_,f){if(1&_&&t.YNc(0,B,3,0,"div",13)(1,U,3,3,"div",13),2&_){const o=f.$implicit;t.Q6J("ngIf",null===o.uploadedByUserId),t.xp6(),t.Q6J("ngIf",null!==o.uploadedByUserId)}}function W(_,f){if(1&_){const o=t.EpF();t.TgZ(0,"a",15),t.NdJ("click",function(){t.CHM(o);const g=t.oxw().$implicit,C=t.oxw();return t.KtG(C.deleteFile(g))}),t._uU(1,"delete"),t.qZA()}}function F(_,f){if(1&_&&t.YNc(0,W,2,0,"a",14),2&_){const o=t.oxw();t.Q6J("ngIf",!o.locked)}}let L=(()=>{class _{constructor(o,a,g,C){this.httpClient=o,this.confirmDeleteDialog=a,this.fileStorageEntryApi=g,this.userService=C,this.destroyRef=(0,t.f3M)(t.ktI),this.locked=!1,this.progress=null,this.form=new p.cw({fileStorageGroupId:new p.NI("",{nonNullable:!0}),files:new p.NI([])})}ngOnInit(){this.form.valueChanges.pipe((0,h.b)(200),(0,s.sL)(this.destroyRef)).subscribe(()=>{this.uploadFiles()})}filter(o){o.fileStorageGroupId=this.fileStorageGroupId}formatSize(o){return r.Z.formatBytes(o.sizeBytes)}sasRedirect(o){return c.N.apiHost+"api/file-storage-entry/sas-redirect/"+o.id}uploadFiles(){if(!this.locked&&this.form.value.files&&0!=this.form.value.files.length){this.form.patchValue({fileStorageGroupId:this.fileStorageGroupId},{emitEvent:!1});var o=this.toFormData(this.form.value);this.httpClient.post(c.N.apiHost+"api/file-storage-entry/upload",o,{reportProgress:!0,observe:"events"}).subscribe({next:a=>{a.type===e.dt.UploadProgress&&a.total&&(this.progress=Math.round(100*a.loaded/a.total))},error:a=>{console.log("Error: ",a)},complete:()=>{this.progress=null,this.fileStorageEntryApi.changed.next(null),this.form.patchValue({files:[]},{emitEvent:!1})}})}}toFormData(o){const a=new FormData;var g={};for(const x of Object.keys(o))if("files"==x){var C=o[x];if(C&&C.length)for(var O=0;O<C.length;O++)a.append("file_"+O,C[O])}else g[x]=o[x];return a.append("data",JSON.stringify(g)),a}deleteFile(o){this.locked||this.confirmDeleteDialog.open({title:o.fileName}).subscribe(a=>{this.fileStorageEntryApi.delete({id:o.id},{successGrowl:"File Deleted"}).subscribe(()=>{})})}static#t=this.\u0275fac=function(a){return new(a||_)(t.Y36(e.eN),t.Y36(u.h),t.Y36(l.z),t.Y36(i.K))};static#e=this.\u0275cmp=t.Xpm({type:_,selectors:[["file-browser"]],inputs:{fileStorageGroupId:"fileStorageGroupId",locked:"locked"},decls:16,vars:3,consts:[[1,"file-list"],[3,"formGroup",4,"ngIf"],[3,"dataSource","search","filter"],["name","filename"],["itemTemplate",""],["name","size"],["name","uploadedAt"],["name","uploadedBy"],["justify","right"],[3,"formGroup"],["formControlName","files",3,"showFiles","progress"],["target","blank",1,"link","is-primary",3,"href"],[1,"fas","fa-link"],[4,"ngIf"],["class","link is-danger",3,"click",4,"ngIf"],[1,"link","is-danger",3,"click"]],template:function(a,g){1&a&&(t.TgZ(0,"div",0),t.YNc(1,v,2,3,"form",1),t.TgZ(2,"grid",2),t.NdJ("filter",function(O){return g.filter(O)}),t.TgZ(3,"grid-column",3),t.YNc(4,I,3,2,"ng-template",null,4,t.W1O),t.qZA(),t.TgZ(6,"grid-column",5),t.YNc(7,P,1,1,"ng-template",null,4,t.W1O),t.qZA(),t._UZ(9,"grid-datetime-column",6),t.TgZ(10,"grid-column",7),t.YNc(11,y,2,2,"ng-template",null,4,t.W1O),t.qZA(),t.TgZ(13,"grid-column",8),t.YNc(14,F,1,1,"ng-template",null,4,t.W1O),t.qZA()()()),2&a&&(t.xp6(),t.Q6J("ngIf",!g.locked),t.xp6(),t.Q6J("dataSource",g.fileStorageEntryApi)("search",!0))},dependencies:[d.O5,p._Y,p.JJ,p.JL,p.sg,p.u,m.Y,T.M,D.I,M.W,d.Ov],encapsulation:2})}return _})()},1986:(A,E,n)=>{n.d(E,{k:()=>l});var e=n(9212),t=n(7147),s=n(6814);function p(i,d){if(1&i){const m=e.EpF();e.TgZ(0,"a",3),e.NdJ("click",function(){e.CHM(m);const D=e.oxw(2);return e.KtG(D.onAdd())}),e._uU(1,"add"),e.qZA()}}function h(i,d){if(1&i&&e.YNc(0,p,2,0,"a",2),2&i){const m=e.oxw();e.Q6J("ngIf",m.canAdd)}}function r(i,d){if(1&i){const m=e.EpF();e.TgZ(0,"a",6),e.NdJ("click",function(){e.CHM(m);const D=e.oxw().$implicit,M=e.oxw();return e.KtG(M.onEdit(D))}),e._uU(1,"view/edit"),e.qZA()}}function c(i,d){if(1&i){const m=e.EpF();e.TgZ(0,"span"),e._uU(1," \xa0 "),e.TgZ(2,"a",7),e.NdJ("click",function(){e.CHM(m);const D=e.oxw().$implicit,M=e.oxw();return e.KtG(M.onDelete(D))}),e._uU(3,"delete"),e.qZA()()}}function u(i,d){if(1&i&&e.YNc(0,r,2,0,"a",4)(1,c,4,0,"span",5),2&i){const m=e.oxw();e.Q6J("ngIf",m.canEdit),e.xp6(),e.Q6J("ngIf",m.canDelete)}}let l=(()=>{class i extends t.I{constructor(){super(),this.canAdd=!0,this.add=new e.vpe,this.canEdit=!0,this.edit=new e.vpe,this.canDelete=!0,this.delete=new e.vpe,this.justify="right"}onAdd(){this.add.emit()}onEdit(m){this.edit.emit(m)}onDelete(m){this.delete.emit(m)}static#t=this.\u0275fac=function(T){return new(T||i)};static#e=this.\u0275cmp=e.Xpm({type:i,selectors:[["grid-action-column"]],inputs:{canAdd:"canAdd",canEdit:"canEdit",canDelete:"canDelete"},outputs:{add:"add",edit:"edit",delete:"delete"},features:[e._Bn([{provide:t.I,useExisting:i}]),e.qOj],decls:4,vars:0,consts:[["headTemplate",""],["itemTemplate",""],["class","link is-success",3,"click",4,"ngIf"],[1,"link","is-success",3,"click"],["class","link is-primary",3,"click",4,"ngIf"],[4,"ngIf"],[1,"link","is-primary",3,"click"],[1,"link","is-danger",3,"click"]],template:function(T,D){1&T&&e.YNc(0,h,1,1,"ng-template",null,0,e.W1O)(2,u,2,2,"ng-template",null,1,e.W1O)},dependencies:[s.O5],encapsulation:2})}return i})()},9510:(A,E,n)=>{n.d(E,{K:()=>h});var e=n(7147),t=n(9212),s=n(6814);function p(r,c){if(1&r&&(t._uU(0),t.ALo(1,"date")),2&r){const u=c.$implicit,l=t.oxw();t.hij(" ",t.xi3(1,1,u[l.name],"dd MMM yyyy"),"\n")}}let h=(()=>{class r extends e.I{static#t=this.\u0275fac=(()=>{let u;return function(i){return(u||(u=t.n5z(r)))(i||r)}})();static#e=this.\u0275cmp=t.Xpm({type:r,selectors:[["grid-date-column"]],features:[t._Bn([{provide:e.I,useExisting:r}]),t.qOj],decls:2,vars:0,consts:[["itemTemplate",""]],template:function(l,i){1&l&&t.YNc(0,p,2,4,"ng-template",null,0,t.W1O)},dependencies:[s.uU],encapsulation:2})}return r})()},9616:(A,E,n)=>{n.d(E,{W:()=>h});var e=n(7147),t=n(9212),s=n(6814);function p(r,c){if(1&r&&(t._uU(0),t.ALo(1,"date")),2&r){const u=c.$implicit,l=t.oxw();t.hij(" ",t.xi3(1,1,u[l.name],"dd MMM yyyy HH:mm"),"\n")}}let h=(()=>{class r extends e.I{static#t=this.\u0275fac=(()=>{let u;return function(i){return(u||(u=t.n5z(r)))(i||r)}})();static#e=this.\u0275cmp=t.Xpm({type:r,selectors:[["grid-datetime-column"]],features:[t._Bn([{provide:e.I,useExisting:r}]),t.qOj],decls:2,vars:0,consts:[["itemTemplate",""]],template:function(l,i){1&l&&t.YNc(0,p,2,4,"ng-template",null,0,t.W1O)},dependencies:[s.uU],encapsulation:2})}return r})()},3773:(A,E,n)=>{n.d(E,{a:()=>r});var e=n(7147),t=n(5619),s=n(9212),p=n(6814);function h(c,u){if(1&c&&(s._uU(0),s.ALo(1,"async")),2&c){const l=u.$implicit,i=s.oxw();s.hij(" ",i.format(s.lcZ(1,1,i.lookup(l[i.name]))),"\n")}}let r=(()=>{class c extends e.I{constructor(){super(...arguments),this._cache={}}lookup(l){if(null==this._cache[l]){if(this._cache[l]=new t.X(null),!this.dataSource)return new t.X(null);this.dataSource.query({skip:0,take:1,filters:[{field:"id",op:"eq",value:l}]}).subscribe(i=>{1==i.items.length&&this._cache[l].next(i.items[0])})}return this._cache[l]}format(l){return l?l.name:""}static#t=this.\u0275fac=(()=>{let l;return function(d){return(l||(l=s.n5z(c)))(d||c)}})();static#e=this.\u0275cmp=s.Xpm({type:c,selectors:[["grid-reference-column"]],inputs:{dataSource:"dataSource"},features:[s._Bn([{provide:e.I,useExisting:c}]),s.qOj],decls:2,vars:0,consts:[["itemTemplate",""]],template:function(i,d){1&i&&s.YNc(0,h,2,3,"ng-template",null,0,s.W1O)},dependencies:[p.Ov],encapsulation:2})}return c})()}}]);