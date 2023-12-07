<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:template match="/">
		<html>
			<head>
				<title>Трансформація</title>
				<style>
					table {
					font-family: Arial, sans-serif;
					border-collapse: collapse;
					width: 100%;
					margin-top: 20px;
					}

					th, td {
					border: 1px solid #dddddd;
					text-align: left;
					padding: 8px;
					}

					th {
					background-color: #f2f2f2;
					}
				</style>
			</head>
			<body>
				<h1>Інформація</h1>
				<table>
					<tr>
						<th>П.І.П.</th>
						<th>ФАКУЛЬТЕТ</th>
						<th>ДЕПАРТАМЕНТ</th>
						<th>КАФЕДРА</th>
						<th>ОСВІТА</th>
						<th>НАВЧАЛЬНИЙ ЗАКЛАД</th>
						<th>ДАТА ПОЧАТКУ</th>
						<th>ДАТА КІНЦЯ</th>
					</tr>
					<xsl:apply-templates select="//Cadr"/>
				</table>
			</body>
		</html>
	</xsl:template>

	<xsl:template match="Cadr">
		<tr>
			<td>
				<xsl:value-of select="Fullname"/>
			</td>
			<td>
				<xsl:value-of select="Facultyinformation/NameOfFac"/>
			</td>
			<td>
				<xsl:value-of select="Facultyinformation/Department"/>
			</td>
			<td>
				<xsl:value-of select="Chair"/>
			</td>
			<td>
				<xsl:value-of select="TypeOfeducation"/>
			</td>
			<td>
				<xsl:value-of select="EducInstitution"/>
			</td>
			<td>
				<xsl:value-of select="StartDate"/>
			</td>
			<td>
				<xsl:value-of select="FinalDate"/>
			</td>
		</tr>
	</xsl:template>

</xsl:stylesheet>
