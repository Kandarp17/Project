package com.example.patel_000.myproject;


import android.app.ProgressDialog;
import android.content.SharedPreferences;
import android.graphics.drawable.ColorDrawable;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;


/**
 * A simple {@link Fragment} subclass.
 */
public class UpdateFragment extends Fragment implements View.OnTouchListener, View.OnClickListener {

    String link, unm, pass, pervalue,feedback;
    View v1;
    EditText per, feed;
    Button submit;
    ColorDrawable bg,hv;
    public UpdateFragment() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        v1= inflater.inflate(R.layout.fragment_update, container, false);

        SharedPreferences sp1 = v1.getContext().getSharedPreferences("Login", 0);
        link = getResources().getString(R.string.url);
        unm = sp1.getString("Unm", null);
        pass = sp1.getString("Psw", null);
        bg = new ColorDrawable(getResources().getColor(R.color.colorPrimary));
        hv = new ColorDrawable(getResources().getColor(R.color.colorAccent));
        findAllViews();
        submit.setOnTouchListener(this);
        submit.setOnClickListener(this);
        return v1;
    }

    private void findAllViews() {
        per=(EditText)v1.findViewById(R.id.percentage);
        feed=(EditText)v1.findViewById(R.id.feedback);
        submit=(Button)v1.findViewById(R.id.btnsumbit);
    }


    @Override
    public boolean onTouch(View v, MotionEvent event) {

        if (v == submit) {
            if (event.getAction() == MotionEvent.ACTION_DOWN) {
                //Button Pressed
                submit.setBackground(bg);
            }

            if (event.getAction() == MotionEvent.ACTION_UP) {
                //finger was lifted
                submit.setBackground(hv);
            }
        }

        return false;
    }

    @Override
    public void onClick(View v) {
        if (v == submit) {

            pervalue = per.getText().toString();

            feedback = feed.getText().toString();
            WebTask wb=new WebTask();
            wb.execute();

        }
    }
        class WebTask extends AsyncTask<String, String, String> {

            ProgressDialog Dialog = null;


            @Override
            protected void onPreExecute() {
                super.onPreExecute();
                Dialog = new ProgressDialog(v1.getContext());
                Dialog.setIndeterminate(true);
                Dialog.setCancelable(false);
                Dialog.setTitle("Loading.....");
                Dialog.setMessage(".....Please Wait.....");
                Dialog.show();
            }

            @Override
            protected String doInBackground(String... params) {
                RequestPackage request = new RequestPackage();
                request.setUri(link + "/api/SitePhotoAPI/GetUpdate");
                request.setMethod("GET");

                request.setParam("Username", unm);
                request.setParam("Password", pass);
                request.setParam("Percentage", pervalue);
                request.setParam("Feedback", feedback);


                String ans = (String) HttpManager.getData(request);


                return ans;
            }

            @Override
            protected void onPostExecute(String ans) {
                super.onPostExecute(ans);
                try {
                    Dialog.dismiss();

                } catch (Exception e) {

                }

                if (ans.startsWith("\"Updated\"")) {
                    Toast.makeText(v1.getContext(), ans , Toast.LENGTH_LONG).show();
                }
                else {
                    Toast.makeText(v1.getContext(), ans + "Try again", Toast.LENGTH_LONG).show();
                }


            }
        }
}
