package com.example.patel_000.myproject;


import android.app.ProgressDialog;
import android.content.SharedPreferences;
import android.graphics.BitmapFactory;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.ColorDrawable;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v4.app.DialogFragment;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import static com.example.patel_000.myproject.R.color.colorAccent;


/**
 * A simple {@link Fragment} subclass.
 */
public class NewBookingFragment extends Fragment implements AdapterView.OnItemSelectedListener, View.OnClickListener, View.OnTouchListener {

    View v1;
    TextView tvdate;
    Spinner spinner, spinnerquant;
    ColorDrawable bg, hv;
        String link, unm, pass, date, catquan, comm, cat;
    List<String> categories;
    Button book;
    String id[];
    EditText comment;

    public NewBookingFragment() {
        // Required empty public constructor
    }


    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        v1 = inflater.inflate(R.layout.fragment_new_booking, container, false);

        // Spinner element
        SharedPreferences sp1 = v1.getContext().getSharedPreferences("Login", 0);
        link = getResources().getString(R.string.url);
        unm = sp1.getString("Unm", null);
        pass = sp1.getString("Psw", null);
        comment = (EditText) v1.findViewById(R.id.tvcomment);
        spinner = (Spinner) v1.findViewById(R.id.spincat);
        spinnerquant = (Spinner) v1.findViewById(R.id.spinres);
        tvdate = (TextView) v1.findViewById(R.id.tvdate);
        book = (Button) v1.findViewById(R.id.btnbook);
        bg = new ColorDrawable(getResources().getColor(R.color.colorPrimary));
        hv = new ColorDrawable(getResources().getColor(R.color.colorAccent));
        tvdate.setBackground(bg);
        book.setBackground(hv);


        tvdate.setOnClickListener(this);
        tvdate.setOnTouchListener(this);
        spinnerquant.setOnTouchListener(this);
        spinner.setOnTouchListener(this);
        book.setOnTouchListener(this);// Spinner click listener

        spinner.setOnItemSelectedListener(this);
        spinnerquant.setOnItemSelectedListener(this);

        // Spinner Drop down elements
        categories = new ArrayList<String>();
        categories.add("select Category");
        //categories.add("Cranes");
        //categories.add("Trucks");
        //id= new String[3];
        //id[1]="2";
        //id[2]="3";
        WebTask wb = new WebTask();
        wb.execute(unm, pass);

        List<String> quant = new ArrayList<String>();
        quant.add("select quantity");
        quant.add("1");
        quant.add("2");
        quant.add("3");
        quant.add("4");
        quant.add("5");


        // Creating adapter for spinner
        ArrayAdapter<String> dataAdapter = new ArrayAdapter<String>(this.getActivity(), R.layout.spinner_custom, categories);
        dataAdapter.setDropDownViewResource(android.R.layout.simple_dropdown_item_1line);

        ArrayAdapter<String> dataAdapterres = new ArrayAdapter<String>(this.getActivity(), R.layout.spinner_custom, quant);
        dataAdapterres.setDropDownViewResource(android.R.layout.simple_dropdown_item_1line);

        // attaching data adapter to spinner
        spinner.setAdapter(dataAdapter);
        spinnerquant.setAdapter(dataAdapterres);
        book.setOnClickListener(this);


        return v1;
    }

    @Override
    public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {

        String item = parent.getItemAtPosition(position).toString();


        // Showing selected spinner item
    }

    @Override
    public void onNothingSelected(AdapterView<?> parent) {

    }

    @Override
    public void onClick(View v) {
        if (v == tvdate) {
            showDatePickerDialog(v);


        }
        if (v == book) {

            date = tvdate.getText().toString();
            catquan = spinnerquant.getSelectedItem().toString();
            comm = comment.getText().toString();
            for (int i = 0; i < categories.size(); i++) {
                if (categories.get(i).equals(spinner.getSelectedItem().toString())) {
                    cat = id[i-1];
                }
            }
            WebTaska wb1 = new WebTaska();
            wb1.execute(unm, pass);

        }

    }

    public void showDatePickerDialog(View v) {
        DatePickerFragment newFragment = new DatePickerFragment();
        newFragment.show(getFragmentManager(), "datePicker");


    }


    @Override
    public boolean onTouch(View v, MotionEvent event) {
        if (v == tvdate) {
            if (event.getAction() == MotionEvent.ACTION_DOWN) {
                //Button Pressed
                tvdate.setBackground(hv);
            }

            if (event.getAction() == MotionEvent.ACTION_UP) {
                //finger was lifted
                tvdate.setBackground(bg);
            }
        }

        if (v == book) {
            if (event.getAction() == MotionEvent.ACTION_DOWN) {
                //Button Pressed
                book.setBackground(bg);
            }

            if (event.getAction() == MotionEvent.ACTION_UP) {
                //finger was lifted
                book.setBackground(hv);
            }
        }
        return false;
    }



    public class WebTask extends AsyncTask<String, String, String> {

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
            request.setUri(link + "/api/ResCategoryAPI/SelectAllCategory");
            request.setMethod("GET");

            request.setParam("Username", unm);
            request.setParam("Password", pass);

            String ans = (String) HttpManager.getData(request);
            try {
                JSONArray arr = new JSONArray(ans);
                id = new String[arr.length()];
                for (int i = 0; i < arr.length(); i++) {
                    JSONObject obj = arr.getJSONObject(i);
                    categories.add(obj.get("Name").toString());
                    id[i] = obj.getString("ResCategoryID");
                }


            } catch (JSONException e) {
                e.printStackTrace();
            } catch (Exception e) {
                e.printStackTrace();
            }

            return "";
        }

        @Override
        protected void onPostExecute(String ans) {
            super.onPostExecute(ans);
            try {
                Dialog.dismiss();

            } catch (Exception e) {

            }


            //Toast.makeText(Login.this,ans,Toast.LENGTH_LONG).show();


        }
    }



    public class WebTaska extends AsyncTask<String, String, String> {

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
            request.setUri(link + "/api/ResBookingAPI/ResourceBooking");
            request.setMethod("GET");

            request.setParam("Username", unm);
            request.setParam("Password", pass);
            request.setParam("Date", date);
            request.setParam("qty", catquan);
            request.setParam("ResCategoryID", cat);
            request.setParam("Comments", comm);

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


            Toast.makeText(v1.getContext(), ans, Toast.LENGTH_LONG).show();


        }
    }
   /* @Override
    public void onBackPressed()
    {
        if(getFragmentManager().getBackStackEntryCount() > 0)
            getFragmentManager().popBackStack();
        else
            super.onBackPressed();
    }*/
}


