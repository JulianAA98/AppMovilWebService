package com.soul.qrupt

import android.Manifest
import android.content.Context
import android.content.Intent
import android.content.pm.PackageManager
import android.net.Uri
import android.os.AsyncTask
import android.os.Build
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import com.android.volley.DefaultRetryPolicy
import com.android.volley.Request
import com.android.volley.Response
import com.android.volley.toolbox.JsonArrayRequest

import org.json.JSONObject

import android.util.Base64
import android.util.Log
import android.view.SurfaceHolder
import android.view.SurfaceView
import android.webkit.URLUtil
import android.widget.Toast
import androidx.core.app.ActivityCompat
import com.android.volley.NetworkResponse
import com.android.volley.toolbox.HttpHeaderParser
import com.android.volley.toolbox.JsonObjectRequest
import com.android.volley.toolbox.Volley

import com.google.android.gms.vision.CameraSource
import com.google.android.gms.vision.Detector
import com.google.android.gms.vision.barcode.Barcode
import com.google.android.gms.vision.barcode.BarcodeDetector
import java.io.IOException
import java.nio.charset.Charset
import java.security.MessageDigest
import javax.crypto.Cipher
import javax.crypto.SecretKey
import javax.crypto.spec.SecretKeySpec

class MainActivity : AppCompatActivity() {



    private var cameraSource: CameraSource? = null
    private var cameraView: SurfaceView? = null
    private val MY_PERMISSIONS_REQUEST_CAMERA = 1
    private var token = ""
    private var tokenanterior = ""

    var matricula="";
    var etapa="";

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        cameraView = findViewById(R.id.camera_view)
        initQR()
    }

    fun initQR() {


        val barcodeDetector = BarcodeDetector.Builder(this)
            .setBarcodeFormats(Barcode.ALL_FORMATS)
            .build()


        cameraSource = CameraSource.Builder(this, barcodeDetector)
            .setRequestedPreviewSize(1600, 1024)
            .setAutoFocusEnabled(true) //you should add this feature
            .build()

        // listener de ciclo de vida de la camara
        cameraView!!.holder.addCallback(object : SurfaceHolder.Callback {
            override fun surfaceCreated(holder: SurfaceHolder) {

                // verifico si el usuario dio los permisos para la camara
                if (ActivityCompat.checkSelfPermission(
                        this@MainActivity,
                        Manifest.permission.CAMERA
                    ) != PackageManager.PERMISSION_GRANTED
                ) {

                    if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.M) {

                        if (shouldShowRequestPermissionRationale(
                                Manifest.permission.CAMERA
                            )
                        );
                        requestPermissions(
                            arrayOf(Manifest.permission.CAMERA),
                            MY_PERMISSIONS_REQUEST_CAMERA
                        )
                    }
                    return
                } else {
                    try {
                        cameraSource!!.start(cameraView!!.holder)
                    } catch (ie: IOException) {
                        Log.e("CAMERA SOURCE", ie.message)
                    }

                }
            }

            override fun surfaceChanged(
                holder: SurfaceHolder,
                format: Int,
                width: Int,
                height: Int
            ) {
            }

            override fun surfaceDestroyed(holder: SurfaceHolder) {
                cameraSource!!.stop()
            }
        })


        barcodeDetector.setProcessor(object : Detector.Processor<Barcode> {
            override fun release() {}


            override fun receiveDetections(detections: Detector.Detections<Barcode>) {
                val barcodes = detections.getDetectedItems()

                if (barcodes.size() > 0) {


                    token = barcodes.valueAt(0).displayValue.toString()


                    if (token != tokenanterior) {


                        tokenanterior = token
                        Log.i("token", token)

                        if (URLUtil.isValidUrl(token)) {

                            val browserIntent = Intent(Intent.ACTION_VIEW, Uri.parse(token))
                            startActivity(browserIntent)
                        } else {
                            desencriptar(token)


                        }

                        Thread(object : Runnable {
                            override fun run() {
                                try {
                                    synchronized(this) {

                                        tokenanterior = ""
                                    }
                                } catch (e: InterruptedException) {
                                    // TODO Auto-generated catch block
                                    Log.e("Error", "Waiting didnt work!!")
                                    e.printStackTrace()
                                }

                            }
                        }).start()

                    }
                }
            }
        })

    }

    private fun desencriptar(cadena:String?):String
    {

        var key = "UPTecamacHALL"
        var md5 = MessageDigest.getInstance("MD5")
        md5.update(key.toByteArray())
        var digest = md5.digest()
        var llave: SecretKey = SecretKeySpec(digest,"DESede")
        var mensaje = Base64.decode(cadena?.toByteArray(), Base64.DEFAULT)
        var Algoritmo = "DESede/ECB/PKCS7Padding"
        var descifrado: Cipher = Cipher.getInstance(Algoritmo)
        descifrado.init(Cipher.DECRYPT_MODE, llave)
        var textoPlano = String(descifrado.doFinal(mensaje), Charsets.UTF_8)


        matricula=textoPlano.split("/")[0];
        etapa=textoPlano.split("/")[4];
        peticionCarta("http://192.168.137.191/api_rest/Alumnos/Insertar") //Direccion del servicio web IPAldair
        return textoPlano
        //http://localhost:54922/api/Alumnos
    }


    private fun peticionCarta(url:String){
        var json = JSONObject() 
        json.put("Matricula", matricula) //Matricula
        json.put("Etapa", etapa) //Etapa
        val cola = Volley.newRequestQueue(this)
        val solicitud = JsonObjectRequest(Request.Method.POST, url, json, Response.Listener{
                response ->
            try {
                var pref = this.getSharedPreferences("Alumnos", Context.MODE_PRIVATE) //Buscar en x parte de nuestro servicio
                val editor = pref.edit()

                if(response.getBoolean("Resultado"))
                {
                    Toast.makeText(this,"Revisado", Toast.LENGTH_LONG).show() //
                }else{
                    Toast.makeText(this,"No revisado", Toast.LENGTH_LONG).show() //
                }

            }catch (e:Exception){
            }
        }, Response.ErrorListener { error ->
            Log.d("Error API:", error.message)
        } )
        cola.add(solicitud)
    }
}



