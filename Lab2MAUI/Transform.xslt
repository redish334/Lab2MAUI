<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<!-- Define parameters for filtering -->
	<xsl:param name="Name" select="''" />
	<xsl:param name="Faculty" select="''" />
	<xsl:param name="Department" select="''" />
	<xsl:param name="Course" select="''" />
	<xsl:param name="RoomNumber" select="''" />
	<xsl:param name="PhoneNumber" select="''" />
	<xsl:param name="PaymentStatus" select="''" />
	<xsl:param name="AvailabilityOfBenefits" select="''" />
	<xsl:param name="subordination" select="''" />
	<xsl:param name="Address" select="''" />

	<!-- Define parameters for column visibility -->
	<xsl:param name="showName" select="true()" />
	<xsl:param name="showFaculty" select="true()" />
	<xsl:param name="showDepartment" select="true()" />
	<xsl:param name="showCourse" select="true()" />
	<xsl:param name="showRoomNumber" select="true()" />
	<xsl:param name="showPhoneNumber" select="true()" />
	<xsl:param name="showPaymentStatus" select="true()" />
	<xsl:param name="showAvailabilityOfBenefits" select="true()" />
	<xsl:param name="showSubordination" select="true()" />
	<xsl:param name="showAddress" select="true()" />

	<xsl:template match="/">
		<html>
			<head>
				<style>
					table { border-collapse: collapse; width: 100%; }
					th, td { border: 1px solid black; padding: 8px; text-align: left; }
					th { background-color: #f2f2f2; }
				</style>
			</head>
			<body>
				<table>
					<tr>
						<xsl:if test="$showName='true'">
							<th>Name</th>
						</xsl:if>
						<xsl:if test="$showFaculty='true'">
							<th>Faculty</th>
						</xsl:if>
						<xsl:if test="$showDepartment='true'">
							<th>Department</th>
						</xsl:if>
						<xsl:if test="$showCourse='true'">
							<th>Course</th>
						</xsl:if>
						<xsl:if test="$showRoomNumber='true'">
							<th>Room Number</th>
						</xsl:if>
						<xsl:if test="$showPhoneNumber='true'">
							<th>Phone Number</th>
						</xsl:if>
						<xsl:if test="$showPaymentStatus='true'">
							<th>Payment Status</th>
						</xsl:if>
						<xsl:if test="$showAvailabilityOfBenefits='true'">
							<th>Availability of Benefits</th>
						</xsl:if>
						<xsl:if test="$showSubordination='true'">
							<th>Subordination</th>
						</xsl:if>
						<xsl:if test="$showAddress='true'">
							<th>Address</th>
						</xsl:if>
					</tr>

					<xsl:for-each select="//student[
                        ($Name='' or @NAME=$Name) and
                        ($Faculty='' or @FACULTY=$Faculty) and
                        ($Department='' or @DEPARTMENT=$Department) and
                        ($Course='' or @COURSE=$Course) and
                        ($RoomNumber='' or @ROOMNUMBER=$RoomNumber) and
                        ($PhoneNumber='' or @PHONENUMBER=$PhoneNumber) and
                        ($PaymentStatus='' or @PAYMENTSTATUS=$PaymentStatus) and
                        ($AvailabilityOfBenefits='' or @AVAILABILITYOFBENEFITS=$AvailabilityOfBenefits) and
                        ($subordination='' or @SUBORDINATION=$subordination) and
                        ($Address='' or @ADDRESS=$Address)
                    ]">
						<tr>
							<xsl:if test="$showName='true'">
								<td>
									<xsl:value-of select="@NAME"/>
								</td>
							</xsl:if>
							<xsl:if test="$showFaculty='true'">
								<td>
									<xsl:value-of select="@FACULTY"/>
								</td>
							</xsl:if>
							<xsl:if test="$showDepartment='true'">
								<td>
									<xsl:value-of select="@DEPARTMENT"/>
								</td>
							</xsl:if>
							<xsl:if test="$showCourse='true'">
								<td>
									<xsl:value-of select="@COURSE"/>
								</td>
							</xsl:if>
							<xsl:if test="$showRoomNumber='true'">
								<td>
									<xsl:value-of select="@ROOMNUMBER"/>
								</td>
							</xsl:if>
							<xsl:if test="$showPhoneNumber='true'">
								<td>
									<xsl:value-of select="@PHONENUMBER"/>
								</td>
							</xsl:if>
							<xsl:if test="$showPaymentStatus='true'">
								<td>
									<xsl:value-of select="@PAYMENTSTATUS"/>
								</td>
							</xsl:if>
							<xsl:if test="$showAvailabilityOfBenefits='true'">
								<td>
									<xsl:value-of select="@AVAILABILITYOFBENEFITS"/>
								</td>
							</xsl:if>
							<xsl:if test="$showSubordination='true'">
								<td>
									<xsl:value-of select="@SUBORDINATION"/>
								</td>
							</xsl:if>
							<xsl:if test="$showAddress='true'">
								<td>
									<xsl:value-of select="@ADDRESS"/>
								</td>
							</xsl:if>
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
