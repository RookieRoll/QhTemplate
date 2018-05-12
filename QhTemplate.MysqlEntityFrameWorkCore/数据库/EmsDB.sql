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

 Date: 12/05/2018 23:51:51
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
-- Table structure for AreaRecruit
-- ----------------------------
DROP TABLE IF EXISTS `AreaRecruit`;
CREATE TABLE `AreaRecruit`  (
  `RecruitMentId` int(11) NOT NULL,
  `AreaId` int(11) NOT NULL,
  PRIMARY KEY (`RecruitMentId`, `AreaId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = latin1 COLLATE = latin1_swedish_ci ROW_FORMAT = Dynamic;

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

SET FOREIGN_KEY_CHECKS = 1;
