/*
 Navicat Premium Data Transfer

 Source Server         : myServer
 Source Server Type    : MySQL
 Source Server Version : 50721
 Source Host           : 119.28.178.12:3306
 Source Schema         : EmsDB

 Target Server Type    : MySQL
 Target Server Version : 50721
 File Encoding         : 65001

 Date: 12/05/2018 23:52:25
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for Area
-- ----------------------------
DROP TABLE IF EXISTS `Area`;
CREATE TABLE `Area`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Code` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `ParentId` int(11) NOT NULL,
  `Path` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `CodeId` char(36) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 19 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

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
INSERT INTO `Area` VALUES (17, '绵阳', 'my', 7, '7,17,', '977402fc-30a4-40e3-b5c7-ca1b7387fa3b');
INSERT INTO `Area` VALUES (18, '雅安', 'ya', 7, '7,18,', '1130d7f0-0bed-43d2-b4dd-24472f299523');

-- ----------------------------
-- Table structure for AreaRecruit
-- ----------------------------
DROP TABLE IF EXISTS `AreaRecruit`;
CREATE TABLE `AreaRecruit`  (
  `RecruitMentId` int(11) NOT NULL,
  `AreaId` int(11) NOT NULL,
  PRIMARY KEY (`RecruitMentId`, `AreaId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of AreaRecruit
-- ----------------------------
INSERT INTO `AreaRecruit` VALUES (9, 14);
INSERT INTO `AreaRecruit` VALUES (9, 17);
INSERT INTO `AreaRecruit` VALUES (10, 8);
INSERT INTO `AreaRecruit` VALUES (10, 14);

-- ----------------------------
-- Table structure for AuditLog
-- ----------------------------
DROP TABLE IF EXISTS `AuditLog`;
CREATE TABLE `AuditLog`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` int(11) NULL DEFAULT NULL,
  `ServiceName` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `MethodName` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Parameters` varchar(1024) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ExecutionTime` datetime(0) NOT NULL,
  `ExecutionDuration` int(11) NOT NULL,
  `ClientIpAddress` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `ClientName` varchar(128) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `BrowserInfo` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Exception` varchar(2000) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for BriefingContent
-- ----------------------------
DROP TABLE IF EXISTS `BriefingContent`;
CREATE TABLE `BriefingContent`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CompanyName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `SchoolId` int(11) NULL DEFAULT NULL COMMENT '举办学校',
  `Held` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '举办详细位置',
  `Title` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '标题',
  `Content` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL COMMENT '内容',
  `OpthonList` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `PublishTime` datetime(0) NULL DEFAULT NULL COMMENT '发布时间',
  `StartTime` datetime(0) NULL DEFAULT NULL COMMENT '开始时间',
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of BriefingContent
-- ----------------------------
INSERT INTO `BriefingContent` VALUES (2, 'cs', 3, 'cs', 'ces', '<p>cs</p>', NULL, '2018-05-09 23:22:02', '2018-05-12 12:23:00');
INSERT INTO `BriefingContent` VALUES (3, '啊打发', 1, '啊打发打发23090923', '234', '<p>234546</p>', NULL, '2018-05-10 09:49:08', '2018-05-17 13:00:00');

-- ----------------------------
-- Table structure for Company
-- ----------------------------
DROP TABLE IF EXISTS `Company`;
CREATE TABLE `Company`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `NAME` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Address` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `CreateTime` datetime(0) NULL DEFAULT NULL,
  `LegalPerson` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `tellphone` varchar(15) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Description` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `Email` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 14 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Company
-- ----------------------------
INSERT INTO `Company` VALUES (9, 'cs1', 'cs1', '2018-05-02 10:04:12', 'cs1', '18723361304', '<p>afasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsfafasdfasdfsdafadfadsf</p>', '268165@qq.com');
INSERT INTO `Company` VALUES (10, 'cs2', 'vcs', '2018-05-03 17:02:30', 'qhao', '111111111', 'dfasdfasdfasdfasdf', '29845894@qq.com');
INSERT INTO `Company` VALUES (11, '1', '1', '2018-05-03 17:04:30', '1', '1', NULL, '1');
INSERT INTO `Company` VALUES (12, '2', '2', '2018-05-03 18:08:44', '2', '2', NULL, '2');
INSERT INTO `Company` VALUES (13, 'test', '12342345', '2018-05-12 21:35:54', 'test', '123435', NULL, '245234@qq.com');

-- ----------------------------
-- Table structure for CompanyUser
-- ----------------------------
DROP TABLE IF EXISTS `CompanyUser`;
CREATE TABLE `CompanyUser`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` int(11) NOT NULL,
  `CompanyId` int(11) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `UserId`(`UserId`) USING BTREE,
  INDEX `CompanyId`(`CompanyId`) USING BTREE,
  CONSTRAINT `CompanyUser_ibfk_1` FOREIGN KEY (`UserId`) REFERENCES `User` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `CompanyUser_ibfk_2` FOREIGN KEY (`CompanyId`) REFERENCES `Company` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 6 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of CompanyUser
-- ----------------------------
INSERT INTO `CompanyUser` VALUES (2, 4, 9);
INSERT INTO `CompanyUser` VALUES (3, 6, 10);
INSERT INTO `CompanyUser` VALUES (4, 8, 9);
INSERT INTO `CompanyUser` VALUES (5, 9, 9);

-- ----------------------------
-- Table structure for FileRelation
-- ----------------------------
DROP TABLE IF EXISTS `FileRelation`;
CREATE TABLE `FileRelation`  (
  `CompanyId` int(11) NOT NULL,
  `UserId` int(11) NOT NULL,
  `RecruitId` int(11) NOT NULL,
  `displayName` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `realName` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  `CreateTime` datetime(0) NULL DEFAULT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for Major
-- ----------------------------
DROP TABLE IF EXISTS `Major`;
CREATE TABLE `Major`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Code` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 4 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci COMMENT = '专业' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Major
-- ----------------------------
INSERT INTO `Major` VALUES (1, '计算机', 'computer');
INSERT INTO `Major` VALUES (2, '测试1', 'cs');
INSERT INTO `Major` VALUES (3, '测试2', 'cs23');

-- ----------------------------
-- Table structure for MajorRecruitMent
-- ----------------------------
DROP TABLE IF EXISTS `MajorRecruitMent`;
CREATE TABLE `MajorRecruitMent`  (
  `MajorId` int(11) NOT NULL,
  `RecruitMentId` int(11) NOT NULL,
  PRIMARY KEY (`MajorId`, `RecruitMentId`) USING BTREE,
  INDEX `MajorRecruitMent_ibfk_2`(`RecruitMentId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of MajorRecruitMent
-- ----------------------------
INSERT INTO `MajorRecruitMent` VALUES (3, 7);
INSERT INTO `MajorRecruitMent` VALUES (1, 9);
INSERT INTO `MajorRecruitMent` VALUES (3, 10);

-- ----------------------------
-- Table structure for NewArticle
-- ----------------------------
DROP TABLE IF EXISTS `NewArticle`;
CREATE TABLE `NewArticle`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Title` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `Content` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `PublishTime` datetime(0) NULL DEFAULT NULL,
  `IsDelete` tinyint(1) NULL DEFAULT NULL,
  `SubContent` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 9 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

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
-- Table structure for Organization
-- ----------------------------
DROP TABLE IF EXISTS `Organization`;
CREATE TABLE `Organization`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ParentId` int(11) NULL DEFAULT NULL,
  `Name` varchar(64) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Path` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL,
  `CreationTime` datetime(0) NOT NULL,
  `LastModificationTime` datetime(0) NULL DEFAULT NULL,
  `DeletionTime` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 5 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Organization
-- ----------------------------
INSERT INTO `Organization` VALUES (1, NULL, '本公司', '1,', 0, '2018-01-18 09:58:21', '2018-01-18 09:58:21', NULL);
INSERT INTO `Organization` VALUES (2, 1, '测试1', '1,2,', 0, '2018-03-01 20:36:53', '2018-03-01 20:38:39', NULL);
INSERT INTO `Organization` VALUES (3, NULL, '测试1', '3,', 0, '2018-03-01 20:37:39', '2018-03-01 20:37:39', NULL);
INSERT INTO `Organization` VALUES (4, NULL, '四川', '4,', 1, '2018-04-06 17:14:48', '2018-04-07 20:50:18', '2018-04-07 20:50:18');

-- ----------------------------
-- Table structure for Permission
-- ----------------------------
DROP TABLE IF EXISTS `Permission`;
CREATE TABLE `Permission`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleId` int(11) NULL DEFAULT NULL,
  `UserId` int(11) NULL DEFAULT NULL,
  `Code` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `CreationTime` datetime(0) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 192 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Permission
-- ----------------------------
INSERT INTO `Permission` VALUES (154, 1, NULL, 'Total.BaseMenu.Article', '2018-05-12 19:19:49');
INSERT INTO `Permission` VALUES (155, 1, NULL, 'Total.BaseMenu.Company', '2018-05-12 19:19:49');
INSERT INTO `Permission` VALUES (156, 1, NULL, 'Total.BaseMenu.SchoolArea', '2018-05-12 19:19:49');
INSERT INTO `Permission` VALUES (157, 1, NULL, 'Total.BaseMenu.Major', '2018-05-12 19:19:49');
INSERT INTO `Permission` VALUES (158, 1, NULL, 'Total.BaseMenu.Organization', '2018-05-12 19:19:49');
INSERT INTO `Permission` VALUES (159, 1, NULL, 'Total.BaseMenu.Organization.RemoveUser', '2018-05-12 19:19:49');
INSERT INTO `Permission` VALUES (160, 1, NULL, 'Total.BaseMenu.Organization.AddUser', '2018-05-12 19:19:49');
INSERT INTO `Permission` VALUES (161, 1, NULL, 'Total.BaseMenu.Organization.Migrate', '2018-05-12 19:19:49');
INSERT INTO `Permission` VALUES (162, 1, NULL, 'Total.BaseMenu.Organization.Rename', '2018-05-12 19:19:49');
INSERT INTO `Permission` VALUES (163, 1, NULL, 'Total.BaseMenu.Organization.Delete', '2018-05-12 19:19:49');
INSERT INTO `Permission` VALUES (164, 1, NULL, 'Total.BaseMenu.Organization.Create', '2018-05-12 19:19:49');
INSERT INTO `Permission` VALUES (165, 1, NULL, 'Total.BaseMenu', '2018-05-12 19:19:49');
INSERT INTO `Permission` VALUES (166, 1, NULL, 'Total.BaseMenu.Role.Delete', '2018-05-12 19:19:49');
INSERT INTO `Permission` VALUES (167, 1, NULL, 'Total.BaseMenu.Role.Update', '2018-05-12 19:19:49');
INSERT INTO `Permission` VALUES (168, 1, NULL, 'Total.BaseMenu.Role.Create', '2018-05-12 19:19:49');
INSERT INTO `Permission` VALUES (169, 1, NULL, 'Total.BaseMenu.User', '2018-05-12 19:19:49');
INSERT INTO `Permission` VALUES (170, 1, NULL, 'Total.BaseMenu.User.Role', '2018-05-12 19:19:49');
INSERT INTO `Permission` VALUES (171, 1, NULL, 'Total.BaseMenu.User.Permission', '2018-05-12 19:19:49');
INSERT INTO `Permission` VALUES (172, 1, NULL, 'Total.BaseMenu.User.Delete', '2018-05-12 19:19:49');
INSERT INTO `Permission` VALUES (173, 1, NULL, 'Total.BaseMenu.User.Edit', '2018-05-12 19:19:49');
INSERT INTO `Permission` VALUES (174, 1, NULL, 'Total.BaseMenu.User.Create', '2018-05-12 19:19:49');
INSERT INTO `Permission` VALUES (175, 1, NULL, 'Total', '2018-05-12 19:19:49');
INSERT INTO `Permission` VALUES (176, 1, NULL, 'Total.BaseMenu.Role', '2018-05-12 19:19:49');
INSERT INTO `Permission` VALUES (180, 2, NULL, 'Total.BaseMenu.CareerTalk', '2018-05-12 19:20:27');
INSERT INTO `Permission` VALUES (181, 2, NULL, 'Total.BaseMenu.Article', '2018-05-12 19:20:27');
INSERT INTO `Permission` VALUES (182, 2, NULL, 'Total', '2018-05-12 19:20:27');
INSERT INTO `Permission` VALUES (183, 2, NULL, 'Total.BaseMenu', '2018-05-12 19:20:27');
INSERT INTO `Permission` VALUES (188, 3, NULL, 'Total.BaseMenu.RecruitMent', '2018-05-12 20:00:38');
INSERT INTO `Permission` VALUES (189, 3, NULL, 'Total.BaseMenu.Resument', '2018-05-12 20:00:38');
INSERT INTO `Permission` VALUES (190, 3, NULL, 'Total', '2018-05-12 20:00:38');
INSERT INTO `Permission` VALUES (191, 3, NULL, 'Total.BaseMenu', '2018-05-12 20:00:38');

-- ----------------------------
-- Table structure for Recruitment
-- ----------------------------
DROP TABLE IF EXISTS `Recruitment`;
CREATE TABLE `Recruitment`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `CompanyId` int(11) NULL DEFAULT NULL,
  `Title` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Content` text CHARACTER SET utf8 COLLATE utf8_general_ci NULL,
  `CreateTime` datetime(0) NULL DEFAULT NULL,
  `EndTime` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 11 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci COMMENT = '职位招聘表\r\n' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Recruitment
-- ----------------------------
INSERT INTO `Recruitment` VALUES (7, 9, '安保官', '<p>安保官&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 这就是楷书，<font face=\"楷体, 楷体_GB2312, SimKai\">不是说好就是楷书么</font></p>', '2018-05-02 15:25:05', '2018-07-19 00:00:00');
INSERT INTO `Recruitment` VALUES (9, 10, '诈骗安保', '<p>诈骗安保，没错就是这么诈骗的</p>', '2018-05-02 15:32:25', '2018-06-01 00:00:00');
INSERT INTO `Recruitment` VALUES (10, 9, 'test', '<p>test<img src=\"http://localhost:59932/upload/image/20180511/6366165468114128099288288.jpg\" title=\"1024x768_d7a3791d242d0df.jpg\" alt=\"1024x768_d7a3791d242d0df.jpg\" width=\"458\" height=\"254\"/></p>', '2018-05-03 20:49:21', '2018-05-18 00:00:00');

-- ----------------------------
-- Table structure for Role
-- ----------------------------
DROP TABLE IF EXISTS `Role`;
CREATE TABLE `Role`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `IsStatic` tinyint(1) NOT NULL,
  `IsDefault` tinyint(1) NOT NULL,
  `IsDeleted` tinyint(1) NOT NULL,
  `CreationTime` datetime(0) NOT NULL,
  `LastModificationTime` datetime(0) NULL DEFAULT NULL,
  `DeletionTime` datetime(0) NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 5 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of Role
-- ----------------------------
INSERT INTO `Role` VALUES (1, 'admin', 1, 1, 0, '2018-01-15 10:21:35', '2018-05-12 19:19:49', NULL);
INSERT INTO `Role` VALUES (2, '教师', 1, 1, 0, '2018-05-06 16:03:26', '2018-05-12 19:20:27', NULL);
INSERT INTO `Role` VALUES (3, '公司', 1, 1, 0, '2018-05-06 16:04:01', '2018-05-12 20:00:38', NULL);
INSERT INTO `Role` VALUES (4, '学生测试', 0, 0, 0, '2018-01-17 21:50:20', '2018-01-18 09:57:48', '2018-01-17 21:50:44');

-- ----------------------------
-- Table structure for SchoolArea
-- ----------------------------
DROP TABLE IF EXISTS `SchoolArea`;
CREATE TABLE `SchoolArea`  (
  `AreaId` int(11) NOT NULL,
  `Name` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Address` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Code` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Path` varchar(255) CHARACTER SET latin1 COLLATE latin1_swedish_ci NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE,
  INDEX `FK_Reference_7`(`AreaId`) USING BTREE,
  CONSTRAINT `FK_Reference_7` FOREIGN KEY (`AreaId`) REFERENCES `Area` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB AUTO_INCREMENT = 5 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of SchoolArea
-- ----------------------------
INSERT INTO `SchoolArea` VALUES (16, '重庆理工大学', '花溪校区', 'cqut', 1, '13,14,16,');
INSERT INTO `SchoolArea` VALUES (11, '电子科技大学清水河校区', '清水河', 'dzkjdx', 3, '7,8,11,');

-- ----------------------------
-- Table structure for SchoolUser
-- ----------------------------
DROP TABLE IF EXISTS `SchoolUser`;
CREATE TABLE `SchoolUser`  (
  `SchoolId` int(11) NOT NULL,
  `UserId` int(11) NOT NULL,
  PRIMARY KEY (`SchoolId`, `UserId`) USING BTREE,
  INDEX `UserId`(`UserId`) USING BTREE,
  CONSTRAINT `SchoolUser_ibfk_1` FOREIGN KEY (`SchoolId`) REFERENCES `SchoolArea` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `SchoolUser_ibfk_2` FOREIGN KEY (`UserId`) REFERENCES `User` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of SchoolUser
-- ----------------------------
INSERT INTO `SchoolUser` VALUES (1, 7);
INSERT INTO `SchoolUser` VALUES (1, 10);

-- ----------------------------
-- Table structure for User
-- ----------------------------
DROP TABLE IF EXISTS `User`;
CREATE TABLE `User`  (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `UserName` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `Password` varchar(32) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `EmailAddress` varchar(256) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL,
  `IsDeleted` tinyint(1) NOT NULL,
  `CreationTime` datetime(0) NOT NULL,
  `LastModificationTime` datetime(0) NULL DEFAULT NULL,
  `DeletionTime` datetime(0) NULL DEFAULT NULL,
  `UserType` int(255) NOT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 11 CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of User
-- ----------------------------
INSERT INTO `User` VALUES (1, 'admin', 'admin', '123456', '28076465688@qq.com', 0, '2018-01-01 15:18:04', '2018-01-01 15:18:08', NULL, 0);
INSERT INTO `User` VALUES (4, 'qh', 'qh', '123456', 'asdf', 0, '2018-05-02 10:20:53', '2018-05-02 10:20:53', NULL, 2);
INSERT INTO `User` VALUES (5, '测试1', 'vcs', '123456', '156498@qq.com', 0, '2018-05-02 19:26:19', '2018-05-02 19:26:19', NULL, 1);
INSERT INTO `User` VALUES (6, '123', '123', '123456', '123@qq.com', 0, '2018-05-03 17:02:45', '2018-05-03 17:02:45', NULL, 2);
INSERT INTO `User` VALUES (7, 'qh', 'qh', '123456', '123456@qq.com', 0, '2018-05-09 23:09:55', '2018-05-09 23:09:55', NULL, 4);
INSERT INTO `User` VALUES (8, 'za', 'za', '123456', '1897@qq.com', 0, '2018-05-12 20:30:37', '2018-05-12 20:30:37', NULL, 2);
INSERT INTO `User` VALUES (9, '12', '12', '123456', '123@qq.com', 0, '2018-05-12 20:33:58', '2018-05-12 20:33:58', NULL, 2);
INSERT INTO `User` VALUES (10, 'zs', 'zs', '123456', 'zs@qq.com', 0, '2018-05-12 23:37:19', '2018-05-12 23:37:19', NULL, 4);

-- ----------------------------
-- Table structure for UserOrganization
-- ----------------------------
DROP TABLE IF EXISTS `UserOrganization`;
CREATE TABLE `UserOrganization`  (
  `UserId` int(11) NOT NULL,
  `OrganizationId` int(11) NOT NULL,
  PRIMARY KEY (`UserId`, `OrganizationId`) USING BTREE,
  INDEX `FK_Reference_4`(`OrganizationId`) USING BTREE,
  CONSTRAINT `FK_Reference_3` FOREIGN KEY (`UserId`) REFERENCES `User` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_Reference_4` FOREIGN KEY (`OrganizationId`) REFERENCES `Organization` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Table structure for UserRole
-- ----------------------------
DROP TABLE IF EXISTS `UserRole`;
CREATE TABLE `UserRole`  (
  `UserId` int(11) NOT NULL,
  `RoleId` int(11) NOT NULL,
  PRIMARY KEY (`UserId`, `RoleId`) USING BTREE,
  INDEX `FK_Reference_2`(`RoleId`) USING BTREE,
  CONSTRAINT `FK_Reference_1` FOREIGN KEY (`UserId`) REFERENCES `User` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_Reference_2` FOREIGN KEY (`RoleId`) REFERENCES `Role` (`Id`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE = InnoDB CHARACTER SET = latin1 COLLATE = latin1_swedish_ci COMMENT = '用户角色表' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of UserRole
-- ----------------------------
INSERT INTO `UserRole` VALUES (1, 1);
INSERT INTO `UserRole` VALUES (5, 1);
INSERT INTO `UserRole` VALUES (6, 1);
INSERT INTO `UserRole` VALUES (8, 1);
INSERT INTO `UserRole` VALUES (7, 2);
INSERT INTO `UserRole` VALUES (8, 2);
INSERT INTO `UserRole` VALUES (10, 2);
INSERT INTO `UserRole` VALUES (4, 3);
INSERT INTO `UserRole` VALUES (8, 3);
INSERT INTO `UserRole` VALUES (9, 3);

SET FOREIGN_KEY_CHECKS = 1;
