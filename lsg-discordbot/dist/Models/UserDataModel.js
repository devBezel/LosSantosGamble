"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
const typeorm_1 = require("typeorm");
let Users = class Users {
};
__decorate([
    typeorm_1.PrimaryGeneratedColumn(),
    __metadata("design:type", Number)
], Users.prototype, "id", void 0);
__decorate([
    typeorm_1.Column({ type: 'varchar', length: 32 }),
    __metadata("design:type", String)
], Users.prototype, "userId", void 0);
__decorate([
    typeorm_1.Column({ type: 'varchar', length: 32 }),
    __metadata("design:type", String)
], Users.prototype, "trelloCardId", void 0);
Users = __decorate([
    typeorm_1.Entity('users')
], Users);
exports.Users = Users;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiVXNlckRhdGFNb2RlbC5qcyIsInNvdXJjZVJvb3QiOiIiLCJzb3VyY2VzIjpbIi4uLy4uL3NyYy9Nb2RlbHMvVXNlckRhdGFNb2RlbC50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOzs7Ozs7Ozs7OztBQUFBLHFDQUFpRTtBQUdqRSxJQUFhLEtBQUssR0FBbEIsTUFBYSxLQUFLO0NBVWpCLENBQUE7QUFSRztJQURDLGdDQUFzQixFQUFFOztpQ0FDYjtBQUdaO0lBREMsZ0JBQU0sQ0FBQyxFQUFFLElBQUksRUFBRSxTQUFTLEVBQUUsTUFBTSxFQUFFLEVBQUUsRUFBRSxDQUFDOztxQ0FDekI7QUFHZjtJQURDLGdCQUFNLENBQUMsRUFBRSxJQUFJLEVBQUUsU0FBUyxFQUFFLE1BQU0sRUFBRSxFQUFFLEVBQUUsQ0FBQzs7MkNBQ25CO0FBUlosS0FBSztJQURqQixnQkFBTSxDQUFDLE9BQU8sQ0FBQztHQUNILEtBQUssQ0FVakI7QUFWWSxzQkFBSyIsInNvdXJjZXNDb250ZW50IjpbImltcG9ydCB7IEVudGl0eSwgQ29sdW1uLCBQcmltYXJ5R2VuZXJhdGVkQ29sdW1uIH0gZnJvbSAndHlwZW9ybSc7XHJcblxyXG5ARW50aXR5KCd1c2VycycpXHJcbmV4cG9ydCBjbGFzcyBVc2VycyB7XHJcbiAgICBAUHJpbWFyeUdlbmVyYXRlZENvbHVtbigpXHJcbiAgICBpZCE6IG51bWJlcjtcclxuXHJcbiAgICBAQ29sdW1uKHsgdHlwZTogJ3ZhcmNoYXInLCBsZW5ndGg6IDMyIH0pXHJcbiAgICB1c2VySWQ6IHN0cmluZztcclxuXHJcbiAgICBAQ29sdW1uKHsgdHlwZTogJ3ZhcmNoYXInLCBsZW5ndGg6IDMyIH0pXHJcbiAgICB0cmVsbG9DYXJkSWQ6IHN0cmluZztcclxuXHJcbn0iXX0=