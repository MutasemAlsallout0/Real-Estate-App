"use strict";(self.webpackChunkreal_estate=self.webpackChunkreal_estate||[]).push([[979],{9979:(T,r,a)=>{a.r(r),a.d(r,{FavoritesModule:()=>A});var u=a(6895),c=a(9197),t=a(4650),l=a(7526),d=a(7392),p=a(1893),v=a(5227),m=a(6534);function g(e,i){1&e&&(t.TgZ(0,"div",11),t._uU(1," \u0644\u0645 \u062a\u0642\u0645 \u0628\u0625\u0636\u0627\u0641\u0629 \u0623\u064a \u0639\u0642\u0627\u0631 \u0625\u0644\u0649 \u0627\u0644\u0645\u0641\u0636\u0644\u0629 "),t.qZA())}function f(e,i){1&e&&(t.TgZ(0,"span",16),t._uU(1," \u0627\u0644\u0639\u0642\u0627\u0631\u0627\u062a \u0627\u0644\u0645\u0641\u0636\u0644\u0629 \u0644\u062f\u064a\u0643 "),t.qZA())}function x(e,i){if(1&e){const o=t.EpF();t.ynx(0),t.TgZ(1,"app-mini-card",17),t.NdJ("Emitting",function(){t.CHM(o);const s=t.oxw(2);return t.KtG(s.getFavList())}),t.qZA(),t.BQk()}if(2&e){const o=i.$implicit;t.xp6(1),t.Q6J("data",o)("isFav",!0)}}function F(e,i){if(1&e&&(t.TgZ(0,"div",6)(1,"div",12),t.YNc(2,f,2,0,"span",13),t.qZA(),t.TgZ(3,"div",14),t.YNc(4,x,2,2,"ng-container",15),t.qZA()()),2&e){const o=t.oxw();t.xp6(2),t.Q6J("ngIf",(null==o.data?null:o.data.length)>0),t.xp6(2),t.Q6J("ngForOf",o.data)}}const h=[{path:"",component:(()=>{var e;class i{constructor(n){this._backend=n,this.window=window,this.p=1,this.fill=6,this.cards=[],this.icon="assets/icons/heart.svg"}ngOnInit(){this.fillArray(),this.getFavList()}fillArray(){for(let n=1;n<=this.fill;n++)this.cards.push(n)}getFavList(){this._backend.getAllFav().subscribe(n=>{this.data=n,console.log(n)})}}return(e=i).\u0275fac=function(n){return new(n||e)(t.Y36(l.w))},e.\u0275cmp=t.Xpm({type:e,selectors:[["app-favorites"]],decls:16,vars:2,consts:[[1,"mx-20"],[1,"flex","justify-center","items-center","text-white","text-5xl",2,"background","center center no-repeat","background-image","linear-gradient(5deg, rgb(45 24 137 / 82%), rgb(4 3 64))","background-size","cover","height","280px"],[1,"flex","flex-row","justify-between","items-center"],["src","assets/icons/heart.svg"],[1,"px-8"],[1,"mx-20","mb-32"],[1,""],["class","text-center pt-12 pb-8 text-secondary text-xl",4,"ngIf"],["class","",4,"ngIf"],["id","back-to-top",1,"bg-secondary","back-to-top","rounded-full","text-center","items-center","justify-center","text-2xl",3,"click"],[1,"!w-full","!min-h-fit","!flex","items-center","justify-center"],[1,"text-center","pt-12","pb-8","text-secondary","text-xl"],[1,"text-center","py-12","text-secondary","text-xl"],["class","text-3xl",4,"ngIf"],[1,"grid","grid-cols-3","gap-5","my-3"],[4,"ngFor","ngForOf"],[1,"text-3xl"],[3,"data","isFav","Emitting"]],template:function(n,s){1&n&&(t._UZ(0,"app-menu"),t.TgZ(1,"div",0)(2,"div",1)(3,"div",2),t._UZ(4,"img",3),t.TgZ(5,"span",4),t._uU(6,"\u0627\u0644\u0645\u0641\u0636\u0644\u0629"),t.qZA(),t._UZ(7,"img",3),t.qZA()()(),t.TgZ(8,"div",5)(9,"div",6),t.YNc(10,g,2,0,"div",7),t.qZA(),t.YNc(11,F,5,2,"div",8),t.qZA(),t.TgZ(12,"a",9),t.NdJ("click",function(){return s.window.scrollTo(0,0)}),t.TgZ(13,"mat-icon",10),t._uU(14,"keyboard_arrow_up"),t.qZA()(),t._UZ(15,"app-footer")),2&n&&(t.xp6(10),t.Q6J("ngIf",0==(null==s.data?null:s.data.length)),t.xp6(1),t.Q6J("ngIf",s.data))},dependencies:[u.sg,u.O5,d.Hw,p.S,v.c,m.M]}),i})()}];let Z=(()=>{var e;class i{}return(e=i).\u0275fac=function(n){return new(n||e)},e.\u0275mod=t.oAB({type:e}),e.\u0275inj=t.cJS({imports:[c.Bz.forChild(h),c.Bz]}),i})();var y=a(591);let A=(()=>{var e;class i{}return(e=i).\u0275fac=function(n){return new(n||e)},e.\u0275mod=t.oAB({type:e}),e.\u0275inj=t.cJS({imports:[u.ez,y.m,Z]}),i})()}}]);