/*
 Navicat Premium Data Transfer

 Source Server         : myservice
 Source Server Type    : MySQL
 Source Server Version : 50721
 Source Host           : 119.28.178.12:3306
 Source Schema         : EmsDB

 Target Server Type    : MySQL
 Target Server Version : 50721
 File Encoding         : 65001

 Date: 09/04/2018 16:54:01
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Records of Area
-- ----------------------------
INSERT INTO `Area` VALUES (7, '四川', 'sc', 0, '7,', '1d5c83b6-5de5-4adb-be04-4ea476843599');
INSERT INTO `Area` VALUES (8, '成都', 'cd', 7, '7,8,', '511ea276-4a46-40f9-ba99-6300ae9b52e3');
INSERT INTO `Area` VALUES (9, '双流区', 'slq', 8, '7,8,9,', '901445ea-7469-4045-b31a-f94839fc3e9c');
INSERT INTO `Area` VALUES (10, '高新区', 'gxq', 8, '7,8,10,', '03eea95b-7769-4a19-95e3-5df35e3f471b');
INSERT INTO `Area` VALUES (11, '郫县区', 'pxq', 8, '7,8,11,', 'e8aac7c3-e636-4d3d-8def-9d82b2cbc44b');
INSERT INTO `Area` VALUES (12, '武侯区', 'whq', 8, '7,8,12,', '55f57a93-3e8e-4ce1-94b6-20c8cbb32e98');
INSERT INTO `Area` VALUES (13, '重庆', 'cq', 0, '13,', '1baa2086-3fcf-489d-a847-462bad7a8684');
INSERT INTO `Area` VALUES (14, '重庆市', 'cqs', 13, '13,14,', 'f9163f3c-4d8a-4b91-98e9-e2a8eca56b60');
INSERT INTO `Area` VALUES (15, '沙坪坝', 'spb', 14, '13,14,15,', '338fa562-82b7-49f4-b017-cf828d86869c');
INSERT INTO `Area` VALUES (16, '巴南区', 'bnq', 14, '13,14,16,', 'f5d47e57-6829-4191-abb3-8d483e2c5829');

-- ----------------------------
-- Records of Major
-- ----------------------------
INSERT INTO `Major` VALUES (1, '计算机', 'computer');
INSERT INTO `Major` VALUES (2, '测试1', 'cs');
INSERT INTO `Major` VALUES (3, '测试2', 'cs23');


-- ----------------------------
-- Records of NewArticle
-- ----------------------------
INSERT INTO `NewArticle` VALUES (1, '啊打发士大夫 ', '<p><img src=\"/upload/image/20180406/6365862189929648404292106.jpg\" title=\"1024x768_d7a3791d242d0df.jpg\" alt=\"1024x768_d7a3791d242d0df.jpg\"/></p>', '2018-04-06 14:31:43', 0, ' ');
INSERT INTO `NewArticle` VALUES (2, '的规划的方式更好', '<p>啊打发嘎尔夫<img src=\"/upload/image/20180406/6365862234226820418465615.jpg\" title=\"1024x768_d7a3791d242d0df.jpg\" alt=\"1024x768_d7a3791d242d0df.jpg\"/></p>', '2018-04-06 14:41:31', 0, '啊打发嘎尔夫');
INSERT INTO `NewArticle` VALUES (3, '测试12++2+3', '<p>就是不告诉你发生了什恶魔啊打发v32415^&amp;*%^&amp;^$)*(<img src=\"/upload/image/20180406/6365862320882202627646679.jpg\" title=\"QQ图片20171014221913.jpg\" alt=\"QQ图片20171014221913.jpg\"/></p>', '2018-04-06 14:53:30', 0, '就是不告诉你发生了什恶魔啊打发v32415^&*%^&^$)*(');
INSERT INTO `NewArticle` VALUES (4, '爱的发声', '<p>阿迪斯发士大夫</p>', '2018-04-06 14:56:13', 0, '阿迪斯发士大夫');
INSERT INTO `NewArticle` VALUES (5, '', '', '2018-04-06 14:57:20', 1, '');
INSERT INTO `NewArticle` VALUES (6, '', '', '2018-04-06 14:57:36', 1, '');
INSERT INTO `NewArticle` VALUES (7, '测试相关哇哦', '<hr/><p><img src=\"http://img.baidu.com/hi/jx2/j_0028.gif\"/>测试测试弟弟<br/></p><hr/><p><br/></p>', '2018-04-06 16:51:36', 0, '测试测试弟弟');
INSERT INTO `NewArticle` VALUES (8, '开封其他', '<p>风干腊肉<img src=\"/upload/image/20180407/6365869393164918578968666.jpg\" title=\"QQ图片20171014221913.jpg\" alt=\"QQ图片20171014221913.jpg\"/></p>', '2018-04-07 10:32:13', 0, '风干腊肉');


-- ----------------------------
-- Records of Organization
-- ----------------------------
INSERT INTO `Organization` VALUES (1, NULL, '本公司', '1,', 0, '2018-01-18 09:58:21', '2018-01-18 09:58:21', NULL);
INSERT INTO `Organization` VALUES (2, 1, '测试1', '1,2,', 0, '2018-03-01 20:36:53', '2018-03-01 20:38:39', NULL);
INSERT INTO `Organization` VALUES (3, NULL, '测试1', '3,', 0, '2018-03-01 20:37:39', '2018-03-01 20:37:39', NULL);
INSERT INTO `Organization` VALUES (4, NULL, '四川', '4,', 1, '2018-04-06 17:14:48', '2018-04-07 20:50:18', '2018-04-07 20:50:18');


-- ----------------------------
-- Records of Permission
-- ----------------------------
INSERT INTO `Permission` VALUES (101, 1, NULL, 'Total.BaseMenu.User', '2018-01-17 22:16:11');
INSERT INTO `Permission` VALUES (102, 1, NULL, 'Total.BaseMenu.Organization', '2018-01-17 22:16:11');
INSERT INTO `Permission` VALUES (103, 1, NULL, 'Total.BaseMenu.Organization.RemoveUser', '2018-01-17 22:16:11');
INSERT INTO `Permission` VALUES (104, 1, NULL, 'Total.BaseMenu.Organization.AddUser', '2018-01-17 22:16:11');
INSERT INTO `Permission` VALUES (105, 1, NULL, 'Total.BaseMenu.Organization.Migrate', '2018-01-17 22:16:11');
INSERT INTO `Permission` VALUES (106, 1, NULL, 'Total', '2018-01-17 22:16:11');
INSERT INTO `Permission` VALUES (107, 1, NULL, 'Total.BaseMenu.Organization.Delete', '2018-01-17 22:16:11');
INSERT INTO `Permission` VALUES (108, 1, NULL, 'Total.BaseMenu.Organization.Create', '2018-01-17 22:16:11');
INSERT INTO `Permission` VALUES (109, 1, NULL, 'Total.BaseMenu.Role', '2018-01-17 22:16:11');
INSERT INTO `Permission` VALUES (110, 1, NULL, 'Total.BaseMenu', '2018-01-17 22:16:11');
INSERT INTO `Permission` VALUES (111, 1, NULL, 'Total.BaseMenu.Organization.Rename', '2018-01-17 22:16:11');


-- ----------------------------
-- Records of Role
-- ----------------------------
INSERT INTO `Role` VALUES (1, 'admin', 1, 1, 0, '2018-01-15 10:21:35', '2018-01-17 22:16:11', NULL);
INSERT INTO `Role` VALUES (2, '学生测试', 0, 0, 0, '2018-01-17 21:50:20', '2018-01-18 09:57:48', '2018-01-17 21:50:44');


-- ----------------------------
-- Records of SchoolArea
-- ----------------------------
INSERT INTO `SchoolArea` VALUES (16, '重庆理工大学', '花溪校区', 'cqut', 1, '13,14,16,');
INSERT INTO `SchoolArea` VALUES (11, '电子科技大学清水河校区', '清水河', 'dzkjdx', 3, '7,8,11,');


-- ----------------------------
-- Records of User
-- ----------------------------
INSERT INTO `User` VALUES (1, 'admin', 'admin', '123456', '28076465688@qq.com', 0, '2018-01-01 15:18:04', '2018-01-01 15:18:08', NULL, 0);

-- ----------------------------
-- Records of UserRole
-- ----------------------------
INSERT INTO `UserRole` VALUES (1, 1);

SET FOREIGN_KEY_CHECKS = 1;
