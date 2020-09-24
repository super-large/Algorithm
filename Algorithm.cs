//******************************************************************** 
//项目名称：    质量管理系统
//程序集名：    HT.Algorithm
//文件名称：    Algorithm。cs
//创 建 者：    白燕生
//创建日期：    2012.8.21
//文件功能：    水泥行业基本运算公式及配比算法
//更新纪录：
//  日  期        姓   名                    描述
// ========      =========  ========================================
//********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Modules;


namespace HT.Algorithm
{
    public class Algorithm
    {
        #region 成员变量
        #endregion

        //算法运算包括    生料配比计算率值(方案2)  decimal[] RateValueCalculate(decimal[,] adRaw, decimal[] adCoal, decimal[] adFQL, decimal[] adKHSMIM, out string strErrorMessage)
        //                                                                        adRaw             11*6矩阵，(原料名称) 石灰石 砂土 矾土 铁矿石 粉煤灰 煤灰  (检测项目) CaO SiO2 Al2O3 Fe2O3 MgO SO3 K2O Na2O Cl LOI My （后两个为烧失量和水分）
        //                                                                        adCoal            1*6矩阵，煤粉检测项目(水分 灰分 挥发份 固定碳 全硫 热值) My Af Vf Cf Sf Qf 
        //                                                                        adFQL             1*3矩阵，游离氧化钙：fC ，系统热耗：Qy ， 熟料烧失量 LOI
        //                                                                        adW               1*4矩阵，调整配比  (原料名称) 石灰石 砂土 矾土 铁矿石
        //                                                                        adClinker         1*11矩阵，熟料化学成分 (检测项目) CaO SiO2 Al2O3 Fe2O3 MgO SO3 K2O Na2O Cl LOI My （后两个为烧失量和水分）
        //                                                                        strErrorMessage   出错信息
        //                                                                        返回              1*7矩阵，熟料、生料率值(Clinker KH、KH-、SM、IM；Raw KH、SM、IM）
           

        //算法运算包括    生料率值计算配比(方案1)  decimal[] RatioCalculate(decimal[,] adRaw, decimal[] adCoal, decimal[] adFQL, decimal[] adKHSMIM, out string strErrorMessage)
        //                                                                        adRaw             11*6矩阵，(原料名称) 石灰石 砂土 矾土 铁矿石 粉煤灰 煤灰  (检测项目) CaO SiO2 Al2O3 Fe2O3 MgO SO3 K2O Na2O Cl LOI My （后两个为烧失量和水分）
        //                                                                        adCoal            1*6矩阵，煤粉检测项目(水分 灰分 挥发份 固定碳 全硫 热值) My Af Vf Cf Sf Qf 
        //                                                                        adFQL             1*3矩阵，游离氧化钙：fC ，系统热耗：Qy ， 熟料烧失量 LOI
        //                                                                        adKHSMIM          1*3矩阵，熟料目标率值 KH SM IM
        //                                                                        strErrorMessage   出错信息
        //                                                                        返回              1*8矩阵，湿基配比和干基配比 (原料名称) 石灰石 砂土 矾土 铁矿石

        //算法运算包括    水泥配比计算四种原料    decimal[] CementRatioCalculate4(decimal[,] adSMLC, decimal[] adCement, decimal[,] adW, out string strErrorMessage)
        //                                                                        adSMLC            4*4矩阵，(原料名称)熟料 石膏 矿渣 石灰石  (检测项目)SO3 MgO Loss常数
        //                                                                        adCement          4*1矩阵，水泥的检测项目 SO3 MgO Loss 常数
        //                                                                        adW               3*4矩阵，前三个周期水泥原料配比</param>
        //                                                                        strErrorMessage   出错信息
        //                                                                        返回              1*4矩阵，水泥配比输出  (原料名称)熟料 石膏 矿渣 石灰石

        //算法运算包括    水泥配比计算五种原料    decimal[] CementRatioCalculate5(decimal[,] adSMLC, decimal[] adCement, decimal[,] adW, out string strErrorMessage)
        //                                                                        adSMLC            5*5矩阵，(原料名称)熟料 石膏 矿渣 石灰石  其他原料(检测项目)SO3 MgO Loss  CaO 常数
        //                                                                        adCement          5*1矩阵，水泥的检测项目 SO3 MgO Loss  CaO 常数
        //                                                                        adW               3*5矩阵，前三个周期水泥原料配比</param>
        //                                                                        strErrorMessage   出错信息
        //                                                                        返回              1*5矩阵，水泥配比输出  (原料名称)熟料 石膏 矿渣 石灰石  其他原料

        //算法运算包括    生料配比输出计算    decimal[] RatioOutputCalculate(decimal[,] adSAFC, decimal[] adW, decimal[] adKHSMIM, decimal[,] adKhSmIm, out string strErrorMessage)
        //                                                                        adSAFC            4*4矩阵，(原料名称)石灰石 砂岩 铁矿石 铝矾土 (化学成分名称)SiO2 Al2O3 Fe2O3 CaO
        //                                                                        adW               1*4矩阵，上周期原料输出配比
        //                                                                        adKHSMIM          1*3矩阵，当前目标率值  KH SM IM
        //                                                                        adKhSmIm          3*3矩阵，荧光分析上1、2、3个周期率值  KH SM IM
        //                                                                        adRight           1*3矩阵，权重值
        //                                                                        strErrorMessage   出错信息
        //                                                                        返回              1*4矩阵，配比输出  (原料名称)石灰石 砂岩 铁矿石 铝矾土   加3个：下周期KH、SM、IM   （最终为1*7矩阵）

        #region 算法运算    生料配比计算率值RateValueCalculate()    生料率值计算配比RatioCalculate()    水泥配比计算CementRatioCalculate()    配比输出计算RatioOutputCalculate()

        #region 生料配比计算率值
        /// <summary>
        /// 生料配比计算率值(方案2)
        /// </summary>
        /// <param name="adRaw">11*6矩阵，(原料名称) 石灰石 砂土 矾土 铁矿石 粉煤灰 煤灰  (检测项目) CaO SiO2 Al2O3 Fe2O3 MgO SO3 K2O Na2O Cl LOI My （后两个为烧失量和水分）</param>
        /// <param name="adCoal">1*6矩阵，煤粉检测项目(水分 灰分 挥发份 固定碳 全硫 热值) My Af Vf Cf Sf Qf</param>
        /// <param name="adFQL">1*3矩阵，游离氧化钙：fC ，系统热耗：Qy ， 熟料烧失量 LOI</param>
        /// <param name="adW">1*4矩阵，调整配比  (原料名称) 石灰石 砂土 矾土 铁矿石</param>
        /// <param name="adClinker">1*11矩阵，熟料化学成分 (检测项目) CaO SiO2 Al2O3 Fe2O3 MgO SO3 K2O Na2O Cl LOI My （后两个为烧失量和水分）</param>
        /// <param name="strErrorMessage">出错信息</param>
        /// <returns>熟料、生料率值(Clinker KH、KH-、SM、IM；Raw KH、SM、IM)</returns>
        public decimal[] RateValueCalculate(decimal[,] adRaw, decimal[] adCoal, decimal[] adFQL, decimal[] adW, out string strErrorMessage, out decimal[] adClinker)
        {
            //输出变量 
            decimal[] adOutput = new decimal[19] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };  //熟料、生料率值   KH SM IM
            adClinker = new decimal[11] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };         //熟料化学成分
            strErrorMessage = "";

            //中间计算变量
            int i, j;
            decimal[] adTmp = new decimal[7];
            decimal[] adRawMaterial = new decimal[11];    //生料化学成分
            decimal[] adBurningBase = new decimal[11];    //灼烧基生料化学成分
            //decimal[] adClinker = new decimal[11];        //熟料化学成分
            decimal dAr;                                 //煤灰掺入率

            //计算生料的干基配比    A=a*(100-My)/100
            adTmp[4] = 0;
            for(i = 0; i < 4; i++)
            {
                adTmp[i] = adW[i] * ( 100 - adRaw[10, i]) / 100;
                adTmp[4] = adTmp[4] + adTmp[i];
            }
            if (Math.Abs(adTmp[4]) < decimal.Parse("0.000001"))
            {
                strErrorMessage = "计算生料的干基配比数据异常，生料的干基配比之和 < 0.000001";
                return adOutput;
            }
            for (i = 0; i < 4; i++)
            {
                adTmp[i] = adTmp[i] / adTmp[4] * 100;
            }

            //计算生料化学成分  化学成分=Σ(各原料组分*生料干基配比)
            for (i = 0; i < 11; i++)
            {
                adRawMaterial[i] = 0;
                for (j = 0; j < 4; j++)
                {
                    adRawMaterial[i] = adRawMaterial[i] + adRaw[i, j] * adTmp[j] / 100;
                }
            }

            //计算灼烧基生料化学成分  CaO‘=CaO/(100-L生)*100
            if (Math.Abs(100 - adRawMaterial[9]) < decimal.Parse("0.000001"))
            {
                strErrorMessage = "计算灼烧基生料化学成分数据异常，(100 - 生料烧失量) < 0.000001";
                return adOutput;
            }
            for (i = 0; i < 11; i++)
            {
                adBurningBase[i] = adRawMaterial[i] / (100 - adRawMaterial[9]) * 100;
            }

            //计算煤灰掺入率：Ar=Af*Qy/Qf
            if (Math.Abs(adCoal[5]) < decimal.Parse("0.000001"))
            {
                strErrorMessage = "计算煤灰掺入率数据异常，煤粉热值Qf < 0.000001";
                return adOutput;
            }
            dAr = adCoal[1] * adFQL[1] / adCoal[5] * 1000;

            //计算熟料化学成分  化学成分=（煤灰组分*煤灰参入率+灼烧基生料组分*（100-煤灰参入率）) *（100-LOI）/ 10000
            if (Math.Abs(100 - adFQL[2]) < decimal.Parse("0.000001"))
            {
                strErrorMessage = "计算熟料化学成分数据异常，熟料烧失量 < 0.000001";
                return adOutput;
            }
            for (i = 0; i < 11; i++)
            {
                //adClinker[i] = (adRaw[i, 5] * dAr + adBurningBase[i] * (100 - dAr)) / (100 - adFQL[2]);
                adClinker[i] = (adRaw[i, 5] * dAr + adBurningBase[i] * (100 - dAr)) * (100 - adFQL[2]) / 10000;
            }

            //计算熟料、生料率值
            adTmp[0] = CalculateKH(adClinker[0], adClinker[2], adClinker[3], adClinker[5], adClinker[1]);              //Clinker KH
            adTmp[1] = CalculateKH_(adClinker[0],adFQL[0], adClinker[2], adClinker[3], adClinker[5], adClinker[1]);    //Clinker KH-
            adTmp[2] = CalculateSM(adClinker[1], adClinker[2], adClinker[3]);                                          //Clinker SM
            adTmp[3] = CalculateIM(adClinker[2], adClinker[3]);                                                        //Clinker IM
            adTmp[4] = CalculateKH(adRawMaterial[0], adRawMaterial[2], adRawMaterial[3], adRawMaterial[5], adRawMaterial[1]);      //Raw KH
            adTmp[5] = CalculateSM(adRawMaterial[1], adRawMaterial[2], adRawMaterial[3]);                                          //Raw SM
            adTmp[6] = CalculateIM(adRawMaterial[2], adRawMaterial[3]);                                                            //Raw IM

            for (i = 0; i < 7; i++)
            {
                if (Math.Abs(1 + adTmp[i]) < decimal.Parse("0.000001"))       //判断adTmp[i]不能为-1
                {
                    strErrorMessage = "计算率值数据异常，率值溢出错误（除零），检查SiO2、Fe2O3、（Al2O3+Fe2O3）的数据";
                    return adOutput;
                }
            }
            for (i = 0; i < 7; i++)
            {
                adOutput[i] = adTmp[i];
            }

            //返回熟料、生料率值   KH SM IM
            return adOutput;
        }
        #endregion

        #region 生料率值计算配比
        /// <summary>
        /// 生料率值计算配比(方案1)
        /// </summary>
        /// <param name="adRaw">11*6矩阵，(原料名称) 石灰石 砂土 矾土 铁矿石 粉煤灰 煤灰  (检测项目) CaO SiO2 Al2O3 Fe2O3 MgO SO3 K2O Na2O Cl LOI My （后两个为烧失量和水分）</param>
        /// <param name="adCoal">1*6矩阵，煤粉检测项目(水分 灰分 挥发份 固定碳 全硫 热值) My Af Vf Cf Sf Qf</param>
        /// <param name="adFQL">1*3矩阵，游离氧化钙：fC ，系统热耗：Qy ， 熟料烧失量 LOI</param>
        /// <param name="adKHSMIM">1*3矩阵，熟料目标率值 KH SM IM</param>
        /// <param name="strErrorMessage">出错信息</param>
        /// <returns>1*8矩阵，湿基配比和干基配比   (原料名称) 石灰石 砂土 矾土 铁矿石</returns>
        public decimal[] RatioCalculate(decimal[,] adRaw, decimal[] adCoal, decimal[] adFQL, decimal[] adKHSMIM, out string strErrorMessage)
        {
            //输入变量
            decimal dKH = adKHSMIM[0], dSM = adKHSMIM[1], dIM = adKHSMIM[2];//熟料目标率值 KH SM IM

            //输出变量 
            decimal[] adOutput = new decimal[23] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };  //配比
            strErrorMessage = "";

            //中间计算变量
            decimal dAr;                                          //煤灰掺入率（灰分*熟料热耗/煤粉热值*1000）
            decimal[,] adP = new decimal[9, 5];                   //原料灼烧基化学成分
            int i, j;
            decimal[,] adABCD = new decimal[4, 4];                //构造矩阵A的数组
            decimal[] adE = new decimal[4];                       //构造矩阵B的数组
            decimal[] adY = new decimal[5];                       //理论熟料配比
            decimal[] adZ = new decimal[5];                       //理论生料配比
            decimal[] adz = new decimal[5];                       //理论干基配比
            decimal[] adX = new decimal[5];                       //湿基配比

            //计算煤灰掺入率：Ar=Af*Qy/Qf
            if (Math.Abs(adCoal[5]) < Decimal.Parse("0.000001"))
            {
                dAr = 0;
                strErrorMessage = "计算煤灰掺入率数据异常，煤粉热值Qf < 0.000001";
                return adOutput;
            }
            else dAr = adCoal[1] * adFQL[1] / adCoal[5] * 1000;   //乘以1000将千焦转换为兆焦

            //计算原料灼烧基化学成分
            for(i = 0; i < 9; i++)
            {
                for (j = 0; j < 4; j++)
                {
                    if (Math.Abs(100 - adRaw[9, j]) < Decimal.Parse("0.000001"))
                    {
                        adP[i, j] = 0;
                        strErrorMessage = "计算原料灼烧基化学成分数据异常，绝对值(100 - 原料烧失量) < 0.000001";
                        return adOutput;
                    }
                    else adP[i, j] = adRaw[i, j] / (100 - adRaw[9, j]) * 100;
                }
                adP[i, 4] = dAr * adRaw[i, 5] / 100;     //煤灰掺入量
            }

            //计算矩阵系数
            //a=2.8*KH*P2+1.65*P3+0.35*P4-P1    b=SM*（P3+P4）-P2    c=IM*P4-P3     d=1
            dKH = dKH + Decimal.Parse("0.002");
            for (i = 0; i < 4; i++)
            {
                adABCD[0, i] = Decimal.Parse("2.8") * dKH * adP[1, i] + Decimal.Parse("1.65") * adP[2, i] + Decimal.Parse("0.35") * adP[3, i] - adP[0, i];
                adABCD[1, i] = dSM * (adP[2, i] + adP[3, i]) - adP[1, i];
                adABCD[2, i] = dIM * adP[3, i] - adP[2, i];
                adABCD[3, i] = 1;
            }
            adE[0] = dAr * (adRaw[0, 5] - (Decimal.Parse("2.8") * dKH * adRaw[1, 5] + Decimal.Parse("1.65") * adRaw[2, 5] + Decimal.Parse("0.35") * adRaw[3, 5]) );
            adE[1] = dAr * (adRaw[1, 5] - dSM * (adRaw[2, 5] + adRaw[3, 5]) );
            adE[2] = dAr * (adRaw[2, 5] - dIM * adRaw[3, 5]);
            adE[3] = 100 - dAr;

            Matrix MatrixA = new Matrix(adABCD);
            Matrix MatrixB = new Matrix(4, 1, adE);
            bool bResult = MatrixA.InvertGaussJordan();
            Matrix MatrixD = MatrixA.Multiply(MatrixB);

            //理论熟料配比： Y=y1+y2y3+y4，Y1=y1/Y*100，Y2=y2/Y*100，Y3=y3/Y*100，Y4=y4/Y*100
            adY[0] = MatrixD.GetElement(0, 0) + MatrixD.GetElement(1, 0) + MatrixD.GetElement(2, 0) + MatrixD.GetElement(3, 0);
            if (Math.Abs(adY[0]) < Decimal.Parse("0.000001"))
            {
                for (i = 0; i < 4; i++)
                {
                    adY[i + 1] = 0;
                }
                strErrorMessage = "计算理论熟料配比数据异常，理论熟料配比之和 < 0.000001";
                return adOutput;
            }
            else
            {
                for (i = 0; i < 4; i++)
                {
                    adY[i + 1] = MatrixD.GetElement(i, 0) / adY[0] * 100;
                }
            }

            //理论生料配比 Z1=Y1/(100-L1)*100, Z2=Y2/(100-L2)*100, Z3=Y3/(100-L3)*100, Z4=Y4/(100-L4)*100, Z=Z1+Z2+Z3+Z4
            for (i = 0; i < 4; i++)
            {
                if (Math.Abs(100 - adRaw[9, i]) < Decimal.Parse("0.000001"))
                {
                    adZ[i + 1] = 0;
                    strErrorMessage = "计算理论生料配比数据异常，绝对值(100 - 原料烧失量) < 0.000001";
                    return adOutput;
                }
                else adZ[i + 1] = adY[i + 1] / (100 - adRaw[9, i]) * 100;
            }
            adZ[0] = adZ[1] + adZ[2] + adZ[3] + adZ[4];

            //理论干基配比:  z1=Z1/Z*100,  z2=z2/Z*100,  z3=Z3/Z*100,  z4=Z4/Z*100
            if (Math.Abs(adZ[0]) < Decimal.Parse("0.000001"))
            {
                for (i = 0; i < 4; i++)
                {
                    adz[i + 1] = 0;
                }
                strErrorMessage = "计算理论干基配比数据异常，理论生料配比之和 < 0.000001";
                return adOutput;
            }
            else
            {
                for (i = 0; i < 4; i++)
                {
                    adz[i + 1] = adZ[i + 1] / adZ[0] * 100;
                    adOutput[i + 4] = adz[i + 1];               //传出干基配比（4~7）
                }
            }

            //湿基配比 : X1=z1/(100-My1)*100, X2=z2/(100-My2)*100, X3=z3/(100-My3)*100, X4=z4/(100-My4)*100,  X=X1+X2+X3+X4
            for (i = 0; i < 4; i++)
            {
                if (Math.Abs(100 - adRaw[10, i]) < Decimal.Parse("0.000001"))
                {
                    adX[i + 1] = 0;
                    strErrorMessage = "计算湿基配比数据异常，绝对值(100 - 原料水分) < 0.000001";
                    return adOutput;
                }
                else adX[i + 1] = adz[i + 1] / (100 - adRaw[10, i]) * 100;
            }
            adX[0] = adX[1] + adX[2] + adX[3] + adX[4];

            if (Math.Abs(adX[0]) < Decimal.Parse("0.000001"))
            {
                //for (i = 0; i < 4; i++)
                //{
                //    adOutput[i] = 0;
                //}
                strErrorMessage = "计算湿基配比数据异常，湿基配比之和 < 0.000001";
                return adOutput;
            }
            else
            {
                //保证输出的总和为100%
                for (i = 0; i < 4; i++)
                {
                    adOutput[i] = adX[i + 1] / adX[0] * 100;
                }
            }

            //返回配比
            return adOutput;
        }
        #endregion

        #region 水泥配比计算四种原料
        /// <summary>
        /// 水泥配比计算方法四种原料
        /// </summary>
        /// <param name="adSMLC">4*4矩阵，(原料名称)熟料 石膏 矿渣 石灰石 (检测项目)SO3 MgO Loss 常数</param>
        /// <param name="adCement">4*1矩阵，水泥的检测项目 SO3 MgO Loss 常数</param>
        /// <param name="adW">3*4矩阵，前三个周期水泥原料配比</param>
        /// <param name="strErrorMessage">出错信息</param>
        /// <returns>1*4矩阵，水泥配比输出  (原料名称)熟料 石膏 矿渣 石灰石</returns>
        public decimal[] CementRatioCalculate4(decimal[,] adSMLC, decimal[] adCement, decimal[,] adW, out string strErrorMessage)
        {
            //输入变量
            decimal dW11, dW21, dW31, dW41;                      //上1个周期水泥原料配比
            decimal dW12, dW22, dW32, dW42;                      //上2个周期水泥原料配比
            decimal dW13, dW23, dW33, dW43;                      //上3个周期水泥原料配比

            //中间计算变量
            decimal dQ1, dQ2, dQ3, dQ4;                          //水泥理论原料配比值
            decimal dw1, dw2, dw3, dw4;                          //差值权重加合

            //输出变量 
            decimal[] adOutput = new decimal[4] { -1, -1, -1, -1 };//水泥计量称输出配比
            strErrorMessage = "";

            //给输入变量赋值，因为使用数组在公式计算中容易出错不好查，故使用赋值操作
            dW11 = adW[0, 0]; dW21 = adW[0, 1]; dW31 = adW[0, 2]; dW41 = adW[0, 3];
            dW12 = adW[1, 0]; dW22 = adW[1, 1]; dW32 = adW[1, 2]; dW42 = adW[1, 3];
            dW13 = adW[2, 0]; dW23 = adW[2, 1]; dW33 = adW[2, 2]; dW43 = adW[2, 3];

            //(1)  先获得矩阵系数
            //得出矩阵A   4*4矩阵，(原料名称)熟料 石膏 矿渣 石灰石 (检测项目)SO3 MgO Loss 常数
            //得出矩阵B   4*1矩阵，水泥的检测项目 SO3 MgO Loss 常数
            //(2)  计算4*4矩阵A的逆，即4*4矩阵C
            //(3)  4*4矩阵C乘以4*1矩阵B，得出4*1矩阵D
            //(4)  4*1矩阵D既是水泥理论原料配比值[Q1,Q2,Q3,Q4]
            Matrix MatrixA = new Matrix(adSMLC);
            Matrix MatrixB = new Matrix(4, 1, adCement);
            bool bResult = MatrixA.InvertGaussJordan();
            Matrix MatrixD = MatrixA.Multiply(MatrixB);
            dQ1 = MatrixD.GetElement(0, 0) * 100;
            dQ2 = MatrixD.GetElement(1, 0) * 100;
            dQ3 = MatrixD.GetElement(2, 0) * 100;
            dQ4 = MatrixD.GetElement(3, 0) * 100;

            //计算差值权重加合
            //(W11-Q1)*0.6 + (W12-Q1)*0.3 + (W13-Q1)*0.1
            //(W21-Q2)*0.6 + (W22-Q2)*0.3 + (W23-Q2)*0.1
            //(W31-Q3)*0.6 + (W32-Q3)*0.3 + (W33-Q3)*0.1
            //(W41-Q4)*0.6 + (W42-Q4)*0.3 + (W43-Q4)*0.1
            dw1 = (dW11 - dQ1) * Decimal.Parse("0.6") + (dW12 - dQ1) * Decimal.Parse("0.3") + (dW13 - dQ1) * Decimal.Parse("0.1");
            dw2 = (dW21 - dQ2) * Decimal.Parse("0.6") + (dW22 - dQ2) * Decimal.Parse("0.3") + (dW23 - dQ2) * Decimal.Parse("0.1");
            dw3 = (dW31 - dQ3) * Decimal.Parse("0.6") + (dW32 - dQ3) * Decimal.Parse("0.3") + (dW33 - dQ3) * Decimal.Parse("0.1");
            dw4 = (dW41 - dQ4) * Decimal.Parse("0.6") + (dW42 - dQ4) * Decimal.Parse("0.3") + (dW43 - dQ4) * Decimal.Parse("0.1");

            //水泥计量称输出配比
            adOutput[0] = dW11 + dw1; adOutput[1] = dW21 + dw2; adOutput[2] = dW31 + dw3; adOutput[3] = dW41 + dw4;

            //保证输出的总和为100%
            decimal dTotal = adOutput[0] + adOutput[1] + adOutput[2] + adOutput[3];
            if (Math.Abs(dTotal) < Decimal.Parse("0.000001"))
            {
                adOutput[0] = -1; adOutput[1] = -1; adOutput[2] = -1; adOutput[3] = -1;
                strErrorMessage = "计算输出的总和数据异常，输出的总和 < 0.000001";
                return adOutput;
            }
            else
            {
                adOutput[0] = adOutput[0] / dTotal * 100; adOutput[1] = adOutput[1] / dTotal * 100; adOutput[2] = adOutput[2] / dTotal * 100; adOutput[3] = adOutput[3] / dTotal * 100;
            }

            //返回水泥计量称输出配比
            return adOutput;
        }

        #endregion

        #region 水泥配比计算五种原料
        /// <summary>
        /// 水泥配比计算方法五种原料
        /// </summary>
        /// <param name="adSMLC">5*5矩阵，(原料名称)熟料 石膏 矿渣 石灰石 其他原料(检测项目)SO3 MgO Loss CaO 常数</param>
        /// <param name="adCement">5*1矩阵，水泥的检测项目 SO3 MgO Loss CaO 常数</param>
        /// <param name="adW">3*5矩阵，前三个周期水泥原料配比</param>
        /// <param name="strErrorMessage">出错信息</param>
        /// <returns>1*5矩阵，水泥配比输出  (原料名称)熟料 石膏 矿渣 石灰石 其他原料</returns>
        public decimal[] CementRatioCalculate5(decimal[,] adSMLC, decimal[] adCement, decimal[,] adW, out string strErrorMessage)
        {
            //输入变量
            decimal dW11, dW21, dW31, dW41, dW51;                //上1个周期水泥原料配比
            decimal dW12, dW22, dW32, dW42, dW52;                //上2个周期水泥原料配比
            decimal dW13, dW23, dW33, dW43, dW53;                //上3个周期水泥原料配比

            //中间计算变量
            decimal dQ1, dQ2, dQ3, dQ4, dQ5;                     //水泥理论原料配比值
            decimal dw1, dw2, dw3, dw4, dw5;                     //差值权重加合

            //输出变量 
            decimal[] adOutput = new decimal[5] { -1, -1, -1, -1, -1 };//水泥计量称输出配比
            strErrorMessage = "";

            //给输入变量赋值，因为使用数组在公式计算中容易出错不好查，故使用赋值操作
            dW11 = adW[0, 0]; dW21 = adW[0, 1]; dW31 = adW[0, 2]; dW41 = adW[0, 3]; dW51 = adW[0, 4];
            dW12 = adW[1, 0]; dW22 = adW[1, 1]; dW32 = adW[1, 2]; dW42 = adW[1, 3]; dW52 = adW[1, 4];
            dW13 = adW[2, 0]; dW23 = adW[2, 1]; dW33 = adW[2, 2]; dW43 = adW[2, 3]; dW53 = adW[2, 4];

            //(1)  先获得矩阵系数
            //得出矩阵A   5*5矩阵，(原料名称)熟料 石膏 矿渣 石灰石 (检测项目)SO3 MgO Loss  CaO 常数
            //得出矩阵B   5*1矩阵，水泥的检测项目 SO3 MgO Loss  CaO 常数
            //(2)  计算5*5矩阵A的逆，即5*5矩阵C
            //(3)  5*5矩阵C乘以5*1矩阵B，得出5*1矩阵D
            //(4)  5*1矩阵D既是水泥理论原料配比值[Q1,Q2,Q3,Q4,Q5]
            Matrix MatrixA = new Matrix(adSMLC);
            Matrix MatrixB = new Matrix(5, 1, adCement);
            bool bResult = MatrixA.InvertGaussJordan();
            Matrix MatrixD = MatrixA.Multiply(MatrixB);
            dQ1 = MatrixD.GetElement(0, 0) * 100;
            dQ2 = MatrixD.GetElement(1, 0) * 100;
            dQ3 = MatrixD.GetElement(2, 0) * 100;
            dQ4 = MatrixD.GetElement(3, 0) * 100;
            dQ5 = MatrixD.GetElement(4, 0) * 100;

            //计算差值权重加合
            //(W11-Q1)*0.6 + (W12-Q1)*0.3 + (W13-Q1)*0.1
            //(W21-Q2)*0.6 + (W22-Q2)*0.3 + (W23-Q2)*0.1
            //(W31-Q3)*0.6 + (W32-Q3)*0.3 + (W33-Q3)*0.1
            //(W41-Q4)*0.6 + (W42-Q4)*0.3 + (W43-Q4)*0.1
            //(W51-Q5)*0.6 + (W52-Q5)*0.3 + (W53-Q5)*0.1
            dw1 = (dW11 - dQ1) * decimal.Parse("0.6") + (dW12 - dQ1) * decimal.Parse("0.3") + (dW13 - dQ1) * decimal.Parse("0.1");
            dw2 = (dW21 - dQ2) * decimal.Parse("0.6") + (dW22 - dQ2) * decimal.Parse("0.3") + (dW23 - dQ2) * decimal.Parse("0.1");
            dw3 = (dW31 - dQ3) * decimal.Parse("0.6") + (dW32 - dQ3) * decimal.Parse("0.3") + (dW33 - dQ3) * decimal.Parse("0.1");
            dw4 = (dW41 - dQ4) * decimal.Parse("0.6") + (dW42 - dQ4) * decimal.Parse("0.3") + (dW43 - dQ4) * decimal.Parse("0.1");
            dw5 = (dW51 - dQ5) * decimal.Parse("0.6") + (dW52 - dQ5) * decimal.Parse("0.3") + (dW53 - dQ5) * decimal.Parse("0.1");

            //水泥计量称输出配比
            adOutput[0] = dW11 + dw1; adOutput[1] = dW21 + dw2; adOutput[2] = dW31 + dw3; adOutput[3] = dW41 + dw4; adOutput[4] = dW51 + dw5;

            //保证输出的总和为100%
            decimal dTotal = adOutput[0] + adOutput[1] + adOutput[2] + adOutput[3] + adOutput[4];
            if (Math.Abs(dTotal) < decimal.Parse("0.000001"))
            {
                adOutput[0] = -1; adOutput[1] = -1; adOutput[2] = -1; adOutput[3] = -1; adOutput[4] = -1;
                strErrorMessage = "计算输出的总和数据异常，输出的总和 < 0.000001";
                return adOutput;
            }
            else
            {
                adOutput[0] = adOutput[0] / dTotal * 100; adOutput[1] = adOutput[1] / dTotal * 100; adOutput[2] = adOutput[2] / dTotal * 100; adOutput[3] = adOutput[3] / dTotal * 100; adOutput[4] = adOutput[4] / dTotal * 100;
            }

            //返回水泥计量称输出配比
            return adOutput;
        }
        #endregion


        #region  生料配比输出计算
        /// <summary>
        /// 配比输出计算方法
        /// </summary>
        /// <param name="adSAFC">4*4矩阵，(原料名称)石灰石 砂岩 铁矿石 铝矾土 (化学成分名称)SiO2 Al2O3 Fe2O3 CaO</param>
        /// <param name="adW">1*4矩阵，上周期原料输出配比</param>
        /// <param name="adKHSMIM">1*3矩阵，当前目标率值 KH SM IM</param>
        /// <param name="adKhSmIm">3*3矩阵，荧光分析上1、2、3个周期率值 KH SM IM</param>
        /// <param name="adRight">1*3矩阵，权重值</param>
        /// <param name="strErrorMessage">出错信息</param>
        /// <returns>1*4矩阵，配比输出  (原料名称)石灰石 砂岩 铁矿石 铝矾土   加3个：下周期KH、SM、IM （最终为1*7矩阵）</returns>
        public decimal[] RatioOutputCalculate(decimal[,] adSAFC, decimal[] adW, decimal[] adKHSMIM, decimal[,] adKhSmIm,decimal[] adRight, out string strErrorMessage)
        {
            //输入变量
            //  石灰石 砂岩 铁矿石 铝矾土  (原料名称)
            decimal dS1, dS2, dS3, dS4;                          //SiO2   (化学成分名称)
            decimal dA1, dA2, dA3, dA4;                          //Al2O3  (化学成分名称)
            decimal dF1, dF2, dF3, dF4;                          //Fe2O3  (化学成分名称)
            decimal dC1, dC2, dC3, dC4;                          //CaO    (化学成分名称)
            decimal dW1, dW2, dW3, dW4;                          //上周期原料输出配比
            decimal dKH, dSM, dIM;                               //当前目标率值
            decimal dkh1, dsm1, dim1;                            //荧光分析上1个周期率值
            decimal dkh2, dsm2, dim2;                            //荧光分析上2个周期率值
            decimal dkh3, dsm3, dim3;                            //荧光分析上3个周期率值

            //中间计算变量
            decimal dQ1 = adRight[0], dQ2 = adRight[1], dQ3 = adRight[2];        //率值目标差值权重系数
            decimal dkh, dsm, dim;                                               //下周期控制目标值
            decimal dKH1, dSM1, dIM1;                                            //目标率值系数
            decimal dKH2, dSM2, dIM2;                                            //输出率值系数
            decimal dw1, dw2, dw3, dw4;                                          //目标原料配比值（矩阵运算）
            decimal dw_1, dw_2, dw_3, dw_4;                                      //下周期配比（矩阵运算））

            //输出变量 
            decimal[] adOutput = new decimal[7] { -1, -1, -1, -1, -1, -1, -1 };//配比输出
            strErrorMessage = "";

            //给输入变量赋值，因为使用数组在公式计算中容易出错不好查，故使用赋值操作
            dS1 = adSAFC[0, 0]; dS2 = adSAFC[0, 1]; dS3 = adSAFC[0, 2]; dS4 = adSAFC[0, 3];
            dA1 = adSAFC[1, 0]; dA2 = adSAFC[1, 1]; dA3 = adSAFC[1, 2]; dA4 = adSAFC[1, 3];
            dF1 = adSAFC[2, 0]; dF2 = adSAFC[2, 1]; dF3 = adSAFC[2, 2]; dF4 = adSAFC[2, 3];
            dC1 = adSAFC[3, 0]; dC2 = adSAFC[3, 1]; dC3 = adSAFC[3, 2]; dC4 = adSAFC[3, 3];
            dW1 = adW[0]; dW2 = adW[1]; dW3 = adW[2]; dW4 = adW[3];
            dKH = adKHSMIM[0]; dSM = adKHSMIM[1]; dIM = adKHSMIM[2];
            dkh1 = adKhSmIm[0, 0]; dsm1 = adKhSmIm[0, 1]; dim1 = adKhSmIm[0, 2];
            dkh2 = adKhSmIm[1, 0]; dsm2 = adKhSmIm[1, 1]; dim2 = adKhSmIm[1, 2];
            dkh3 = adKhSmIm[2, 0]; dsm3 = adKhSmIm[2, 1]; dim3 = adKhSmIm[2, 2];

            //计算下周期控制目标值  (实际计算中将公式中变量前加d前缀表示浮点数即可)
            //Q1=0.6，Q2=0.3，Q3=0.1-------------合计=1
            //kh=((KH-kh1)*Q1+(KH-kh2)*Q2+(KH-kh3)*Q3)+KH
            //sm=((SM-sm1)*Q1+(SM-sm2)*Q2+(SM-sm3)*Q3)+SM
            //im=((IM-im1)*Q1+(IM-im2)*Q2+(IM-im3)*Q3)+IM
            dkh = (( dKH - dkh1 ) * dQ1 + ( dKH - dkh2 ) * dQ2 + ( dKH - dkh3 ) * dQ3 ) + dKH;
            dsm = (( dSM - dsm1 ) * dQ1 + ( dSM - dsm2 ) * dQ2 + ( dSM - dsm3 ) * dQ3 ) + dSM;
            dim = (( dIM - dim1 ) * dQ1 + ( dIM - dim2 ) * dQ2 + ( dIM - dim3 ) * dQ3 ) + dIM;
            adOutput[4] = dkh;
            adOutput[5] = dsm;
            adOutput[6] = dim;

            //计算目标率值系数  (实际计算中将公式中变量前加d前缀表示浮点数即可)
            //KH1=1.65*IM+2.8*KH*SM*(IM+1)+0.35
            //SM1=SM*(IM+1)
            //IM1=IM
            dKH1 = decimal.Parse("1.65") * dIM + decimal.Parse("2.8") * dKH * dSM * ( dIM + 1 ) + decimal.Parse("0.35");
            dSM1 = dSM * ( dIM + 1 );
            dIM1 = dIM;

            //计算输出率值系数  (实际计算中将公式中变量前加d前缀表示浮点数即可)
            //KH2=1.65*im+2.8*kh*sm*(im+1)+0.35
            //SM2=sm*(im+1)
            //IM2=im
            dKH2 = decimal.Parse("1.65") * dim + decimal.Parse("2.8") * dkh * dsm * ( dim + 1 ) + decimal.Parse("0.35");
            dSM2 = dsm * ( dim + 1);
            dIM2 = dim;

            //原料化学成分参数命名表
            //    原料名称  石灰石	砂岩	铁矿石	铝矾土
            //化学成分名称
            //        SiO2    S1     S2       S3      S4
            //       Al2O3    A1     A2       A3      A4
            //       Fe2O3    F1     F2       F3      F4
            //         CaO    C1     C2       C3      C4

            //(1)  先计算出目标值矩阵系数
            //得出矩阵A（S1至C4的4*4矩阵）和B（K1至K4的4*1矩阵）
            //	    1	        2	        3	        4	    K
            //S	C1-KH1*F1	C2-KH1*F2	C3-KH1*F3	C4-KH1*F4	1
            //A	A1-SM1*F1	A2-SM1*F2	A3-SM1*F3	A4-SM1*F4	1
            //F	S1-IM1*F1	S2-IM1*F2	S3-IM1*F3	S4-IM1*F4	1
            //C	    1	        1	        1	        1	    1
            //(2)  计算4*4矩阵A的逆，即4*4矩阵C
            //(3)  4*4矩阵C乘以4*1矩阵B，得出4*1矩阵D
            //(4)  4*1矩阵D既是目标原料配比值[w1,w2,w3,w4]
            decimal[,] adMatrixA = new decimal[4, 4];
            adMatrixA[0, 0] = dC1 - dKH1 * dF1; adMatrixA[0, 1] = dC2 - dKH1 * dF2; adMatrixA[0, 2] = dC3 - dKH1 * dF3; adMatrixA[0, 3] = dC4 - dKH1 * dF4;
            //adMatrixA[1, 0] = dA1 - dSM1 * dF1; adMatrixA[1, 1] = dA2 - dSM1 * dF2; adMatrixA[1, 2] = dA3 - dSM1 * dF3; adMatrixA[1, 3] = dA4 - dSM1 * dF4;
            //adMatrixA[2, 0] = dS1 - dIM1 * dF1; adMatrixA[2, 1] = dS2 - dIM1 * dF2; adMatrixA[2, 2] = dS3 - dIM1 * dF3; adMatrixA[2, 3] = dS4 - dIM1 * dF4;
            adMatrixA[1, 0] = dS1 - dSM1 * dF1; adMatrixA[1, 1] = dS2 - dSM1 * dF2; adMatrixA[1, 2] = dS3 - dSM1 * dF3; adMatrixA[1, 3] = dS4 - dSM1 * dF4;
            adMatrixA[2, 0] = dA1 - dIM1 * dF1; adMatrixA[2, 1] = dA2 - dIM1 * dF2; adMatrixA[2, 2] = dA3 - dIM1 * dF3; adMatrixA[2, 3] = dA4 - dIM1 * dF4;
            adMatrixA[3, 0] = 1; adMatrixA[3, 1] = 1; adMatrixA[3, 2] = 1; adMatrixA[3, 3] = 1;

            Matrix MatrixA = new Matrix(adMatrixA);
            Matrix MatrixB = new Matrix(4, 1, new decimal[] { 1, 1, 1, 1 });
            bool bResult = MatrixA.InvertGaussJordan();
            Matrix MatrixD = MatrixA.Multiply(MatrixB);
            dw1 = MatrixD.GetElement(0, 0);
            dw2 = MatrixD.GetElement(1, 0);
            dw3 = MatrixD.GetElement(2, 0);
            dw4 = MatrixD.GetElement(3, 0);

            //(1)  先计算出下周期矩阵系数
            //得出矩阵A（S1至C4的4*4矩阵）和B（K1至K4的4*1矩阵）
            //	    1	        2	        3	        4	    K
            //S	C1-KH2*F1	C2-KH2*F2	C3-KH2*F3	C4-KH2*F4	1
            //A	A1-SM2*F1	A2-SM2*F2	A3-SM2*F3	A4-SM2*F4	1
            //F	S1-IM2*F1	S2-IM2*F2	S3-IM2*F3	S4-IM2*F4	1
            //C	    1	        1	        1	        1	    1
            //(2)  计算4*4矩阵A的逆，即4*4矩阵C
            //(3)  4*4矩阵C乘以4*1矩阵B，得出4*1矩阵D
            //(4)  4*1矩阵D既是下周期配比[w_1,w_2,w_3,w_4]
            adMatrixA[0, 0] = dC1 - dKH2 * dF1;     adMatrixA[0, 1] = dC2 - dKH2 * dF2;     adMatrixA[0, 2] = dC3 - dKH2 * dF3;     adMatrixA[0, 3] = dC4 - dKH2 * dF4;
            adMatrixA[1, 0] = dS1 - dSM2 * dF1;     adMatrixA[1, 1] = dS2 - dSM2 * dF2;     adMatrixA[1, 2] = dS3 - dSM2 * dF3;     adMatrixA[1, 3] = dS4 - dSM2 * dF4;
            adMatrixA[2, 0] = dA1 - dIM2 * dF1;     adMatrixA[2, 1] = dA2 - dIM2 * dF2;     adMatrixA[2, 2] = dA3 - dIM2 * dF3;     adMatrixA[2, 3] = dA4 - dIM2 * dF4;
            //adMatrixA[1, 0] = dA1 - dSM2 * dF1; adMatrixA[1, 1] = dA2 - dSM2 * dF2; adMatrixA[1, 2] = dA3 - dSM2 * dF3; adMatrixA[1, 3] = dA4 - dSM2 * dF4;
            //adMatrixA[2, 0] = dS1 - dIM2 * dF1; adMatrixA[2, 1] = dS2 - dIM2 * dF2; adMatrixA[2, 2] = dS3 - dIM2 * dF3; adMatrixA[2, 3] = dS4 - dIM2 * dF4;
            adMatrixA[3, 0] = 1;                  adMatrixA[3, 1] = 1;                  adMatrixA[3, 2] = 1;                  adMatrixA[3, 3] = 1;

            MatrixA = new Matrix(adMatrixA);
            MatrixB = new Matrix(4, 1, new decimal[] { 1, 1, 1, 1 });
            bResult = MatrixA.InvertGaussJordan();
            MatrixD = MatrixA.Multiply(MatrixB);
            dw_1 = MatrixD.GetElement(0, 0);    dw_2 = MatrixD.GetElement(1, 0);    dw_3 = MatrixD.GetElement(2, 0);    dw_4 = MatrixD.GetElement(3, 0);

            //计算配比差值
            //W_1=w_1- w1	W_2=w_2- w2	W_3=w_3-w3	W_4=w_4-w4
            //执行配比
            //W1+W_1*100	W2+W_2*100	W3+W_3*100	W4+W_4*100
            adOutput[0] = dW1 + (dw_1 - dw1) * 100;     adOutput[1] = dW2 + (dw_2 - dw2) * 100;     adOutput[2] = dW3 + (dw_3 - dw3) * 100;     adOutput[3] = dW4 + (dw_4 - dw4) * 100;

            //保证输出的总和为100%
            decimal dTotal = adOutput[0] + adOutput[1] + adOutput[2] + adOutput[3];
            if (Math.Abs(dTotal) < decimal.Parse("0.000001"))
            {
                adOutput[0] = -1; adOutput[1] = -1; adOutput[2] = -1; adOutput[3] = -1;
                strErrorMessage = "计算输出的总和数据异常，输出的总和 < 0.000001";
                return adOutput;
            }
            else
            {
                adOutput[0] = adOutput[0] / dTotal * 100; adOutput[1] = adOutput[1] / dTotal * 100; adOutput[2] = adOutput[2] / dTotal * 100; adOutput[3] = adOutput[3] / dTotal * 100;
            }

            //返回配比
            return adOutput;
        }
        #endregion

        #endregion

        //基本运算公式包括   1.饱和比：KH- KH  2.硅酸率：SM   3.铝氧率：IM   4.硅酸三钙：C3S    5.硅酸二钙：C2S    6.铝酸三钙：C3A  7.铁铝酸四钙：C4AF  8.碱含量：R2O   9.硫碱比：S/R    10.液相量：Lq
        //基本运算公式包括   11.煤灰参入率     12.煤耗        13.结皮指数    14.煅烧系数（B.I） 15.煅烧系数（B.F） 16.煅烧温度      17.合格率           18.标准偏差：S  19.变异系数：Cv  20.灼烧基生料

        #region 基本运算公式

        /// <summary>
        /// 用公式计算KH-
        /// </summary>
        /// <param name="dCaO">氧化钙含量</param>
        /// <param name="dF_CaO">游离氧化钙含量</param>
        /// <param name="dAl2O3">氧化铝含量</param>
        /// <param name="dFe2O3">氧化铁含量</param>
        /// <param name="dSO3">三氧化硫含量</param>
        /// <param name="dSiO2">氧化硅含量</param>
        /// <returns>KH-</returns>
        public decimal CalculateKH_(decimal dCaO, decimal dF_CaO, decimal dAl2O3, decimal dFe2O3, decimal dSO3, decimal dSiO2)
        {
            if (Math.Abs(dSiO2) < decimal.Parse("0.000001")) return -1;

            //输出变量KH- 
            decimal dKH_ = ((dCaO - dF_CaO) - decimal.Parse("1.65") * dAl2O3 - decimal.Parse("0.35") * dFe2O3 - decimal.Parse("0.7") * dSO3) / (decimal.Parse("2.8") * dSiO2);
            return dKH_;
        }

        /// <summary>
        /// 用公式计算KH
        /// </summary>
        /// <param name="dCaO">氧化钙含量</param>
        /// <param name="dAl2O3">氧化铝含量</param>
        /// <param name="dFe2O3">氧化铁含量</param>
        /// <param name="dSO3">三氧化硫含量</param>
        /// <param name="dSiO2">氧化硅含量</param>
        /// <returns>KH</returns>
        public decimal CalculateKH(decimal dCaO, decimal dAl2O3, decimal dFe2O3, decimal dSO3, decimal dSiO2)
        {
            if (Math.Abs(dSiO2) <decimal.Parse("0.000001")) return -1;

            //输出变量 KH
            decimal dKH = (dCaO - decimal.Parse("1.65") * dAl2O3 - decimal.Parse("0.35") * dFe2O3 - decimal.Parse("0.7") * dSO3) / (decimal.Parse("2.8") * dSiO2);
            return dKH;
        }

        /// <summary>
        /// 用公式计算SM
        /// </summary>
        /// <param name="dSiO2">氧化硅含量</param>
        /// <param name="dAl2O3">氧化铝含量</param>
        /// <param name="dFe2O3">氧化铁含量</param>
        /// <returns>SM</returns>
        public decimal CalculateSM(decimal dSiO2, decimal dAl2O3, decimal dFe2O3)
        {
            if (Math.Abs(dAl2O3 + dFe2O3) < decimal.Parse("0.000001")) return -1;

            //输出变量 SM
            decimal dSM = dSiO2 / (dAl2O3 + dFe2O3);
            return dSM;
        }

        /// <summary>
        /// 用公式计算IM
        /// </summary>
        /// <param name="dAl2O3">氧化铝含量</param>
        /// <param name="dFe2O3">氧化铁含量</param>
        /// <returns>IM</returns>
        public decimal CalculateIM(decimal dAl2O3, decimal dFe2O3)
        {
            if (Math.Abs(dFe2O3) < decimal.Parse("0.000001")) return -1;

            //输出变量 IM
            decimal dIM = dAl2O3 / dFe2O3;
            return dIM;
        }

        /// <summary>
        /// 用公式计算硅酸三钙
        /// </summary>
        /// <param name="dCaO">氧化钙含量</param>
        /// <param name="dF_CaO">游离氧化钙含量</param>
        /// <param name="dSiO2">氧化硅含量</param>
        /// <param name="dAl2O3">氧化铝含量</param>
        /// <param name="dFe2O3">氧化铁含量</param>
        /// <param name="dSO3">三氧化硫含量</param>
        /// <returns>C3S</returns>
        public decimal CalculateC3S(decimal dCaO, decimal dF_CaO, decimal dSiO2, decimal dAl2O3, decimal dFe2O3, decimal dSO3)
        {
            //输出变量C3S 
            decimal dC3S = decimal.Parse("4.07") * (dCaO - dF_CaO) - decimal.Parse("7.6") * dSiO2 - decimal.Parse("6.72") * dAl2O3 - decimal.Parse("1.43") * dFe2O3 - decimal.Parse("2.85") * dSO3;
            return dC3S;
        }

        /// <summary>
        /// 用公式计算硅酸二钙
        /// </summary>
        /// <param name="dSiO2">氧化硅含量</param>
        /// <param name="dAl2O3">氧化铝含量</param>
        /// <param name="dFe2O3">氧化铁含量</param>
        /// <param name="dSO3">三氧化硫含量</param>
        /// <param name="dCaO">氧化钙含量</param>
        /// <returns>C2S</returns>
        public decimal CalculateC2S(decimal dSiO2, decimal dAl2O3, decimal dFe2O3, decimal dSO3, decimal dCaO)
        {
            //输出变量C2S 
            decimal dC2S = decimal.Parse("8.6") * dSiO2 + decimal.Parse("5.07") * dAl2O3 + decimal.Parse("1.07") * dFe2O3 + decimal.Parse("2.15") * dSO3 - decimal.Parse("3.07") * dCaO;
            return dC2S;
        }

        /// <summary>
        /// 通过C3S和SiO2计算C2S
        /// </summary>
        /// <param name="dSiO2"></param>
        /// <param name="dC3S"></param>
        /// <returns></returns>
        public decimal CalculateC2SByC3S(decimal dSiO2, decimal dC3S)
        {
            //输出变量C2S 
            decimal dC2S = decimal.Parse("2.87") * dSiO2 - decimal.Parse("0.75") * dC3S;
            return dC2S;
        }

        /// <summary>
        /// 用公式计算铝酸三钙
        /// </summary>
        /// <param name="dAl2O3">氧化铝含量</param>
        /// <param name="dFe2O3">氧化铁含量</param>
        /// <returns>C3A</returns>
        public decimal CalculateC3A(decimal dAl2O3, decimal dFe2O3)
        {
            //输出变量C3A 
            decimal dC3A = decimal.Parse("2.65") * dAl2O3 - decimal.Parse("1.69") * dFe2O3;
            return dC3A;
        }

        /// <summary>
        /// 用公式计算铁铝酸四钙
        /// </summary>
        /// <param name="dFe2O3">氧化铁含量</param>
        /// <returns>C4AF</returns>
        public decimal CalculateC4AF(decimal dFe2O3)
        {
            //输出变量C4AF 
            decimal dC4AF = decimal.Parse("3.04") * dFe2O3;
            return dC4AF;
        }

        /// <summary>
        /// 用公式计算碱含量
        /// </summary>
        /// <param name="dK2O">氧化钾含量</param>
        /// <param name="dNa2O">氧化钠含量</param>
        /// <returns>R2O</returns>
        public decimal CalculateR2O(decimal dK2O, decimal dNa2O)
        {
            //输出变量R2O 
            decimal dR2O = decimal.Parse("0.658") * dK2O + dNa2O;
            return dR2O;
        }

        /// <summary>
        /// 用公式计算硫碱比
        /// </summary>
        /// <param name="dSO3">三氧化硫含量</param>
        /// <param name="dR2O">碱含量</param>
        /// <returns>S_R</returns>
        public decimal CalculateS_R(decimal dSO3, decimal dR2O)
        {
            if (Math.Abs(dR2O) < decimal.Parse("0.000001")) return -1;

            //输出变量S_R 
            decimal dS_R = dSO3 / dR2O;
            return dS_R;
        }

        /// <summary>
        /// 用公式计算液相量
        /// </summary>
        /// <param name="dAl2O3">氧化铝含量</param>
        /// <param name="dFe2O3">氧化铁含量</param>
        /// <param name="dMgO">氧化镁含量</param>
        /// <param name="dR2O">碱含量</param>
        /// <returns>Lq</returns>
        public decimal CalculateLq(decimal dAl2O3, decimal dFe2O3, decimal dMgO, decimal dR2O)
        {
            //输出变量Lq 
            decimal dLq = 3 * dAl2O3 + decimal.Parse("2.25") * dFe2O3 + dMgO + dR2O;
            return dLq;
        }

        /// <summary>
        /// 用公式计算煤灰参入率
        /// </summary>
        /// <param name="dCoalAsh">煤灰分</param>
        /// <param name="dHeatConsumption">热耗</param>
        /// <param name="dCalorificValue">热值</param>
        /// <returns>AshRate</returns>
        public decimal CalculateAshRate(decimal dCoalAsh, decimal dHeatConsumption, decimal dCalorificValue)
        {
            if (Math.Abs(dCalorificValue) < decimal.Parse("0.000001")) return -1;

            //输出变量AshRate
            decimal dAshRate = dCoalAsh * dHeatConsumption / dCalorificValue;
            return dAshRate;
        }

        /// <summary>
        /// 用公式计算煤耗
        /// </summary>
        /// <param name="dHeatConsumption">热耗</param>
        /// <param name="dCalorificValue">热值</param>
        /// <returns>CoalConsumption</returns>
        public decimal CalculateCoalConsumption(decimal dHeatConsumption, decimal dCalorificValue)
        {
            if (Math.Abs(dCalorificValue) < decimal.Parse("0.000001")) return -1;

            //输出变量CoalConsumption
            decimal dCoalConsumption = dHeatConsumption / dCalorificValue;
            return dCoalConsumption;
        }

        /// <summary>
        /// 用公式计算结皮指数
        /// </summary>
        /// <param name="dC3A">铝酸三钙</param>
        /// <param name="dC4AF">铁铝酸四钙</param>
        /// <param name="dC2S">硅酸二钙</param>
        /// <param name="dFe2O3">氧化铁含量</param>
        /// <returns>CrustIndex</returns>
        public decimal CalculateCrustIndex(decimal dC3A, decimal dC4AF, decimal dC2S, decimal dFe2O3)
        {
            //输出变量CrustIndex
            decimal dCrustIndex = dC3A + dC4AF + decimal.Parse("0.2") * dC2S + 2 * dFe2O3;
            return dCrustIndex;
        }

        /// <summary>
        /// 用公式计算煅烧系数（B.I）
        /// </summary>
        /// <param name="dC3S">硅酸三钙</param>
        /// <param name="dC3A">铝酸三钙</param>
        /// <param name="dC4AF">铁铝酸四钙</param>
        /// <param name="dMgO">氧化镁含量</param>
        /// <param name="dR2O">碱含量</param>
        /// <returns>B_I</returns>
        public decimal CalculateB_I(decimal dC3S, decimal dC3A, decimal dC4AF, decimal dMgO, decimal dR2O)
        {
            if (Math.Abs(dC3A + dC4AF + dMgO + dR2O) < decimal.Parse("0.000001")) return -1;

            //输出变量B_I
            decimal dB_I = dC3S / (dC3A + dC4AF + dMgO + dR2O);
            return dB_I;
        }

        /// <summary>
        /// 用公式计算煅烧系数（B.F）（熟料化学成分）
        /// </summary>
        /// <param name="dCaO">氧化钙含量</param>
        /// <param name="dF_CaO">游离氧化钙含量</param>
        /// <param name="dSiO2">氧化硅含量</param>
        /// <param name="dAl2O3">氧化铝含量</param>
        /// <param name="dFe2O3">氧化铁含量</param>
        /// <param name="dSM">硅酸率</param>
        /// <param name="dMgO">氧化镁含量</param>
        /// <param name="dR2O">碱含量</param>
        /// <returns>B_F</returns>
        public decimal CalculateB_F(decimal dCaO, decimal dF_CaO, decimal dSiO2, decimal dAl2O3, decimal dFe2O3, decimal dSM, decimal dMgO, decimal dR2O)
        {
            if (Math.Abs(decimal.Parse("2.8") * dSiO2 + decimal.Parse("1.18") * dAl2O3 + decimal.Parse("0.65") * dFe2O3) < decimal.Parse("0.000001")) return -1;

            //输出变量B_F 
            decimal dB_F = (dCaO - dF_CaO) / (decimal.Parse("2.8") * dSiO2 + decimal.Parse("1.18") * dAl2O3 + decimal.Parse("0.65") * dFe2O3) * 100 + 10 * dSM - 3 * (dMgO + dR2O);
            return dB_F;
        }

        /// <summary>
        /// 用公式计算煅烧温度d
        /// </summary>
        /// <param name="dC3S">硅酸三钙</param>
        /// <param name="dC3A">铝酸三钙</param>
        /// <param name="dC4AF">铁铝酸四钙</param>
        /// <returns>CalcinationTemperature</returns>
        public decimal CalculateCalcinationTemperature(decimal dC3S, decimal dC3A, decimal dC4AF)
        {
            //输出变量CalcinationTemperature 
            decimal dCalcinationTemperature = 1300 + decimal.Parse("4.51") * dC3S - decimal.Parse("3.74") * dC3A - decimal.Parse("12.64") * dC4AF;
            return dCalcinationTemperature;
        }

        /// <summary>
        /// 用公式计算合格率
        /// </summary>
        /// <param name="dQualifiedData">合格数据个数</param>
        /// <param name="dTotalData">数据总个数</param>
        /// <returns>PassRate</returns>
        public decimal CalculatePassRate(decimal dQualifiedData, decimal dTotalData)
        {
            if (Math.Abs(dTotalData) < decimal.Parse("0.000001")) return -1;

            //输出变量PassRate 
            decimal dPassRate = dQualifiedData / dTotalData * 100;
            return dPassRate;
        }

        /// <summary>
        /// 用公式计算标准偏差
        /// </summary>
        /// <param name="adData">数据子样数组</param>
        /// <param name="nLen">数据总个数</param>
        /// <returns>S</returns>
        public decimal CalculateS(decimal[] adData, int nLen)
        {
            if (nLen == 0 || nLen == 1) return -1;

            //输出变量S 
            decimal dS;
            decimal dTotle = 0, dMean, dTmp;
            int i;
            for (i = 0; i < nLen; i++)
            {
                dTotle = dTotle + adData[i];
            }
            dMean = dTotle / nLen;//计算平均值
            dTotle = 0;

            for (i = 0; i < nLen; i++)
            {
                dTmp = (adData[i] - dMean);
                dTotle = dTotle + dTmp * dTmp;
            }
            dTmp = decimal.Parse(Math.Sqrt(double.Parse(dTotle.ToString())).ToString());
            dS = dTmp / (nLen - 1);

            return dS;
        }

        /// <summary>
        /// 用公式计算变异系数
        /// </summary>
        /// <param name="dStandardDeviation">标准偏差</param>
        /// <param name="dMean">平均值</param>
        /// <returns>Cv</returns>
        public decimal CalculateCv(decimal dStandardDeviation, decimal dMean)
        {
            if (Math.Abs(dMean) < decimal.Parse("0.000001")) return -1;

            //输出变量Cv 
            decimal dCv = dStandardDeviation / dMean;
            return dCv;
        }

        /// <summary>
        /// 用公式计算灼烧基生料
        /// </summary>
        /// <param name="dRawIngredients">生料成分</param>
        /// <param name="dLoss">烧失量</param>
        /// <returns>BBRM</returns>
        public decimal CalculateBurningBasedRawMaterial(decimal dRawIngredients, decimal dLoss)
        {
            if (Math.Abs(1 - dLoss) < decimal.Parse("0.000001")) return -1;

            //输出变量BBRM 
            decimal dBBRM = dRawIngredients / (1 - dLoss) * 100;
            return dBBRM;
        }

        /// <summary>
        /// 计算煤的固定碳
        /// </summary>
        /// <param name="dbInsideWater">内水</param>
        /// <param name="dbV">挥发分</param>
        /// <param name="dbA">灰分</param>
        /// <returns></returns>
        public decimal CalculateCarbon(decimal dbInsideWater,decimal dbV,decimal dbA)
        {
            decimal dbCarbon = 0;
            dbCarbon = 100 - dbInsideWater - dbV - dbA;
            return dbCarbon;
        }

        /// <summary>
        /// 计算原煤低位热值
        /// </summary>
        /// <param name="dbOutsideWater">外水</param>
        /// <param name="dbInsideWater">内水</param>
        /// <param name="dbHighHeat">高位热值</param>
        /// <returns></returns>
        public decimal CalculateLowHeat(decimal dbOutsideWater,decimal dbInsideWater,decimal dbHighHeat)
        {
            decimal dbLowHeat = 0;
            dbLowHeat = (100 - dbOutsideWater) * dbHighHeat / (100 - dbInsideWater) + (6 * dbInsideWater) - (6 * dbOutsideWater);
            return dbLowHeat;
        }
        /// <summary>
        /// 计算原煤收到基低位发热量[Qnet.ar = (Qnet.ad + 23Mad)*(100-Mt)/(100-Mad)-23Mt ]（经验公式）
        /// </summary>
        /// <param name="dcQnetad">干燥基低位发热量</param>
        /// <param name="dcMar">收到基水份=外水</param>
        /// <param name="dcMad">干燥基水份=内水</param>
        /// <returns></returns>
        public decimal CalculateQnetar(decimal dcQnetad, decimal dcMar,decimal dcMad)
        {
            decimal dcQnetar = 0;
            dcQnetar = dcQnetad * (100 - dcMar) / (100 - dcMad) - decimal.Parse("5.4") * (dcMar - dcMad * (100 - dcMar) / (100 - dcMad));
            return dcQnetar;
        }

        /// <summary>
        /// 计算原煤干燥基低位热值(分析基低位发热量)（Qnet,ad = 35859.9—73.7Vad—395.7Aad—702.0Mad + 173.6CRC ）（经验公式）
        /// </summary>
        /// <param name="dbK">K值</param>
        /// <param name="dbInsideWater">内水</param>
        /// <param name="dbA">灰分</param>
        /// <param name="dbV">挥发分</param>
        /// <returns></returns>
        public decimal CalculateHighHeat(decimal dbK, decimal dbInsideWater, decimal dbA,decimal dbV)
        {
            decimal dbHighHeat = 0;
            dbHighHeat = 100 * dbK - (dbInsideWater + dbA) * (dbK + 6) - 3 * dbV;
            return dbHighHeat;
        }
        /// <summary>
        /// 计算Vdaf（查找K值使用值）
        /// </summary>
        /// <param name="dcVad">挥发份</param>
        /// <param name="dcMad">干燥基水份=内水</param>
        /// <param name="dcAad">灰份</param>
        /// <returns></returns>
        public decimal CalculateVdaf(decimal dcVad,decimal dcMad,decimal dcAad)
        {
            decimal dcVdaf = 0;
            dcVdaf = 100 * dcVad / (100 - dcMad - dcAad);
            return dcVdaf;
        }

        /// <summary>
        /// 保留小数位数函数
        /// </summary>
        /// <param name="strOriginalityData">数值型字符串</param>
        /// <param name="digits">保留小数位数</param>
        /// <param name="standard">小数位数舍入标准</param>
        /// <returns></returns>
        public string GetDecimalDigits(string strOriginalData, int digits,int standard)
        {
            //处理后的数据
            string strNewData = "";
            if (strOriginalData.Contains("."))
            {
                int dotLocal = strOriginalData.IndexOf(".");
                string strTemp = strOriginalData.Substring(0, dotLocal + 1 + digits);
                string strLast = strOriginalData.Substring(dotLocal + 1 + digits, 1);
                if (int.Parse(strLast) >= standard)
                {
                    //如果后一位小数大于等于舍入标准，小数进位
                    strNewData = (int.Parse(strTemp) + 1).ToString();
                }
                else
                {
                    //如果后一位小数小于舍入标准，小数不进位
                    strNewData = strTemp;
                }
            }
            else
            {
                strNewData = strOriginalData;
            }
            return strNewData;
        }

        #endregion

        #region [标准判定]
        /// <summary>
        /// 根据TextBox和Label数据内容判定结果是否合格
        /// </summary>
        /// <param name="strTextBox"></param>
        /// <param name="strLabel"></param>
        /// <returns></returns>
        public bool StandardCalculate(string strTextBox, string strLabel)
        {
            bool result = false;
            string[] strSplite,strLowLimite,strUpperLimite;
            if (strLabel.Trim().Length == 0|| strLabel == "----")
            {
                return true;
            }
            else               //如果标准具有上下限进行上下限的拆分
            {
                strSplite = strLabel.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                if (strSplite.Length == 2)
	            {
            		foreach (string item in strSplite)
	                {
            		    strLowLimite = strSplite[0].Split(' ');
                        strUpperLimite = strSplite[1].Split(' ');
                        if (SubStandardCalculate(strTextBox, strLowLimite[0], strLowLimite[1]) && SubStandardCalculate(strTextBox, strUpperLimite[0], strUpperLimite[1]))     //上下限双边限定比较
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                        }
	                }

	            }
                else if(strSplite.Length == 1)                                            //单边限定进行比较
                {
                    strSplite = strLabel.Split(' ');
                    if (SubStandardCalculate(strTextBox, strSplite[0], strSplite[1]))
                    {
                        result = true;
                    }
                    else
                    {
                        result = false;
                    }
                }
 

            }

            return result;
        }

        /// <summary>
        /// 质量标准比较基本计算公式
        /// </summary>
        /// <param name="strData"></param>
        /// <param name="strOperation"></param>
        /// <param name="strStandard"></param>
        /// <returns></returns>
        public bool SubStandardCalculate(string strData, string strOperation, string strStandard)
        {
            switch (strOperation)
            {
                case "<=":
                    if (decimal.Parse(strData) <= decimal.Parse(strStandard))
                    {
                        return true;
                    }
                    break;

                case ">=":
                    if (decimal.Parse(strData) >= decimal.Parse(strStandard))
                    {
                        return true;
                    }
                    break;
                case ">":
                    if (decimal.Parse(strData) > decimal.Parse(strStandard))
                    {
                        return true;
                    }
                    break;
                case "<":
                    if (decimal.Parse(strData) < decimal.Parse(strStandard))
                    {
                        return true;
                    }
                    break;
                default:
                    break;
            }
            return false;
        }

        //在此追加双上限或是双下限标准比较，需要原料标识，并判断是否具有双限标准
        public bool StandardSuperaddition(string strTextBox, string strLable)
        {
            string[] strSplite, strLimite1, strLimite2;
            decimal dPrecision, dProportion;
            decimal dDeductProportion;
            
            if (strLable == string.Empty || strLable == "----")
            {
                return true;
            }
            else
            {
                strSplite = strLable.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in strSplite)
                {
                    strLimite1 = item.Split(' ');
                    //比较第一个界限
                    if (SubStandardCalculate(strTextBox, strLimite1[0], strLimite1[1]))
                    {
                        return true;
                    }
                    else
                    {
                        if (strSplite[1] != string.Empty)
                        {
                            strLimite2 = strSplite[1].Split(' ');
                            //比较第二个界限
                            if (SubStandardCalculate(strTextBox, strLimite2[0], strLimite2[1]))
                            {
                                //判定精度和扣除比例是否存在，如存在作如下计算
                                if (strSplite[2].ToString() != string.Empty)
                                {
                                    dPrecision = decimal.Parse(strSplite[2]);
                                    if (strSplite[3].ToString() != string.Empty)
                                    {
                                        dProportion = decimal.Parse(strSplite[3]);
                                        dDeductProportion = Math.Abs(decimal.Parse(strTextBox) - decimal.Parse(strLimite1[1])) / dPrecision * dProportion;
                                        MessageBox.Show("建议按照" + dDeductProportion + "%扣除重量！", "提示：", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("建议拒收！", "提示：", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                return false;
                            }
                        }
                        else
                        {
                            //判定精度和扣除比例是否存在，如存在作如下计算
                            if (strSplite[2].ToString() != string.Empty)
                            {
                                dPrecision = decimal.Parse(strSplite[2]);
                                if (strSplite[3].ToString() != string.Empty)
                                {
                                    dProportion = decimal.Parse(strSplite[3]);
                                    dDeductProportion = Math.Abs(decimal.Parse(strTextBox) - decimal.Parse(strLimite1[1])) / dPrecision * dProportion;
                                    MessageBox.Show("建议按照" + dDeductProportion + "%扣除重量！", "提示：", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                    return false;
                                }
                            }
                        }
                    }
                }
                
  
            }
            return false;
        }


        /// <summary>
        /// 根据物料Id和从数据库中检索到的编号计算当前该使用的编号
        /// </summary>
        /// <param name="strMaterialId">物料ID</param>
        /// <param name="strMaxCode">在数据库中检索到的编号</param>
        /// <returns></returns>
        public string GetCurrentSampleCode(string strMaterialId, string strMaxCode)
        {
            string strCurrentCode = "";

            string strCodeTail = strMaxCode.Substring(strMaxCode.LastIndexOf('.') + 1, 4);
            string strCodeHead = strMaxCode.Substring(0, strMaxCode.LastIndexOf('.') + 1);

            strCurrentCode = strCodeHead + (Convert.ToInt32(strCodeTail) + 1).ToString("0000");
            return strCurrentCode;
        }
        #endregion

        #region 多元线性回归计算回归系数

        /// <summary>
        /// 解线性方程
        /// </summary>
        /// <param name="adData">[nCount，(nCount+1)]二维矩阵数组</param>
        /// <param name="nCount">数据行数</param>
        /// <param name="adAnswer">[nCount]一维数组；求解数组</param>
        /// <returns>返回值：0求解成功，-1无解或者无穷解</returns>
        public int LinearEquations(double[,] adData, int nCount, double[] adAnswer)
        {
            int i, m, n;
            double dTmp;
            double[,] adDat = new double[nCount, (nCount+1)];
            double[] adD = new double[nCount + 1];

            for (m = 0; m < nCount; m++)
            {
                for (n = 0; n < nCount + 1; n++)
                {
                    adDat[m, n] = adData[m, n];
                }
            }

            for (m = 0; m < nCount - 1; m++)
            {
                // 如果主对角线元素为0，行交换
                for (n = m + 1; n < nCount && adDat[m, m] == 0.0; n++)
                {
                    if (adDat[n, m] != 0.0)
                    {
                        for (i = 0; i < nCount; i++)
                        {
                            dTmp = adDat[m, i];
                            adDat[m, i] = adDat[n, i];
                            adDat[n, i] = dTmp;
                        }
                    }
                }
                // 行交换后，主对角线元素仍然为0，无解，返回-1
                if (adDat[m, m] == 0.0)
                    return -1;

                // 消元
                for (n = m + 1; n < nCount; n++)
                {
                    dTmp = adDat[n, m] / adDat[m, m];
                    for (i = m; i <= nCount; i++)
                        adDat[n, i] -= dTmp * adDat[m, i];
                }
            }

            for (i = 0; i < nCount; i++)
                adD[i] = 0.0;

            // 求得count - 1的元
            adAnswer[nCount - 1] = adDat[nCount - 1, nCount] / adDat[nCount - 1, nCount - 1];

            // 逐行代入求各元
            for (m = nCount - 2; m >= 0; m--)
            {
                for (i = nCount - 1; i > m; i--)
                    adD[m] += adAnswer[i] * adDat[m, i];
                adAnswer[m] = (adDat[m, nCount] - adD[m]) / adDat[m, m];
            }

            return 0;
        }

        /// <summary>
        /// 求多元回归方程：Y = B0 + B1X1 + B2X2 + ...BnXn
        /// </summary>
        /// <param name="nType">计算类型： 1 一次线性    2 二次非线性  3 三次非线性</param>
        /// <param name="adData">[nRows，nCols]二维数组；X1i,X2i,...Xni,Yi (i=0 to nRows-1)</param>
        /// <param name="nRows">数据行数</param>
        /// <param name="nCols">数据列数</param>
        /// <param name="adValue">[nCols]一维数组；X1,X2,...Xn,Y (X1,X2,...Xn为预测参数，Y为最后返回的预测值)</param>
        ///// <param name="adSquarePoor">[10]一维数组；返回方差分析指标: 回归平方和，剩余平方和，回归平方差，剩余平方差，离差平方和， 标准误差，F检验， 相关系数， 最大剩余误差， 最小剩余误差</param>
        /// <param name="adSquarePoor">[11]一维数组；返回方差分析指标: 回归平方和，剩余平方和，回归平方差，剩余平方差，离差平方和， 标准误差，F检验， 相关系数， 最大剩余误差， 最小剩余误差,标准偏差</param>
        /// <param name="adAnswer">[(nCols - 1) * nType + 1]一维数组；返回回归系数数组(B0,B1...Bn)(Y = B0 + B1X1 + B2X2 + ...BnXn)</param>
        /// <param name="strErrorMessage">出错信息</param>
        /// <returns>返回值：0求解成功，-1错误</returns>
        public int MultipleRegression(int nType, double[,] adDataOld, int nRows, int nCols, double[] adValue, double[] adSquarePoor, double[] adAnswer, out string strErrorMessage)
        {
            if (nType < 1)
            {
                strErrorMessage = "nType < 1";
                return -1;
            }

            int i, j, k, l, m, n, nCount;
            double dTmp, dSq;
            double dMaxY = -100000, dMinY = 100000;
            double dP, dP1, dA, dB;

            double[,] adData = new double[nRows, (nCols - 1) * nType + 1];
            for (i = 0; i < nRows; i++)
            {
                adData[i, (nCols - 1) * nType] = adDataOld[i, nCols - 1];
                for (j = 0; j < nCols - 1; j ++)
                {
                    dTmp = adDataOld[i, j];
                    for (k = 0; k < nType; k++)
                    {
                        dSq = dTmp;
                        for (l = 0; l < k; l++)
                        {
                            dSq = dSq * dTmp;
                        }
                        adData[i, j * nType + k] = dSq;
                    }
                }
            }
            nCols = (nCols - 1) * nType + 1;

//            double[] adAnswer = new double[nCols];  //一维数组；返回回归系数数组(B0,B1...Bn)(Y = B0 + B1X1 + B2X2 + ...BnXn)

            strErrorMessage = "";

            nCount = nCols - 1;
            double[,] adDat = new double[nCols , nCols + 1];

            if (adData == null || adAnswer == null || nRows < 2 || nCols < 2)
            {
                strErrorMessage = "adData == null || adAnswer == null || nRows < 2 || nCols < 2";
                return -1;
            }

            adDat[0, 0] = (double)nRows;
            for (n = 0; n < nCols - 1; n++)                     // n = 0 to nCols - 2
            {
                dA = dB = 0.0;
                for (m = 0; m < nRows; m++)                     // m = 0 to nRows - 1
                {
                    dP = adData[m, n];
                    dA += dP;
                    dB += (dP * dP);
                }

                adDat[0, n + 1] = dA;                            // dat[0, n+1] = Sum(Xn)
                adDat[n + 1, 0] = dA;                            // dat[n+1, 0] = Sum(Xn)
                adDat[n + 1,  n + 1] = dB;                       // dat[n+1,n+1] = Sum(Xn * Xn)

                for (i = n + 1; i < nCols - 1; i++)             // i = n+1 to nCols - 1 - 2
                {
                    dA = 0.0;
                    for (m = 0; m < nRows; m++)                 // m = 0 to nRows - 1
                    {
                        dP = adData[m, n];
                        dP1 = adData[m, i];
                        dA += dP * dP1;
                     }

                    adDat[n + 1, i + 1] = dA;                    // dat[n+1, i+1] = Sum(Xn * Xi)
                    adDat[i + 1, n + 1] = dA;                    // dat[i+1, n+1] = Sum(Xn * Xi)
                }
            }

            dB = 0.0;
            for (m = 0; m < nRows; m++)                         // m = 0 to nRows - 1
            {
                dP = adData[m, n];
                dB += dP;
            }
            adDat[0, nCols] = dB;                                // dat[0, nCols] = Sum(Y)

            for (n = 0; n < nCols - 1; n++)                     // n = 0 to nCols - 2
            {
                dA = 0.0;
                for (m = 0; m < nRows; m++)                     // m = 0 to nRows - 1
                {
                    dP = adData[m, n];
                    dP1 = adData[m, nCols - 1];
                    dA += dP * dP1;
                }


                adDat[n + 1, nCols] = dA;                        // dat[n+1, cols] = Sum(Xn * Y)
            }
            n = LinearEquations(adDat, nCols, adAnswer);        // 计算方程式
            if (n == -1)
            {
                strErrorMessage = "解线性方程无解或者无穷解";
                return -1;
            }

            // 方差分析
            if (n == 0 && adSquarePoor != null)
            {
                dB = dB / nRows;                                  // b = Y的平均值
                adSquarePoor[0] = adSquarePoor[1] = 0.0;
                for (m = 0; m < nRows; m++)                     // m = 0 to nRows - 1
                {
                    for (i = 1, dA = adAnswer[0]; i < nCols; i++)
                    {
                        dP =  adData[m, i - 1];
                        dA += (dP * adAnswer[i]);                 // a = Ym的估计值
                    }
                    dP = adData[m, i - 1];
                    adSquarePoor[0] += ((dA - dB) * (dA - dB));                         // U(回归平方和)
                    adSquarePoor[1] += ((dP - dA) * (dP - dA));                         // Q(剩余平方和)(*p = Ym)
                    //最大值取上限值
                    if (dP - dA > dMaxY)
                        dMaxY = dP - dA;

                    //最小值取下限值
                    if (dP - dA < dMinY)
                        dMinY = dP - dA;

                    //if (Math.Abs(dP - dA) > dMaxY)
                    //    dMaxY = Math.Abs(dP - dA);
                    //if (Math.Abs(dP - dA) < dMinY)
                    //    dMinY = Math.Abs(dP - dA);
                }
                adSquarePoor[2] = adSquarePoor[0] / nCount;                         // 回归方差

                if ((nRows - nCols) > 0)
                    adSquarePoor[3] = adSquarePoor[1] / (nRows - nCols);            // 剩余方差
                else
                    adSquarePoor[3] = 0.0;

                adSquarePoor[4] = adSquarePoor[0] + adSquarePoor[1];                // 离差平方和：

                adSquarePoor[5] = Math.Sqrt(adSquarePoor[3]);                       // 标准误差：
                

                if (adSquarePoor[3] != 0.0)
                    adSquarePoor[6] = adSquarePoor[2] / adSquarePoor[3];            // F检验
                else
                    adSquarePoor[6] = 0.0;

                if (adSquarePoor[4] != 0.0)
                    adSquarePoor[7] = Math.Sqrt(adSquarePoor[0] / adSquarePoor[4]); // 相关系数
                else
                    adSquarePoor[7] = 0.0;
                adSquarePoor[8] = dMaxY;
                adSquarePoor[9] = dMinY;
                //adSquarePoor[10] = Math.Sqrt(adSquarePoor[3] / nRows);//标准偏差
            }

            //返回预测值
            for (i = 1, dA = adAnswer[0]; i < nCols; i += nType)
            {
                dP = adValue[(i - 1) / nType];
                for (k = 0; k < nType; k++)
                {
                    dSq = dP;
                    for (l = 0; l < k; l++)
                    {
                        dSq = dSq * dP;
                    }
                    dA += dSq * adAnswer[i + k];
                }
            }
            adValue[(nCols - 1) / nType] = dA; // a = Y的估计值

            return n;
        }

        /// <summary>
        /// 单纯形法求解不定方程
        /// </summary>
        /// <param name="n">自变量个数</param>
        /// <param name="m">函数个数数</param>
        /// <param name="dEp">精度参数</param>
        /// <param name="adX">[n]一维数组；开始时存放初始值，结束时存放极值点</param>
        /// <param name="adA">[m,n * p + 1]二维数组；存放函数系数值</param>
        /// <param name="p">p次非线性</param>
        /// <returns>无返回值</returns>
        public void SM(int n, int m, double dEp, double[] adX, double[,] adA, int p)
        {
	        double dA1, dB1, dC1, dS = 0, dU = 0, dV, dD = 0;
	        int k, l, nA2 = 0, nC2 = 0, nNum = n + 1;

            double[] adOldX = new double[nNum];
            double[] adR1 = new double[nNum];
            double[] adR2 = new double[nNum];
            double[] adR3 = new double[nNum];
            double[] adQ = new double[nNum];
            double[] adY = new double[nNum];
            double[] adP = new double[nNum * nNum];

            for (k = 1; k <= n; k++)
            {
                adOldX[k] = adX[k];
            }     	

	        dV = 0.0001;
        ds1:for(l = 0; l <= n; l++)
	        {
		        for(k = 1; k <= n; k++)
		        {
			        if(l != k)
			        {
                        adR1[k] = adX[k];
                        adP[l * (n + 1) + k] = adX[k];
			        }
                    else if (Math.Abs(adX[k]) < 0.0000000001)
			        {
				        adR1[k] = 0.0000000001;
				        adP[l * (n + 1) + k] = 0.0000000001;
			        }
			        else
			        {
                        adR1[k] = (1 + dV) * adX[k];
				        adP[l*(n + 1) + k] = adR1[k];
			        }
		        }
                adQ[l] = SubFunctionSM(n, m, adR1, adY, adA, p, adOldX);
	        }
        ds2:dA1 = 0;
	        dB1 = 0;
	        dC1 = 1000000000000000000;
	        for(l = 0; l <= n; l++)
	        {
		        if(dA1 > adQ[l]) goto ds3;
		        dA1 = adQ[l];
		        nA2 = l;
        ds3:	if(dC1 < adQ[l]) goto ds4;
		        dC1 = adQ[l];
		        nC2 = l;
        ds4:;
	        }
	        for(l = 0; l <= n; l++)
	        {
		        if(l != nA2 && dB1 < adQ[l])
		        {
			        dB1 = adQ[l];
		        }
	        }
	        for(k = 1; k <= n; k++)
	        {
                adX[k] = adP[nC2 * (n + 1) + k];
		        adR1[k] = 0;
	        }
            dD++;
            if (dC1 <= dEp || Math.Abs(1 - dA1 / dC1) <= dEp || dD >= 1000000) goto ds7;
            for (l = 0; l <= n; l++)
	        {
		        for(k = 1;k <= n; k++)
		        {
			        if(l != nA2) adR1[k] = adR1[k] + adP[l * (n + 1) + k] /n;
		        }
	        }
	        for(k = 1;k <= n; k++)
	        {
		        adR2[k] = 2 * adR1[k] - adP[nA2 * (n + 1) + k];
	        }
            dS = SubFunctionSM(n, m, adR2, adY, adA, p, adOldX);
	        if(dS >= dA1) goto ds6;
	        for(k = 1; k <= n; k++)
	        {
		        adR3[k] = 3 * adR1[k] - 2 * adP[nA2 * (n + 1) + k];
		        adP[nA2 * (n + 1) + k] = adR2[k];
	        }
	        adQ[nA2] = dS;
            dU = SubFunctionSM(n, m, adR3, adY, adA, p, adOldX);
	        if(dU >= dS) goto ds2;
        ds5:for(k = 1;k <= n; k++)
	        {
		        adP[nA2 * (n + 1) + k] = adR3[k];
	        }
	        adQ[nA2] = dU;
	        goto ds2;
        ds6:for(k = 1; k <= n; k++)
	        {
		        adR3[k]=0.5 * adR1[k] + 0.5 * adP[nA2 * (n + 1) + k];
	        }
            dU = SubFunctionSM(n, m, adR3, adY, adA, p, adOldX);
	        if(dU < dB1) goto ds5;
	        dV = 0.5 * dV;
	        goto ds1;
        ds7: ;
        }

        /// <summary>
        /// 单纯形法子函数
        /// </summary>
        /// <param name="n">自变量个数</param>
        /// <param name="m">函数个数数</param>
        /// <param name="adX">[n]一维数组；存放自变量</param>
        /// <param name="adY">[m]一维数组；存放函数值</param>
        /// <param name="adA">[m,n * p + 1]二维数组；存放函数系数值</param>
        /// <param name="p">p次非线性</param>
        /// <returns>无返回值</returns>
        public double SubFunctionSM(int n, int m, double[] adX, double[] adY, double[,] adA, int p, double[] adOldX)
        {
            int i, j, k;
            double dSumTmp, dTmp;

            //*********************************************//	ffun(n,m,blx,y,a,o);
            for (i = 1; i <= m; i++)
            {
                dSumTmp = adA[i - 1, 0];
                for (j = 0; j < n; j++)
                {
                    if (adX[j + 1] > 0)
                    {
                        dTmp = adX[j + 1]; 
                        for (k = 0; k < p; k++)
                        {
                            dSumTmp = dSumTmp + adA[i - 1, j * p + k + 1] * dTmp;
                            dTmp = dTmp * adX[j + 1];
                        }
                    }
                }
                adY[i] = dSumTmp;
            }
            adY[0] = 0;
            //*********************************************//  求误差平方和
            dSumTmp = 0;
            for (i = 1; i <= m; i++)
            {
                dSumTmp = dSumTmp + adY[i] * adY[i];
            }
            //**********************************************//  加梯度控制
            double dGradient;
            if (m > 1)
            {
                dGradient = 33333.3;
            }
            else
            {
                dGradient = 99999.9;
            }
            dTmp = 0;
            for (i = 1; i <= n; i++)
            {
                dTmp = dTmp + Math.Abs((adX[i] - adOldX[i]));
            }
            dSumTmp = dSumTmp + dTmp * dGradient;
            //**********************************************
            return (dSumTmp);
        }


        #endregion

        #region 【计算合格率、合格个数、平均值】
        /// <summary>
        /// 计算合格率、合格个数、平均值
        /// </summary>
        /// <param name="standard">质量标准</param>
        /// <param name="datagridview">加载数据的DataGridView</param>
        /// <param name="dataColumn">DataColumn列名称</param>
        /// <param name="nNumber">保留小数的位数</param>
        /// <returns>字符串数组：合格数/总数，合格率（%），平均值</returns>
        public string[] GetControlCalculateEligibleAvg(string standard, DataGridView datagridview,string dataColumn,int nNumber)
        {
            Algorithm algorithm =new Algorithm();
            string[] strArray = new string[3];
            string strValue = string.Empty;
            int nAllValue=0,nNGValue=0;
            decimal dcTotalValue=0;
            for(int i=0;i < datagridview.Rows.Count;i++)
            {
                if (standard != "----" && standard != string.Empty)
                {
                    if (datagridview.Rows[i].Cells[dataColumn].Value != null)
                    {
                        strValue = datagridview.Rows[i].Cells[dataColumn].Value.ToString();
                        dcTotalValue += decimal.Parse(strValue);
                        nAllValue++;
                        if (algorithm.StandardCalculate(strValue, standard) == false)
                        {
                            datagridview.Rows[i].Cells[dataColumn].Style.ForeColor = Color.Red;
                            nNGValue++;
                        }
                    }
                }
            }
            if (nAllValue > 0)
            {
                strArray[0] = (nAllValue - nNGValue).ToString() + "/" + nAllValue.ToString();
                strArray[1] = (100 * (nAllValue - nNGValue) / nAllValue).ToString("f2") + "%";
                strArray[2] = Math.Round(dcTotalValue / nAllValue,nNumber).ToString();
            }
            else
            {
                strArray[0] = "0";
                strArray[1] = "0%";
                strArray[2] = "0";
            }
            return strArray;
        }
        #endregion

        #region [删除数字类型最后的零]
        /// <summary>
        /// 删除数字类型（字符串）最后的零（如果是整数该方法不进行操作）
        /// </summary>
        /// <param name="strSource">数字类型（字符串）</param>
        /// <returns>字符串</returns>
        public string DeleteLastZero(string strSource)
        {
            string strDestination;

            if (strSource.Contains("."))
            {
                strDestination = strSource.TrimEnd(new char[] { '0' });
            }
            else
            {
                strDestination = strSource;
            }
            strDestination = strDestination.TrimEnd(new char[] { '.' });
            return strDestination;
        }
        #endregion

        #region [查找标准拼接字符串]
        /// <summary>
        /// 查找标准拼接字符串
        /// </summary>
        /// <param name="item">质量标准实体类的记录</param>
        /// <returns>字符串</returns>
        public string StandardQuery(tQCS_STD_StandardInfo item)
        {
            string strStandard = "", strTemp;
            if (item.Std_BestLowerLimitOperation.Trim().Length != 0)
            {
                strTemp = DeleteLastZero(item.Std_BestLowerLimit.ToString());
                strStandard += item.Std_BestLowerLimitOperation + " " + strTemp + "\r\n";

            }

            if (item.Std_LowerLimitOperation.Trim().Length != 0)
            {
                strTemp = DeleteLastZero(item.Std_LowerLimit.ToString());
                strStandard += item.Std_LowerLimitOperation + " " + strTemp + "\r\n";
            }

            if (item.Std_UpperLimitOperation.Trim().Length != 0)
            {
                strTemp = DeleteLastZero(item.Std_UpperLimit.ToString());
                strStandard += item.Std_UpperLimitOperation + " " + strTemp + "\r\n";
            }

            if (item.Std_BestUpperLimitOperation.Trim().Length != 0)
            {
                strTemp = DeleteLastZero(item.Std_BestUpperLimit.ToString());
                strStandard += item.Std_BestUpperLimitOperation + " " + item.Std_BestUpperLimit;
            }

            //if(strStandard.Substring(strStandard.Length-1,1) == ";")
            //                ｛
            //                    RawKH = RawKH.Substring(0, RawKH.Length-1); //去掉最后
            //                    //s1.Replace(s1.Substring(s1.Length-1,1),"" )//替换空格
            //                ｝
            return strStandard;
        }

        #endregion

        #region [熟料热耗计算：将KCal/Kg转换为MJ/Kg]
        /// <summary>
        /// 熟料热耗计算：将KCal/Kg转换为MJ/Kg
        /// </summary>
        /// <param name="dCoefficient">转化系数</param>
        /// <param name="dKCal">熟料热耗====单位：KCal</param>
        /// <returns>熟料热耗====单位：MJ</returns>
        public decimal KCalToMJ(decimal dCoefficient, decimal dKCal)
        {
            decimal dMJ = 0;
            dMJ = dKCal * dCoefficient / 1000;
            return dMJ;
        }
        #endregion

    }
        


}
